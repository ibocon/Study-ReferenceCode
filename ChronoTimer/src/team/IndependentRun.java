package team;

import java.util.Collections;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

public class IndependentRun extends TRun {
	
	protected LinkedList<TRacer> racers, toStart, toEnd;
	protected List<Racer> safe_racers;
	
	protected int front;
	protected int back;
	
	protected boolean started;
	
	@Override
	public boolean hasStarted() {
		return started;
	}
	
	@Override
	public boolean safeTrigger(Channel c) {
		long time = timer.getTimeManager().getTime();
		if(c.getID() == 1) {
			if(toStart.isEmpty()) return false;
			TRacer racer = toStart.pop();
			records.get(racer.id).start = time;
			toEnd.addLast(racer);
			started = true;
		} else if(c.getID() == 2) {
			if(toEnd.isEmpty()) return false;
			TRacer racer = toEnd.pop();
			TRecord rec = records.get(racer.id);
			rec.finish = time;
			rec.ended = true;
		}
		return true;
	}

	@Override
	public boolean safeAddRacer(TRacer r) {
		racers.addFirst(r);
		toStart.addFirst(r);
		return true;
	}
	
	/**
	 * Ends the current run.
	 * @return True if the operation was successful. false if Chrono Timer is off or latest run already ended.
	 */
	public boolean endRun() {
		if(!timer.isOn() || finished) return false;
		finished = true;
		for(TRecord r : records.values())
			r.ended = true;
		return true;
	}
	
	/**
	 * Flags a runner as "Do Not Finish."
	 * @return True if the runner was successfully flagged
	 * @return False if there are no runners to flag or the run is finished
	 */
	@Override
	public boolean doNotFinish() {
		if(!timer.isOn() || finished || getRacers().isEmpty()) return false;
		TRacer r = toEnd.pollFirst();
		if(r == null) {
			r = toStart.pollFirst();
			if(r == null) return false;
		}
		records.get(r.id).ended = true;
		return true;
	}
	
	/**
	 * This create a list of racer's records and return String
	 * @return String of racer's record list
	 * 
	 */
	public String toString(){
		String result="";
		Iterator<TRacer> t=racers.iterator();
		while(t.hasNext()){
			result+=(racers.toString()+"\n");
		}
		return result;
	}
	
	@Override
	public List<Racer> getRacers() {
		return safe_racers;
	}
	
	@Override
	public Racer getLast() {
		return racers.getLast();
	}
	
	
	/**
	 * This method removes the target id racer from racers, start, and finish list.
	 * @return working correctly or not
	 * @param target racer's id.
	 */
	protected TRacer safeRemoveRacer(int target) {
		TRacer temp= racerSearch(target);
		if(temp!=null){
			racers.remove(temp);
			if(toStart.indexOf(temp)!=-1) toStart.remove(temp);
			if(toEnd.indexOf(temp)!=-1) toEnd.remove(temp);
			return temp;
		}else{
			return null;
		}
	}
	
	/**
	 * private method to find out the target id racer
	 * @param target
	 * @return the target id racer
	 */
	private TRacer racerSearch(int target){
		Iterator<TRacer> t=racers.iterator();
		while(t.hasNext()){
			TRacer cur=(TRacer)t.next();
			if(cur.getID()==target)
				return cur;
		}
		return null;
	}
	
	@Override
	public EventType getEventType() {
		return EventType.IND;
	}
	
	public IndependentRun(TChronoTimer timer, int id) {
		super(timer, id);
		racers=new LinkedList<TRacer>();
		toStart = new LinkedList<TRacer>();
		toEnd = new LinkedList<TRacer>();
		safe_racers = Collections.unmodifiableList(racers);
	}
	
	public IndependentRun(TRun old) {
		super(old);
		racers=new LinkedList<TRacer>();
		toStart = new LinkedList<TRacer>();
		toEnd = new LinkedList<TRacer>();
		safe_racers = Collections.unmodifiableList(racers);
		TRacerRecord rR;
		for(TRecord r : old.getRecords()) {
			if(r instanceof TRacerRecord) {
				rR = (TRacerRecord) r;
				addRacer(rR.getRacer().getID());
			}
		}
	}

	public boolean swap() {
		if(toEnd.size() < 2) return false;
		TRacer t1 = toEnd.pop();
		TRacer t2 = toEnd.pop();
		toEnd.push(t1);
		toEnd.push(t2);
		return true;
	}
	
}
