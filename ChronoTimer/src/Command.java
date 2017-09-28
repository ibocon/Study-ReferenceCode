import java.io.PrintStream;
import java.util.Collections;
import java.util.HashSet;
import java.util.Set;

import team.ChronoTimer;


public abstract class Command {
	
	public final String name, usage, description;
	public final Set<String> aliases;
	public final int minimum_args, maximum_args;
	
	/**
	 * Executes the command with the given stream, timer and arguments.
	 * @param stream - The stream to send output to.
	 * @param timer - The chronotimer to use.
	 * @param args - The list of arguments thrown with the command.
	 * @return true if the command had proper arguments, false if arguments were improper and could not execute.
	 */
	public abstract boolean execute(PrintStream stream, ChronoTimer timer, String[] args);
	
	public boolean run(PrintStream stream, ChronoTimer timer, String[] args) {
		boolean result;
		if(args.length > maximum_args || args.length < minimum_args || !(result = execute(stream, timer, args))) {
			stream.println("Wrong syntax, " + args.length + " " + this + " " + usage);
			return false;
		}
		return result;
	}
	
	public String toString() {
		return name;
	}
	
	public Command(String name, String usage, String description) {
		this.name = name.toLowerCase();
		this.usage = usage;
		this.description = description;
		minimum_args = 0;
		maximum_args = Integer.MAX_VALUE;
		aliases = Collections.unmodifiableSet(new HashSet<String>());
	}
	
	public Command(String name, String usage, String description, int min, int max) {
		this.name = name.toLowerCase();
		this.usage = usage;
		this.description = description;
		minimum_args = min;
		maximum_args = max;
		aliases = Collections.unmodifiableSet(new HashSet<String>());
	}
	
	public Command(String name, String usage, String description, String... a) {
		this.name = name.toLowerCase();
		this.usage = usage;
		this.description = description;
		minimum_args = 0;
		maximum_args = Integer.MAX_VALUE;
		
		Set<String> na = new HashSet<String>();
		for (String s : a) {
			na.add(s.toLowerCase());
		}
		aliases = Collections.unmodifiableSet(na);
	}
	
	public Command(String name, String usage, String description, int min, int max, String... a) {
		this.name = name.toLowerCase();
		this.usage = usage;
		this.description = description;
		minimum_args = min;
		maximum_args = max;
		
		Set<String> na = new HashSet<String>();
		for (String s : a)
			na.add(s.toLowerCase());
		aliases = Collections.unmodifiableSet(na);
	}
	
}
