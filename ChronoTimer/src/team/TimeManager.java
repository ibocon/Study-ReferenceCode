package team;

import java.util.concurrent.TimeUnit;


public class TimeManager {
	
	private long epoch;
	
	public final long getEpoch() {
		return epoch;
	}
	
	public void setEpoch(long e) {
		epoch = e;
	}
	
	/**
	 * Get input from user. "TIME hour:min:sec" string format.
	 * Set system_time from 'time' to 'system_time'.
	 * @param time
	 */
	public void setTime(String time){
		String format[] = time.split(":");
		if(time.contains("."))
			format[2] = format[2].substring(0,format[2].indexOf("."));
		epoch = System.currentTimeMillis() - intoMillisecs(format);
	}
	
	/**
	 * Converts a string into the number of milliseconds that string represents
	 * @param unformatted - an unformatted string to be converted into milliseconds
	 * @return a long containing the number of milliseconds that unformatted represents
	 */
	public long intoMillisecs(String unformatted) {
		String[] s = unformatted.split(":");
		s[s.length-1] = s[s.length-1].substring(0, s[s.length-1].indexOf("."));
		return intoMillisecs(s);
	}
	
	/**
	 * Converts a string into the number of milliseconds that string represents
	 * @param unformatted - an unformatted string to be converted into milliseconds
	 * @return a long containing the number of milliseconds that unformatted represents
	 */
	public long intoMillisecs(String[] unformatted) {
		long hrs, mins, secs;

		hrs = TimeUnit.MILLISECONDS.convert(Long.parseLong(unformatted[0]), TimeUnit.HOURS);
		mins = TimeUnit.MILLISECONDS.convert(Long.parseLong(unformatted[1]), TimeUnit.MINUTES);
		secs = TimeUnit.MILLISECONDS.convert(Long.parseLong(unformatted[2]), TimeUnit.SECONDS);
		return (hrs + mins + secs);
	}
	
	/**
	 * Returns the internal system time - the time we set (epoch)
	 * @return internal system time - the time we set (epoch)
	 */
	public long getTime(){
		return subtractTime(System.currentTimeMillis(),epoch);
	}
	
	/**
	 * get absolute time and return into "hour:min:sec" string format.
	 * @param input - epoch
	 * @return string formatted as "hour:min:sec"
	 */
	public String formatTime(long input) {
		if(input < 0) return "NOT RECORDED";
		return String.format("%d:%s:%s.%d",
					TimeUnit.MILLISECONDS.toHours(input),
					String.format( "%02d",TimeUnit.MILLISECONDS.toMinutes(input) - TimeUnit.HOURS.toMinutes(TimeUnit.MILLISECONDS.toHours(input))),
					String.format( "%02d",TimeUnit.MILLISECONDS.toSeconds(input) - TimeUnit.MINUTES.toSeconds(TimeUnit.MILLISECONDS.toMinutes(input))),
					TimeUnit.MILLISECONDS.toMillis(input) - TimeUnit.SECONDS.toMillis(TimeUnit.MILLISECONDS.toSeconds(input)));
	}
	
	/**
	 * Return the difference between the two supplied times
	 * @param first - time to subtract from
	 * @param second - time to subtract
	 * @return difference between the two times
	 */
	public long subtractTime(long first, long second){
		return first-second;
	}

}
