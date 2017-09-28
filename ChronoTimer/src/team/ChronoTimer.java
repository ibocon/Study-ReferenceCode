package team;
import java.util.List;
import java.util.Map;


public interface ChronoTimer {
	
	/**
	 * The maximum amount of channels for any ChronoTimer.
	 */
	public static final int MAXIMUM_CHANNELS = 8;
	
	/**
	 * 
	 * @return True if the ChronoTimer is on, false if it is off.
	 */
	public boolean isOn();
	
	/**
	 * Sets the power state of the chrono timer.
	 * @param pow - The power state the chrono timer should have.
	 * @return True if the state was changed, false if it was already set to given state.
	 */
	public boolean setPower(boolean on);
	
	/**
	 * Turns the ChronoTimer on.
	 * @return True if the ChronoTimer turned on, false if it was already on.
	 */
	public default boolean powerOn() {
		return setPower(true);
	}
	
	/**
	 * Turns the ChronoTimer off.
	 * @return True if the ChronoTimer turned off, false if it was already off.
	 */
	public default boolean powerOff() {
		return setPower(false);
	}
	
	public TimeManager getTimeManager();
	
	/**
	 * Toggles the power of the chrono timer.
	 * @return True if operation was sucessful.
	 */
	public default boolean togglePower() {
		if(isOn())
			setPower(false);
		else
			setPower(true);
		return true;
	}
	
	/**
	 * @return A map of all the racers associated with this ChronoTimer mapped using their id.
	 */
	public Map<Integer,Racer> getRacers();
	
	/**
	 * Determines if a racer exists currently in this ChronoTimer.
	 * @param id - The id of the racer.
	 * @return True if the racer has been create, otherwise false.
	 */
	public default boolean racerExists(int id) {
		return getRacers().containsKey(id);
	}
	
	/**
	 * Sets the type of event the current run will be.
	 */
	public boolean setEvent(EventType event);
	
	/**
	 * Returns list of all the channels that are associated with this ChronoTimer.
	 * @return The list of all channels.
	 */
	public List<Channel> getChannels();
	
	public default Channel getChannel(int id) {
		if(id < 1 || id > MAXIMUM_CHANNELS) return null;
		return getChannels().get(id-1);
	}
	
	/**
	 * Returns list of all of the runs that are associated with this ChronoTimer.
	 * @return The list of all the runs.
	 */
	public List<Run> getRuns();
	
	/**
	 * Returns the latest run.
	 */
	public Run getLatestRun();
	
	public Run newRun();
	public boolean endRun();
	
	public boolean reset();
	
	/**
	 * Triggers a specific Channel.
	 * @return True if the channel was triggered successfully.
	 */
	public boolean trigger(Channel c);
	
	/**
	 * Triggers a specific Channel.
	 * @return True if the channel was triggered successfully.
	 */
	public default boolean trigger(int c) {
		if(c < 1 || c > MAXIMUM_CHANNELS) return false;
		return trigger(getChannels().get(c-1));
	}
	
	/**
	 * Connects a specific sensor to an specific channel.
	 * @param s - The sensor to connect.
	 * @param c - The channel that the sensor should be connected to.
	 * @return True if the sensor was connected successfully.
	 */
	public default boolean connect(SensorType s, Channel c) {
		if(!isOn() || c == null) return false;
		if(s == null) s = SensorType.NONE;
		c.setEnabled(false);
		c.setSensorType(s);
		return true;
	}
	
	/**
	 * Connects a specific sensor to an specific channel.
	 * @param s - The sensor to connect.
	 * @param c - The channel that the sensor should be connected to.
	 * @return True if the sensor was connected successfully.
	 */
	public default boolean connect(SensorType s, int c) {
		if(c < 1 || c > MAXIMUM_CHANNELS) return false;
		return connect(s,getChannels().get(c-1));
	}
	
	/**
	 * Disconnects a specific sensor to an specific channel.
	 * @param c - The channel that should be disconnected.
	 * @return True if the sensor was connected successfully.
	 */
	public default boolean disconnect(Channel c) {
		if(!isOn() || c == null) return false;
		c.disable();
		c.setSensorType(SensorType.NONE);
		return true;
	}
	
	/**
	 * Disconnects a specific sensor to an specific channel.
	 * @param c - The channel that should be disconnected.
	 * @return True if the sensor was connected successfully.
	 */
	public default boolean disconnect(int c) {
		if(c < 1 || c > MAXIMUM_CHANNELS) return false;
		return disconnect(getChannels().get(c-1));
	}
	
	/**
	 * Sets the next competitor that would finish to not finish and prematurely exit the run.
	 * @return True if the operation was successful.
	 */
	public boolean doNotFinish();
	
	/**
	 * Swaps the next two competitors to finish.
	 * @return True if the operation was successful.
	 */
	public boolean swap();
	
	/**
	 * @return The default exporter used by this chronotimer.
	 */
	public Exporter getExporter();
	
}
