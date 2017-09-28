package team;

import java.io.DataOutputStream;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class WebExporter implements Exporter {

	protected final String address;
	protected final int port;
	protected final JSONExporter json;
	
	public WebExporter(String a, int p, JSONExporter e){
		address = a;
		port = p;
		json = e;
	}
	
	@Override
	public void export(Exportable e) {
		Task t = new Task("sendresults",json.exportToString(e));
		Thread newThread = new Thread(t);
		newThread.start();
	}
	
	public void reset() {
		Task t = new Task("clear","");
		Thread newThread = new Thread(t);
		newThread.start();
	}
	
	private class Task implements Runnable {
		
		final String cmd;
		final String txt;
		
		Task(String command, String message) {
			cmd = command;
			txt = message;
		}

		@Override
		public void run() {
			synchronized(WebExporter.this) {
				try {
					//Client will connect to this location
					URL site = new URL("http://"+address+":"+port+"/" + cmd);
					HttpURLConnection conn = (HttpURLConnection) site.openConnection();
	
					// now create a POST request
					conn.setRequestMethod("POST");
					conn.setDoOutput(true);
					conn.setDoInput(true);
					DataOutputStream out = new DataOutputStream(conn.getOutputStream());
					
	
					// write out string to output buffer for message
					out.writeBytes(txt);
					out.flush();
					out.close();
	
					InputStreamReader inputStr = new InputStreamReader(conn.getInputStream());
	
					// string to hold the result of reading in the response
					StringBuilder sb = new StringBuilder();
	
					// read the characters from the request byte by byte and build up the sharedResponse
					int nextChar = inputStr.read();
					while (nextChar > -1) {
						sb=sb.append((char)nextChar);
						nextChar=inputStr.read();
					}
					System.out.println("Return String: "+ sb);
	
				} catch (Exception ex) {
					System.out.println("WebExporter failed to send run info.");
				}
			}
		}
		
	}

}
