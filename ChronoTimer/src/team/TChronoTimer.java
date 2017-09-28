package team;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Map;
import java.util.concurrent.ConcurrentHashMap;

public class TChronoTimer implements ChronoTimer {
	
	/**
	 * The current power state of the ChronoTimer.
	 */
	private volatile boolean power;
	/**
	 * Array holding all the channels that belong to this ChonoTimer.
	 */
	private final TChannel[] channels;
	/**
	 * List containing all safe channels
	 */
	private final List<Channel> safe_channels;
	/**
	 * List containing all the runs.
	 */
	private final List<TRun> runs;
	/**
	 * List containing all safe runs
	 */
	private final List<Run> safe_runs;
	/**
	 * The index of the current/latest run.
	 */
	private int current_run;
	
	/**
	 * A map that stores all racers that are associated with this ChronoTimer.
	 */
	protected final Map<Integer,TRacer> racers = new ConcurrentHashMap<Integer,TRacer>();
	/**
	 * A non-modifiable view of all the racers.
	 */
	protected final Map<Integer,Racer> safe_racers;
	
	protected final JSONExporter exporter = new JSONExporter();
	
	protected final WebExporter wExporter = new WebExporter("localhost", 8000, exporter);
	
	protected final TimeManager timeManager = new TimeManager();
	
	public Map<Integer,Racer> getRacers() {
		return safe_racers;
	}
	
	public final TRacer getRacer(int id) {
		TRacer r = racers.get(id);
		if(r == null) {
			r = new TRacer(id);
			racers.put(id, r);
		}
		return r;
	}
	
	/**
	 * Checks power status of the ChronoTimer
	 * @return The current status of power
	 */
	synchronized public boolean isOn() {
		return power;
	}
	
	/**
	 * Toggles the power of the ChonoTimer
	 * @return True if power was toggled
	 */
	synchronized public boolean togglePower(){
		power = !power;
		return true;
	}
	/**
	 * @param New power value
	 * @return True if power status has changed
	 */
	synchronized public boolean setPower(boolean pow) {
		boolean result = power != pow;
		power = pow;
		return result;
	}
	
	/**
	 * Resets the ChronoTimer to default settings and clears all runs.
	 */
	synchronized public boolean reset() {
		for(int i=0; i<ChronoTimer.MAXIMUM_CHANNELS; i++) {
			channels[i].setSensorType(SensorType.NONE);
			channels[i].disable();
		}
		racers.clear();
		runs.clear();
		runs.add(TRun.getDefaultRun(this, 1));
		current_run = 0;
		return true;
	}
	
	/**
	 * Toggles the given channel.
	 * @param ch - The channel.
	 * @return True if the operation was successful.
	 */
	synchronized public boolean toggleChannel(int ch){
		if(!power || ch < 1 || ch > channels.length-1) return false;
		return channels[ch-1].toggle();
	}
	
	/**
	 * Creates a new run.
	 * @return True if operation was successful, otherwise false if the chrono timer is off or the current race has not ended.
	 */
	synchronized public Run newRun(){
		if(!power || !runs.isEmpty() && !runs.get(current_run).finished) return null;
		TRun result = TRun.getDefaultRun(this, ++current_run + 1);
		runs.add(result);
		wExporter.export(result);
		return result;
	}
	
	synchronized public boolean swap(){
		if(!getLatestRun().getEventType().equals(EventType.IND)) return false;
		IndependentRun run = (IndependentRun) getLatestRun();
		boolean r = run.swap();
		wExporter.export(run);
		return r;
	}
	
	/**
	 * @return A list of channels used by the ChonoTimer
	 */
	@Override
	public List<Channel> getChannels() {
		return safe_channels;
	}
	
	/**
	 * @return All runs in use by the ChonoTimer
	 */
	@Override
	public List<Run> getRuns() {
		return safe_runs;
	}
	/**
	 * Triggers or starts the the latest run on the ChonoTimer.
	 * @param channel to trigger
	 * @return true if successfully triggered
	 * @return false if either Run's start or end queues are empty
	 */
	@Override
	public boolean trigger(Channel c) {
		return c.trigger();
	}
	
	/**
	 * Sets the current event type for the latest run.
	 * @param New EventType
	 * @return True if the run's event was changed
	 * @return False for no event
	 */
	@Override
	synchronized public boolean setEvent(EventType event) {
		if(!power || event == null) return false;
		if(getLatestRun().hasStarted()) return false;
		TRun newRun = event.getNewInstanceFromOld(getLatestRun());
		if(newRun == null) return false;
		runs.set(current_run, newRun);
		return true;
	}
	/**
	 * @return The current/latest run on this ChonoTimer
	 */
	@Override
	public TRun getLatestRun() {
		return runs.get(current_run);
	}
	
	/**
	 * Create a new TChonoTimer with parameters set to default.
	 * This automatically instantiates a new run for this
	 * TChonoTimer instance, and by default the TChonoTimer's power is off.
	 * @return A new TChonoTimer instance
	 */
	public TChronoTimer() {
		power = false;
		
		timeManager.setTime("00:00:00");
		channels = new TChannel[ChronoTimer.MAXIMUM_CHANNELS];
		safe_channels = Collections.unmodifiableList(Arrays.asList(channels));
		for(int i=0; i<ChronoTimer.MAXIMUM_CHANNELS; i++)
			channels[i] = new TChannel(this,i+1);
		
		runs = new ArrayList<TRun>();
		safe_runs = Collections.unmodifiableList(runs);
		safe_racers = Collections.unmodifiableMap(racers);
		runs.add(TRun.getDefaultRun(this, 1)); // Always starts at run 1 by default.
	}

	@Override
	synchronized public boolean endRun() {
		boolean r = getLatestRun().endRun();
		wExporter.export(getLatestRun());
		return r;
	}

	@Override
	synchronized public boolean doNotFinish() {
		boolean r = getLatestRun().doNotFinish();
		wExporter.export(getLatestRun());
		return r;
	}
	
	@Override
	public JSONExporter getExporter() {
		return exporter;
	}
	
	public TimeManager getTimeManager() {
		return timeManager;
	}
	
}
