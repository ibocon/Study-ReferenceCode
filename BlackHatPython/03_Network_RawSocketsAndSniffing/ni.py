import netifaces
import socket
import fcntl
import struct
import sys


def get_netmask(ifname):
        s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
        return socket.inet_ntoa(fcntl.ioctl(s.fileno(), 0x891b, struct.pack('256s', ifname))[20:24])

if len(sys.argv) == 2:
        print get_netmask(sys.argv[1])

def get_networkinterfaces():
    return netifaces.interfaces()


# return ip_address of the specified interface name
def get_ip_address(ifname):
    interface = ifname if ifname else 'eth0';
    netifaces.ifaddresses(ifname)
    ip = netifaces.ifaddresses(ifname)[2][0]['addr']
    return ip