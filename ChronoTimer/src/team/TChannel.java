package team;

public class TChannel implements Channel, Runnable {
	
	/**
	 * The channels id.
	 */
	public final int id;
	
	/**
	 * The ChronoTimer that this channel is owned by.
	 */
	public final TChronoTimer timer;
	
	/**
	 * The sensor that is attached to this channel.
	 */
	protected SensorType t;
	
	/**
	 * The sensors state, if it is enabled or not.
	 */
	private volatile boolean enabled;
	
	protected Thread thread = new Thread(this);
	
	/**
	 * @param timer - TCronoTimer object
	 * @param id - this channel's ID
	 */
	public TChannel(TChronoTimer timer, int id) {
		this.id = id;
		this.timer = timer;
		t = SensorType.NONE;
		enabled = false;
	}
	
	/**
	 * @param timer - TChronoTimer object
	 * @param id - This channel's ID
	 * @param enabled - boolean value indicating if the channel is enabled or not
	 */
	public TChannel(TChronoTimer timer, int id, boolean enabled) {
		this.id = id;
		this.timer = timer;
		this.enabled = enabled;
	}
	/**
	 * @param timer - TChronoTimer object
	 * @param id - This channel's ID
	 * @param t - initial SensorType connected to this channel
	 */
	public TChannel(TChronoTimer timer, int id, SensorType t) {
		this.id = id;
		this.timer = timer;
		this.t = t;
		enabled = false;
	}
	/**
	 * @param timer - TChronoTimer object
	 * @param id - This channel's ID
	 * @param t - initial SensorType connected to this channel
	 * @param enabled - boolean value indicating if the channel is enabled or not
	 */
	public TChannel(TChronoTimer timer, int id, SensorType t, boolean enabled) {
		this.id = id;
		this.timer = timer;
		this.t = t;
		this.enabled = enabled;
	}
	
	
	
	/**
	 * Returns true if the channel is enabled.
	 * @return true if channel is enabled. False if channel is disabled.
	 */
	public boolean isEnabled(){
		return enabled;
	}
	
	/**
	 * Toggle the channel enable state from on -> off or off -> on
	 * @return true after toggling
	 */
	public boolean toggle() {
		enabled = !enabled;
		if(enabled == false) thread.interrupt();
		else {
			thread =new Thread(this);
			thread.start();
		}
		return true;
	}

	/**
	 * Sends a trigger signal from this channel to the ChronoTimer timer
	 * @return the result of the call to timer.trigger. Returns true if the trigger is valid, false if not.
	 */
	public boolean trigger() {
		if(!timer.isOn() || !isEnabled() || getSensorType().equals(SensorType.NONE)) return false;
		TRun latestRun = timer.getLatestRun();
		synchronized (this) {
			notify();
		}
		return true;
	}
	
	/**
	 * Gets the channel ID
	 * @return channel ID
	 */
	@Override
	public int getID() {
		return id;
	}
	
	/**
	 * Gets the sensor type connected to this channel
	 * @return Type of sensor connected to this channel
	 */
	@Override
	public SensorType getSensorType() {
		return t;
	}

	/**
	 * Set the enable state of the current channel to the value specified in the parameter e
	 * @param e - new enable state for the channel
	 * @return true if the channel enable state has changed, false otherwise
	 */
	@Override
	public boolean setEnabled(boolean e) {
		if (!timer.isOn()) return false;
		boolean old = enabled;
		enabled = e;
		if(enabled == false) thread.interrupt();
		else {
			thread =new Thread(this);
			thread.start();
		}
		return old != enabled;
	}

	/**
	 * If the chronoTimer is on, set the type of the sensor connected to this channel to s
	 * @param s - sensor type to set the sensor of the current channel to
	 */
	@Override
	public void setSensorType(SensorType s) {
		if (!timer.isOn()) return;
		if(s == null) s = SensorType.NONE;
		t = s;
	}
	
	@Override
	public void run() {
		while(true) {
			try {
				synchronized(this) {
					wait();
				}
				synchronized(timer) {
					TRun run = timer.getLatestRun();
					run.trigger(this);
				}
			} catch (InterruptedException e) {
				break;
			}
		}
	}
	
}
