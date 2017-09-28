package team;

public interface Record {
	
	/**
	 * @return The run that this record is associated with.
	 */
	public Run getRun();
	
//	/**
//	 * @return The racer that this record is associated with.
//	 */
//	public Racer getRacer();
//	
	/**
	 * @return The racers start time, will return -1 if they do not have a start time recorded.
	 */
	public long getStartTime();
	
	/**
	 * @return The racers finish time, will return -1 if they do not have a finish time recorded.
	 */
	public long getFinishTime();
	
	public boolean isFinished();
	
	/**
	 * @return True if the racer did not finish(DNF).
	 */
	public default boolean didNotFinish() {
		return isFinished() && getFinishTime() == -1;
	}
	
	/**
	 * @return The elapsed time it took the racer to complete the run.
	 * Will return -1 if they did not finish or do not have both a start and finish time recorded.
	 */
	public default long getElapsedTime() {
		if(didNotFinish() || getStartTime() == -1 || getFinishTime() == -1) return -1;
		return getFinishTime() - getStartTime();
	}
	
}
