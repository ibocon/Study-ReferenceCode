# Windows stores local passwords in the 'SAM' registry hive in a hased format,
# and alongisde Windows boot key stored in the 'system' registry hive.

# Get Profiles
# python vol.py imageinfo -f "memorydump.img"
# Find where registry hives live in memory
# python vol.py hivelist --profile=<profileinfo> -f "<profileinfo>.vmem"
# Get Hash
# python vol.py hashdump -d -d -f "<profileinfo>.vmem" --profile=<profileinfo> -y <system's virtual> -s <?address?>

import sys
import struct
import volatility.conf as conf
import volatility.registry as registry

# set a variable to point to the memory image that going to analyze
memory_file       = "<profileinfo>.vmem"
# in order to import the Volatility libraries
sys.path.append("<Volatility Root Folder>")

# set up instance of Volatility with profile and configuration options
registry.PluginImporter()
config = conf.ConfObject()

import volatility.commands as commands
import volatility.addrspace as addrspace

config.parse_options()
config.PROFILE  = "<profileinfo>"
config.LOCATION = "file://%s" % memory_file

registry.register_global_options(config, commands.Command)
registry.register_global_options(config, addrspace.BaseAddressSpace)

from volatility.plugins.registry.registryapi import RegistryApi
from volatility.plugins.registry.lsadump import HashDump

# instantiate a new instace of 'RegistryApi' which is helper class
registry = RegistryApi(config)
# 'populate_offsets' performs the equivalent to running the 'hivelist' command.
registry.populate_offsets()

sam_offset = None
sys_offset = None

# start walking through each of the discovered hives looking for 'SAM' and 'system' hives.
for offset in registry.all_offsets:

    if registry.all_offsets[offset].endswith("\\SAM"):
        sam_offset = offset
        print "[*] SAM: 0x%08x" % offset

    if registry.all_offsets[offset].endswith("\\system"):
        sys_offset = offset
        print "[*] System: 0x%08x" % offset

    # if found target hive, update the current configuration object with respective offsets
    if sam_offset is not None and sys_offset is not None:
        config.sys_offset = sys_offset
        config.sam_offset = sam_offset

        hashdump = HashDump(config)

        # iterate over the results from the calculate function call,
        # which produces the actual usernames and associated hashes.
        for hash in hashdump.calculate():
            print hash

        break

if sam_offset is None or sys_offset is None:
    print "[*] Failed to find the system or SAM offsets."
