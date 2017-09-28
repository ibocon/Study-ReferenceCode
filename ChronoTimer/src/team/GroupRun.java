package team;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

public class GroupRun extends TRun {
	
	protected final List<TRacer> racers = new ArrayList<TRacer>();
	public final List<TRacer> safeRacers;
	protected int ind, ph = 1;
	
	protected long startTime = -1;
	
	public GroupRun(TRun run) {
		super(run);
		for(TRecord r : recordList) {
			if(!(r instanceof TRacerRecord)) continue;
			TRacerRecord rec = (TRacerRecord) r;
			racers.add(rec.racer);
		}
		safeRacers = Collections.unmodifiableList(racers);
	}

	public GroupRun(TChronoTimer timer, int id) {
		super(timer, id);
		safeRacers = Collections.unmodifiableList(racers);

	}

	@Override
	public EventType getEventType() {
		return EventType.GRP;
	}

	@Override
	public boolean hasStarted() {
		return startTime != -1;
	}

	@Override
	public boolean endRun() {
		for(int i = ind; i < racers.size(); i++){
			records.get(racers.get(i).id).ended = true;
		}
		finished = true;
		return true;
	}

	@Override
	public boolean doNotFinish() {
		if(finished || racers.isEmpty()) return false;
		getRecord(ind++).ended = true;
		return true;
	}

	@Override
	protected TRacer safeRemoveRacer(int target) {
		Iterator<TRacer> it = racers.iterator();
		while(it.hasNext()) {
			TRacer r = it.next();
			if(r.id == target) {
				it.remove();
				return r;
			}
		}
		return null;
	}
	
	@Override
	public TRacer addRacer(int id) {
		if(!timer.isOn() || finished || records.containsKey(id)) return null;
		TRacer racer = timer.getRacer(id);
		TRecord rec = null;
		int index = 0;
		for(Record r : recordList) {
			if(r instanceof TPlaceHolderRecord) {
				TPlaceHolderRecord pr = (TPlaceHolderRecord) r;
				recordList.remove(index);
				rec = new TRacerRecord(this,racer);
				rec.ended = pr.ended;
				rec.start = pr.start;
				rec.finish = pr.finish;
				recordList.add(index, rec);
				break;
			}
			++index;
		}
		if(rec == null && !hasStarted()) {
			rec = new TRacerRecord(this,racer);
			recordList.addFirst(rec);
		}
		if(rec == null) return null;
		racers.add(0,racer);
		records.put(id, rec);
		racer.records.put(this.id, rec);
		timer.wExporter.export(this);
		return racer;
	}
	
	@Override
	protected boolean safeAddRacer(TRacer r) {
		return true;
	}

	@Override
	public List<TRacer> getRacers() {
		return safeRacers;
	}

	@Override
	public TRacer getLast() {
		if (racers.isEmpty()) return null;
		return racers.get(racers.size()-1);
	}

	@Override
	protected boolean safeTrigger(Channel c) {
		long time = timer.getTimeManager().getTime();
		int id = c.getID();
		if(id == 1){
			if(hasStarted()) return false;
			startTime = time;
			for(TRecord r : records.values()){
				r.start = startTime;
			}
			return true;
		}
		else if(id == 2 && hasStarted()){
			TRecord record;
			if(ind >= recordList.size()) {
				record = new TPlaceHolderRecord(this,ph++);
				record.start = startTime;
				recordList.addLast(record);
				++ind;
			}
			else
				record = recordList.get(ind++);
			record.finish = time;
			record.ended = true;
			return true;
		}
		return false;
	}

}
