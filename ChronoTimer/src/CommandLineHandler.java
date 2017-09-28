import java.io.File;
import java.io.FileNotFoundException;
import java.io.PrintStream;
import java.util.AbstractMap;
import java.util.Collection;
import java.util.HashMap;
import java.util.HashSet;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Map.Entry;
import java.util.Scanner;
import java.util.Set;

import team.Channel;
import team.ChronoTimer;
import team.EventType;
import team.PlaceHolderRecord;
import team.RacerRecord;
import team.Record;
import team.Run;
import team.SensorType;
import team.TChronoTimer;
import team.TRun;

public class CommandLineHandler {
	
	/**
	 * The CommandLineHandler instance.
	 */
	private static CommandLineHandler instance;
	/**
	 * A map that maps all commands names/aliases to their respective command object.
	 */
	private static final Map<String,Command> mcommands;
	/**
	 * A set containing all the commands 1/command.
	 */
	private static final Set<Command> scommands;
	
	/**
	 * @return The CommandLineHandler instance, if not initialized it will initialize it.
	 */
	public static CommandLineHandler getSingleton() {
		if(instance == null)
			instance = new CommandLineHandler(new TChronoTimer(),System.out);
		return instance;
	}
	
	/**
	 * Returns a command with that name or alias.
	 * @param cmd - the name or alias of the command.
	 * @return The command or null if there is no command with that name/alias.
	 */
	public static final Command getCommand(String cmd) {
		return mcommands.get(cmd.toLowerCase());
	}
	
	/**
	 * Adds a given command.
	 * @param c - The command that should be added.
	 * @return true if the command was added successfully, false if there was already a command that shares the name or an alias.
	 */
	public static final boolean addCommand(Command c) {
		if(c == null) return false;
		if(mcommands.containsKey(c.name)) return false;
		for(String a : c.aliases) {
			if(mcommands.containsKey(a)) return false;
		}
		mcommands.put(c.name,c);
		scommands.add(c);
		for(String a : c.aliases) {
			mcommands.put(a,c);
		}
		return true;
	}
	
	/**
	 * Removes a given command.
	 * @param c - The command that should be removed.
	 * @return true if the command was previously added, false if it was never added.
	 */
	public static final boolean removeCommand(Command c) {
		if(c == null) return false;
		if(mcommands.remove(c.name) == null)
			return false;
		for(String a : c.aliases)
			mcommands.remove(a);
		scommands.remove(c);
		return true;
	}
	
	/**
	 * The chronotimer being simulated.
	 */
	private final ChronoTimer timer;
	/**
	 * The stream that all output should be sent to.
	 */
	private PrintStream stream;
	/**
	 * The scanner used for user input.
	 */
	private final Scanner scan;
	/**
	 * Boolean indicating if the CLI should keep running or if it should stop.
	 */
	protected boolean keepRunning;
	
	public static void main(String[] args) {
		
		getSingleton().handle(System.out);
		
	}
	
	/**
	 * Executes a given string as a command with arguements.
	 * @param in - The input string that contains the command name/alias and its arguments.
	 * @return true if the command exists, false if it does not exist.
	 */
	public boolean executeCommand(String in) {
		
		String[] oargs = in.split(" ");
		Command cmd = getCommand(oargs[0]);
		
		if(cmd == null) {
			stream.println("Invalid command, you may want to use the 'help' command.");
			return false;
		}
		String[] args = new String[oargs.length-1];
		for(int i=0;i<oargs.length-1;i++)
			args[i] = oargs[i+1];
		
		cmd.run(stream, timer, args);
		return true;
	}
	
	/**
	 * Executes file located at given path.
	 * @param path - The path of the file that should be executed.
	 */
	public void executeFile(String path) {
		executeFile(new File(path));
	}
	
	/**
	 * Executes a given file.
	 * @param f - The file that should be executed.
	 */
	public void executeFile(File f) {
		if(f == null || !f.exists() || !f.isFile()) return;
		Scanner in;
		try {
			in = new Scanner(f);
		} catch (FileNotFoundException e) {
			System.out.println("File not found!");
			return;
		}
		String line;
		Entry<Long,String> delayedCommand;
		line = in.nextLine();
		executeCommand(extractCommandFileLine(line).getValue());
		while(in.hasNext()) {
			line = in.nextLine();
			delayedCommand = extractCommandFileLine(line);
			if(delayedCommand == null) break;
			timer.getTimeManager().setTime(timer.getTimeManager().formatTime(delayedCommand.getKey()));
			executeCommand(delayedCommand.getValue());
		}
		in.close();
	}
	
	private Entry<Long,String> extractCommandFileLine(String line) {
		String stime = line.substring(0,line.indexOf("	"));
		String command = line.substring(line.indexOf("	"),line.length()).replace("	", "");
		long time = timer.getTimeManager().intoMillisecs(stime);
		return new AbstractMap.SimpleEntry<Long,String>(time,command);
	}
	
	/**
	 * The main CLI loop. It blocks waiting for input from the user then executes the given command until it should quit.
	 * @param s - The stream it should send all output to.
	 */
	private void handle(PrintStream s) {
		
		String input;
		
		while(keepRunning) {
			
			s.print(">");
			s.flush();
			input = scan.nextLine();
			
			executeCommand(input);
			
		}
		
		s.println("Simulator ended.");
		
	}
	
	public CommandLineHandler(ChronoTimer t, PrintStream s) {
		timer = t;
		scan = new Scanner(System.in);
		stream = s;
		keepRunning = true;
	}
	
	// Statically initialize all the commands.
	static {
		mcommands = new HashMap<String,Command>();
		scommands = new HashSet<Command>();
		
		addCommand(new Command("help","[command]...","Lists given or all commands.","?") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				Collection<Command> cm;
				if(args.length == 0) {
					cm = scommands;
				} else {
					cm = new LinkedHashSet<Command>();
					for(String arg : args) {
						Command cmd = getCommand(arg.toLowerCase());
						if(cmd == null) {
							stream.println("No command found called '" + arg + "'");
							continue;
						}
						cm.add(cmd);
					}
				}
				if(cm.size() == 0)
					return true;
				stream.println("COMMAND | USAGE | DESCRIPTION | ALIASES");
				stream.println("=======================================");
				for(Command c : cm) {
					stream.println(c + " | " + c + " " + c.usage + " | " + c.description + " | " + c.aliases);
				}
				return true;
			}
		});
		
		addCommand(new Command("quit", "", "Quits the entire simulator.", 0, 0, "exit", "q", "stop") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				getSingleton().keepRunning = false;
				return true;
			}
		});
		
		addCommand(new Command("power", "[on/off]", "Toggles the power on/off for the Chrono timer.", 0, 1, "pow") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				if(args.length == 0) {
					timer.togglePower();
					if(timer.isOn())
						stream.println("Chronotimer has powered on.");
					else
						stream.println("Chronotimer has powered off.");
					return true;
				} else if(args.length == 1) {
					if(args[0].equalsIgnoreCase("on")) {
						if(!timer.setPower(true))
							stream.println("Timer was already on.");
						else
							stream.println("Timer has turned on.");
						return true;
					} else if(args[0].equalsIgnoreCase("off")) {
						if(!timer.setPower(false))
							stream.println("Timer was already off.");
						else
							stream.println("Timer has turned off.");
						return true;
					}
				}
				return false;
			}
		});
		
		addCommand(new Command("on", "", "Turns the chrono timer on.", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				getSingleton().executeCommand("power on");
				return true;
			}
		});
		
		addCommand(new Command("off", "", "Turns the chrono timer off.", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				getSingleton().executeCommand("power off");
				return true;
			}
		});
		
		addCommand(new Command("time", "", "Sets the current time.", 1, 1, "settime") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				timer.getTimeManager().setTime(args[0]);
				System.out.println("Set time to " + args[0]);
				return true;
			}
		});
		
		addCommand(new Command("reset", "", "Resets the chrono timer back to initial state.", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				timer.reset();
				stream.println("Chronotimer has been reset.");
				return true;
			}
		});
		
		addCommand(new Command("num", "<number>", "Sets <number> as the next competetor to start.", 1, 1, "queue") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int number = Integer.parseInt(args[0]);
					if(timer.getLatestRun().addRacer(number) != null)
						stream.println("Successfully added racer " + number);
					else
						stream.println("Failed to add racer " + number);
				} catch(NumberFormatException e) {
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("clear", "<number>", "Clear <number> as the next competetor.", 1, 1, "clr", "dequeue") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int number = Integer.parseInt(args[0]);
					if(timer.getLatestRun().removeRacer(number))
						stream.println("Successfully removed racer " + number);
					else
						stream.println("Failed to remove racer " + number);
				} catch(NumberFormatException e) {
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("swap", "", "Swaps the front two racers positions in the current run.", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				timer.swap();
				return true;
			}
		});
		
		addCommand(new Command("dnf", "", "The next competetor to finish will not finish.", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				timer.doNotFinish();
				return true;
			}
		});
		
		addCommand(new Command("trigger", "<channel>", "Triggers a certain channel.", 1, 1, "trig") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int number = Integer.parseInt(args[0]);
					if(!timer.trigger(number))
						stream.println("Failed to trigger channel " + number);
					else
						stream.println("Successfully triggered channel " + number);
				} catch(NumberFormatException e) {
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("start", "", "Triggers the first channel.") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				return getSingleton().executeCommand("trigger 1");
			}
		});
		
		addCommand(new Command("finish", "", "Triggers the second channel.") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				return getSingleton().executeCommand("trigger 2");
			}
		});
		
		addCommand(new Command("toggle", "<channel>", "Connects a specific sensor to a specific channel.", 1, 1, "tog", "togg") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int c = Integer.parseInt(args[0]);
					if(c < 1 || c > ChronoTimer.MAXIMUM_CHANNELS) {
						stream.println("Invalid channel id, must be from 1-12");
					}
					Channel ch = timer.getChannels().get(c-1);
					if(!ch.toggle())
						stream.println("Failed to toggle channel " + ch.getID());
					else {
						if(ch.isEnabled())
							stream.println("Channel " + ch.getID() + " is now enabled.");
						else
							stream.println("Channel " + ch.getID() + " is now disabled.");
					}
				} catch(NumberFormatException e) {
					stream.println("Channel id must be a number.");
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("connect", "<sensor> <channel>", "Connects a specific sensor to a specific channel.", 2, 2, "conn") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				SensorType s = SensorType.valueOf(args[0].toUpperCase());
				if(s == null) {
					stream.println("No sensor called " + args[0]);
					return true;
				}
				try {
					int c = Integer.parseInt(args[1]);
					if(c < 0 || c > ChronoTimer.MAXIMUM_CHANNELS) {
						stream.println("Invalid channel id!");
					}
					if(timer.connect(s, c))
						stream.println("Connected " + s + " sensor to channel " + c);
					else
						stream.println("Failed to connect channel " + c);
				} catch(NumberFormatException e) {
					stream.println("Channel id must be a number.");
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("disconnect", "<channel>", "Disconnects a sensor from channel <channel>.", 1, 1, "disc") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int c = Integer.parseInt(args[0]);
					if(c < 0 || c > ChronoTimer.MAXIMUM_CHANNELS) {
						stream.println("Invalid channel id!");
					}
					if(!timer.disconnect(c))
						stream.println("Failed to disconnect channel " + c);
				} catch(NumberFormatException e) {
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("event", "<type>", "Sets the current run with the given event type.", 1, 1) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				EventType event;
				try {
					event = EventType.valueOf(args[0].toUpperCase());
				} catch(Exception e) {
					stream.println("No event type called " + args[0]);
					return true;
				}
				if(timer.setEvent(event))
					stream.println("Successfully set event to " + event);
				else
					stream.println("Failed to set event to " + event);
				return true;
			}
		});
		
		addCommand(new Command("newrun", "", "Creates a new run.(Must end a run first)", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				Run run = timer.newRun();
				if(run != null)
					stream.println("Successfully created new run " + run.getID());
				else
					stream.println("Failed to create new run!");
				return true;
			}
		});
		
		addCommand(new Command("endrun", "", "Done with the current run.", 0, 0) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				if(timer.endRun())
					stream.println("Ended run " + timer.getLatestRun().getID());
				else
					stream.println("Failed to end run!");
				return true;
			}
		});
		
		addCommand(new Command("print", "[run]", "Prints the given run.", 0, 1) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int rid;
					if(args.length == 1)
						rid = Integer.parseInt(args[0])-1;
					else
						rid = timer.getLatestRun().getID()-1;
					if(rid < 0 || rid >= timer.getRuns().size()) {
						stream.println("No run found.");
						return true;
					}
					Run run = timer.getRuns().get(rid);
					if(run == null) {
						stream.println("No run found with id of " + rid);
						return true;
					}
					stream.println("Run " + run.getID());
					stream.println("===== ID : START - FINISH =====");
					for(Record r : run.getRecords()) {
						if(r instanceof RacerRecord) {
							RacerRecord rec = (RacerRecord) r;
							stream.print("Racer " + rec.getRacer().getID() + ": ");
						} else if(r instanceof PlaceHolderRecord) {
							PlaceHolderRecord rec = (PlaceHolderRecord) r;
							stream.print("PlaceHolder " + rec.getPlaceHolder() + ": ");
						}
						if(r.didNotFinish()) {
							stream.println(timer.getTimeManager().formatTime(r.getStartTime()) + " - DNF");
						} else {
							stream.println(timer.getTimeManager().formatTime(r.getStartTime()) + " - " + timer.getTimeManager().formatTime(r.getFinishTime()));
						}
					}
				} catch(NumberFormatException e) {
					stream.println("Must enter valid run number.");
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("export", "<run>", "Exports the given run.", 1, 1) {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				try {
					int number = Integer.parseInt(args[0]);
					if(number < 1 || number > timer.getRuns().size()) {
						stream.println("No run found with that id.");
						return true;
					}
					timer.getExporter().export((TRun) timer.getRuns().get(number-1));
				} catch(NumberFormatException e) {
					return false;
				}
				return true;
			}
		});
		
		addCommand(new Command("execute", "<file path>", "Executes file.", 1, 1, "exec") {
			public boolean execute(PrintStream stream, ChronoTimer timer, String[] args) {
				getSingleton().executeFile(args[0]);
				return true;
			}
		});
		
	}
	
}
