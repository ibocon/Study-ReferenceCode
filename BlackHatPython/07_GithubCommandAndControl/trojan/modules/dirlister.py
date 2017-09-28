
import os


# this enables to laod each module and leaves enough extensibility
# So if desire, can customize the config files to pass args to module
def run(**args):
    print "[*] In dirlister module."
    files = os.listdir(".")
    return str(files)
