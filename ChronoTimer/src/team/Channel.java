package team;

public interface Channel {
	
	/**
	 * @return The channels id.
	 */
	public int getID();
	
	/**
	 * @return The sensor that is attached to the channel.
	 */
	public SensorType getSensorType();
	
	/**
	 * Attaches a sensor to the channel.
	 * @param s - The type of sensor to attach.
	 */
	public void setSensorType(SensorType s);
	
	
	/**
	 * @return True if the channel is enabled.
	 */
	public boolean isEnabled();
	
	/**
	 * Sets the channels enabled state.
	 * @param e - The state the channel should be in.
	 * @return True if the channels state was changed.
	 */
	public boolean setEnabled(boolean e);
	
	/**
	 * Enables the channel.
	 * @return True if the channel was enabled, false if it was already enabled.
	 */
	public default boolean enable() {
		return setEnabled(true);
	}
	
	/**
	 * Disables the channel.
	 * @return True if the channel was disabled, false if it was already disabled.
	 */
	public default boolean disable() {
		return setEnabled(false);
	}
	
	/**
	 * Toggles the channel.
	 * If it is enabled it will become disabled.
	 * If it is disabled it will become enabled.
	 * @return True if the operation was successful.
	 */
	public default boolean toggle() {
		if(isEnabled())
			return disable();
		else
			return enable();
	}
	
	/**
	 * Triggers this channel.
	 * @return True if the channel was triggered successfully.
	 */
	public boolean trigger();
	
}
