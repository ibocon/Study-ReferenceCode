package team;

import java.util.Collections;
import java.util.HashMap;
import java.util.Map;

public class TRacer implements Racer {
	
	public final int id;
	protected Map<Integer,TRecord> records = new HashMap<Integer,TRecord>(), safe_records;

	@Override
	public int getID() {
		return id;
	}
	
	public String toString(){
		return "Racer " + id;
	}
	
	@Override
	public Map<Integer,TRecord> getRecords() {
		return safe_records;
	}
	
	@Override
	public boolean equals(Object o) {
		if(o instanceof TRacer)
			return id == ((TRacer)o).id;
		if(o instanceof Integer)
			return id == (Integer)o;
		return false;
	}
	
	public TRacer(int id){
		this.id=id;
		safe_records = Collections.unmodifiableMap(records);
	}
	
}
