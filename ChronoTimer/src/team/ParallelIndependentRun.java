package team;

import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

public class ParallelIndependentRun extends TRun {
	
	protected final LinkedList<TRacer> allRacers;
	protected final LinkedList<TRacer> toStart, ended;
	public final List<Racer> racers;
	
	protected final LinkedList<TRacer> track1S, track1E, track2S, track2E;
	
	private boolean started;
	private LinkedList<TRacer> lastStarted = new LinkedList<TRacer>();
	
	@Override
	public boolean safeAddRacer(TRacer r) {
		allRacers.addFirst(r);
		toStart.addFirst(r);
		return true;
	}

	@Override
	public boolean endRun() {
		if(!timer.isOn() || finished) return false;
		finished = true;
		for(TRecord r : records.values()) {
			r.ended = true;
		}
		toStart.clear();
		ended.clear();
		return true;
	}

	@Override
	public boolean doNotFinish() {
		if(!timer.isOn() || finished || getRacers().isEmpty()) return false;
		TRacer r;
		if(lastStarted.isEmpty()) {
			if(ended.size() == allRacers.size()) return false;
			r = allRacers.get(ended.size());
			track1E.remove(r);
			track2E.remove(r);
		} else {
			r = lastStarted.pop();
			track1E.remove(r);
			track2E.remove(r);
		}
		records.get(r.id).ended = true;
		ended.addFirst(r);
		return true;
	}
	
	@Override
	public boolean hasStarted() {
		if(finished) return true;
		return started;
	}
	
	protected TRacer safeRemoveRacer(int target) {
		Iterator<TRacer> it = allRacers.iterator();
		while(it.hasNext()) {
			TRacer ir = it.next();
			if(ir.id == target) {
				it.remove();
				track1S.remove(ir);
				track2S.remove(ir);
				return ir;
			}
		}
		return null;
	}

	@Override
	public List<Racer> getRacers() {
		return racers;
	}

	@Override
	public Racer getLast() {
		return allRacers.get(allRacers.size()-1);
	}
	
	private void start() {
		
		if(started) return;
		started = true;
		if(toStart.size() == 0) return;
		int firstHalf = toStart.size()/2;
		int secondHalf = toStart.size() - firstHalf;
		for(int e = 0; e<firstHalf; ++e) {
			track1S.addLast(toStart.removeLast());
		}
		for(int i = 0; i< secondHalf; i++)
		{
			track2S.addLast(toStart.removeLast());
		}
		
	}
	
	@Override
	public boolean safeTrigger(Channel c) {
		long time = timer.getTimeManager().getTime();
		TRacer r = null;
//		LinkedList<TRacer> track = getTrackFromChannel(c.getID());
		switch (c.getID()) {
			case 1:
				start();
				r = track1S.poll();
				if(r == null) break;
				track1E.add(r);
			case 3:
				start();
				if(r == null) {
					r = track2S.poll();
					if(r == null) break;
					track2E.add(r);
				}
				lastStarted.addLast(r);
				records.get(r.id).start = time;
//				track.add(r);
				break;
			case 2:
				r = track1E.poll();
				if(r == null) break;
			case 4:
				if(r == null) r = track2E.poll();
				if(r == null) break;
				TRecord rec = records.get(r.id);
				rec.finish = time;
				rec.ended = true;
				lastStarted.remove(r);
				ended.add(r);
				break;
		}
		return true;
	}
	
	private LinkedList<TRacer> getTrackFromChannel(int c) {
		switch(c) {
			case 1:
				return track1S;
			case 2:
				return track1E;
			case 3:
				return track2S;
			case 4:
				return track2E;
		}
		return null;
	}
	
	@Override
	public EventType getEventType() {
		return EventType.PARIND;
	}
	
	public ParallelIndependentRun(TChronoTimer timer, int id) {
		super(timer, id);
		allRacers = new LinkedList<TRacer>();
		toStart = new LinkedList<TRacer>();
		ended = new LinkedList<TRacer>();
		track1S = new LinkedList<TRacer>();
		track1E = new LinkedList<TRacer>();
		track2S = new LinkedList<TRacer>();
		track2E = new LinkedList<TRacer>();
		racers = Collections.unmodifiableList(allRacers);
	}
	
	public ParallelIndependentRun(TRun old) {
		super(old);
		allRacers = new LinkedList<TRacer>();
		toStart = new LinkedList<TRacer>();
		ended = new LinkedList<TRacer>();
		track1S = new LinkedList<TRacer>();
		track1E = new LinkedList<TRacer>();
		track2S = new LinkedList<TRacer>();
		track2E = new LinkedList<TRacer>();
		racers = Collections.unmodifiableList(allRacers);
		for(TRecord r : old.getRecords()) {
			if(r instanceof TRacerRecord){
				TRacerRecord rec = (TRacerRecord) r;
				allRacers.add(rec.racer);
				toStart.add(rec.racer);
			}
		}
	}
	
}
