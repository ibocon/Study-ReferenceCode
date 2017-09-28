package team;

import java.util.Collections;
import java.util.HashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;

public abstract class TRun implements Run {
	
	protected final TChronoTimer timer;
	protected final int id;
	protected final Map<Integer,TRecord> records = new HashMap<Integer,TRecord>();
	protected final LinkedList<TRecord> recordList = new LinkedList<TRecord>();
	protected final Map<Integer,TRecord> safe_records;
	
	protected boolean finished;
	
	/**
	 * Removes the racer from the run. This method is so that the specific Run instance can remove their specific fields of this race.
	 * The rest of the fields that are associated with this racer are removed from removeRacer();
	 * @param target - The racer that should be removed.
	 * @return The racer that was removed.
	 */
	protected abstract TRacer safeRemoveRacer(int target);
	
	public TRecord getRecord(Racer r) {
		return records.get(r.getID());
	}
	
	public TRecord getRecord(int id){
		return records.get(id);
	}
	
	/**
	 * Removes the racer with the given id from the run.
	 */
	public boolean removeRacer(int target) {
		if(!timer.isOn() || hasStarted()) return false;
		TRacer r = safeRemoveRacer(target);
		if(r == null) return false;
		records.remove(r.id);
		r.records.remove(this.id);
		for(TRecord rec : recordList) {
			if(rec instanceof TRacerRecord) {
				TRacerRecord rRec = (TRacerRecord) rec;
				if(rRec.getRacer().equals(r)){
					recordList.remove(rec);
					break;
				}
			}
		}
		return true;
	}
	
	protected abstract boolean safeAddRacer(TRacer r);
	
	public TRacer addRacer(int id) {
		if(!timer.isOn() || finished || records.containsKey(id)) return null;
		TRacer racer = timer.getRacer(id);
		if(!safeAddRacer(racer)) return null;
		TRecord rec = new TRacerRecord(this,racer);
		records.put(id, rec);
		recordList.addFirst(rec);
		racer.records.put(this.id, rec);
		timer.wExporter.export(this);
		return racer;
	}

	public abstract List<? extends Racer> getRacers();

	@Override
	public boolean isFinished() {
		return finished;
	}
	
	public List<TRecord> getRecords() {
		return recordList;
	}
	
	public abstract Racer getLast();

	@Override
	public int getID() {
		return id;
	}
	
	public boolean trigger(Channel c) {
		synchronized(timer) {
			safeTrigger(c);
			timer.wExporter.export(this);
			return true;
		}
	}
	
	protected abstract boolean safeTrigger(Channel c);
	
	public TRun(TChronoTimer timer,int id) {
		this.timer = timer;
		this.id = id;
		finished=false;
		safe_records = Collections.unmodifiableMap(records);
	}
	
	public TRun(TRun other) {
		timer = other.timer;
		id = other.id;
		finished=false;
		records.putAll(other.records);
		for(TRecord r : records.values()) {
			r.run = this;
		}
		safe_records = Collections.unmodifiableMap(records);
		recordList.addAll(other.recordList);
	}
	
	public static final TRun getDefaultRun(TChronoTimer timer, int id) {
		return new IndependentRun(timer, id);
	}
	
}
