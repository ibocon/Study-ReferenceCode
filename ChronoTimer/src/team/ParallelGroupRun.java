package team;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;

public class ParallelGroupRun extends TRun {

	protected long start = -1;
	protected int emptyLane = 0;
	protected List<TRacer> racers = new ArrayList<TRacer>(ChronoTimer.MAXIMUM_CHANNELS);
	public final List<TRacer> safeRacers;
	protected TRacer[] lanes = new TRacer[ChronoTimer.MAXIMUM_CHANNELS];
	
	public ParallelGroupRun(TRun run) {
		super(run);
		if(run.recordList.size() > ChronoTimer.MAXIMUM_CHANNELS){
			throw new UnsupportedOperationException("Not enough lanes for all racers.");
		}
		int i = 0;
		int racersSize = run.getRacers().size();
		for(Racer r: run.getRacers()){
			TRacer tr = (TRacer)r;
			racers.add(tr);
			lanes[(racersSize-1) - i++] = tr;
		}
		emptyLane = i;
		safeRacers = Collections.unmodifiableList(racers);
	}
	
	public ParallelGroupRun(TChronoTimer timer, int id) {
		super(timer, id);
		safeRacers = Collections.unmodifiableList(racers);
	}

	@Override
	public EventType getEventType() {
		return EventType.PARGRP;
	}

	@Override
	public boolean hasStarted() {
		return start > -1;
	}

	@Override
	public boolean endRun() {
		for(int i = 0; i < lanes.length; i++){
			if(lanes[i] != null) records.get(lanes[i].id).ended = true;
		}
		finished = true;
		return true;
	}

	@Override
	public boolean doNotFinish() {
		for(int i = 0; i < lanes.length; i++){
			if(lanes[i] != null){ 
				records.get(lanes[i].id).ended = true;
				return true;
			}
		}
		return false;
	}

	@Override
	protected TRacer safeRemoveRacer(int target) {
		Iterator<TRacer> it = racers.iterator();
		while(it.hasNext()) {
			TRacer r = it.next();
			if(r.equals(target)) {
				it.remove();
				int lane = -1;
				for(int i=0; i<lanes.length; ++i) {
					if(r.equals(lanes[i])) {
						lane = i;
						lanes[i] = null;
						break;
					}
				}
				for(int i=lane; i<lanes.length-1; ++i) {
					lanes[i] = lanes[i+1];
				}
				return r;
			}
		}
		return null;
	}

	@Override
	protected boolean safeAddRacer(TRacer r) {
		if(racers.size() < lanes.length) {
			racers.add(r);
			lanes[racers.size()-1] = r;
			return true;
		}
		return false;
	}

	@Override
	public List<TRacer> getRacers() {
		return safeRacers;
	}

	@Override
	public TRacer getLast() {
		for(int i = lanes.length-1; i >= 0; i--){
			if(lanes[i] != null) return lanes[i];
		}
		return null;
	}

	@Override
	protected boolean safeTrigger(Channel c) {
		long time = timer.getTimeManager().getTime();
		int id = c.getID();
		if(!hasStarted()){
			if(id == 1){
				start = time;
				for(TRecord r : recordList) {
					r.start = start;
				}
				return true;
			}
			return false;
		}
		if(lanes[id-1] != null){
			TRecord r = getRecord(lanes[id-1].getID());
			r.ended = true;
			r.finish = time;
			lanes[id-1] = null;
			return true;
		}
		
		return false;
	}

}
