package team;

import java.util.Map;

public interface Racer {
	
	/**
	 * @return The racers ID.
	 */
	public int getID();
	
	public Map<Integer,? extends Record> getRecords();
	
	
}
