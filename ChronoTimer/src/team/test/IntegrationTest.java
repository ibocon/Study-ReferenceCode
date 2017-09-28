package team.test;

import static org.junit.Assert.assertEquals;
import static org.junit.Assert.assertFalse;
import static org.junit.Assert.assertTrue;

import java.util.LinkedList;
import java.util.Random;

import org.junit.Test;

import team.Channel;
import team.ChronoTimer;
import team.EventType;
import team.Racer;
import team.Record;
import team.Run;
import team.SensorType;
import team.TChronoTimer;
import team.TRacerRecord;
import team.TimeManager;
/*
 * This is black box testing between components.
 * 1. ChronoTimer and IND
 * 2. ChronoTimer and PARIND
 * 3. Run and Runner
 */
//Do not manipulate direct to component!
//This is "Black Box" testing, which means you can only access to exposed method and field.
public class IntegrationTest {

	@Test
	public void testIND() {
		
		ChronoTimer timer = new TChronoTimer();
		
		assertFalse(timer.trigger(1));
		assertFalse(timer.trigger(2));
		assertTrue(timer.powerOn());
		assertFalse(timer.trigger(1));
		assertFalse(timer.trigger(2));
		
		timer.getChannels().get(0).setSensorType(SensorType.EYE);
		assertFalse(timer.trigger(1));
		assertEquals(timer.getChannels().get(0).getSensorType(),SensorType.EYE);
		timer.getChannels().get(1).setSensorType(SensorType.EYE);
		assertFalse(timer.trigger(2));
		assertEquals(timer.getChannels().get(1).getSensorType(),SensorType.EYE);
		
		Run run = timer.getLatestRun();
		assertTrue(run != null);
		assertEquals(run.getEventType(), EventType.IND);
		assertTrue(run.getRecords().isEmpty());
		
		assertFalse(run.addRacer(25) == null);
		assertTrue(run.addRacer(25) == null);
		assertFalse(run.addRacer(5) == null);
		assertTrue(run.addRacer(5) == null);
		assertFalse(run.addRacer(7) == null);
		assertFalse(run.addRacer(2) == null);
		assertTrue(run.removeRacer(7));
		assertFalse(run.removeRacer(7));
		
		assertFalse(timer.trigger(1));
		assertFalse(timer.getChannel(1).isEnabled());
		assertFalse(timer.trigger(2));
		assertFalse(timer.getChannel(2).isEnabled());
		assertTrue(timer.getChannel(1).enable());
		assertTrue(timer.getChannel(2).enable());
		assertTrue(timer.getChannel(1).isEnabled());
		assertTrue(timer.getChannel(2).isEnabled());
		
		timer.getTimeManager().setTime("12:00:00");
		assertTrue(timer.trigger(1));
		assertTrue(timer.doNotFinish());
		assertTrue(timer.trigger(2));
		assertTrue(timer.trigger(1));
		assertTrue(timer.trigger(2));
		
		Record rec = run.getRecord(25);
		assertTrue(rec != null);
		assertFalse(rec.didNotFinish());
		
		rec = run.getRecord(5);
		assertTrue(rec != null);
		assertFalse(rec.didNotFinish());
		
		rec = run.getRecord(2);
		assertTrue(rec != null);
		assertTrue(rec.didNotFinish());
		
	}
	
	@Test
	public void testPARINDConversion() {
		
		ChronoTimer timer = new TChronoTimer();
		
		assertTrue(timer.powerOn());
		assertTrue(timer.isOn());
		
		int iterations = 10, averageRacers = 25, id = 1;
		LinkedList<Racer> racers = new LinkedList<Racer>();
		Random rand = new Random();
		
		for(int i=0; i<iterations; ++i) {
			assertEquals(i+1,timer.getRuns().size());
			Run run = timer.getLatestRun();
			assertEquals(run.getEventType(),EventType.IND);
			
			int totalRacers = rand.nextInt(20) - 10 + averageRacers;
			for(int j=0; j<totalRacers; ++j) {
				Racer r = run.addRacer(id++ * (i+1));
				assertTrue(r != null);
				racers.addFirst(r);
			}
			
			timer.setEvent(EventType.PARIND);
			run = timer.getLatestRun();
			assertEquals(run.getEventType(),EventType.PARIND);
			assertEquals(racers.size(), run.getRacers().size());
			
			for(Racer rc : run.getRacers()) {
				Racer r = racers.poll();
				assertEquals(r,rc);
				assertEquals(r,((TRacerRecord)run.getRecord(r.getID())).getRacer());
			}
			assertTrue(racers.isEmpty());
			
			assertTrue(timer.endRun());
			timer.newRun();
			
		}
		
	}
	
	@Test
	public void testPARIND() {
		
		ChronoTimer timer = new TChronoTimer();
		assertTrue(timer.powerOn());
		
		timer.getTimeManager().setTime("12:00:00");
		
		assertTrue(timer.setEvent(EventType.PARIND));
		Run run = timer.getLatestRun();
		assertTrue(run != null);
		assertEquals(1,run.getID());
		
		run.addRacer(99);
		run.addRacer(86);
		run.addRacer(26);
		run.addRacer(925);
		run.addRacer(3);
		
		assertEquals(5,run.getRacers().size());
		
		for(int i=1; i<=4; ++i) {
			Channel c = timer.getChannel(i);
			assertEquals(SensorType.NONE,c.getSensorType());
			assertFalse(c.isEnabled());
			c.setSensorType(SensorType.PAD);
			assertEquals(SensorType.PAD,c.getSensorType());
			assertTrue(c.enable());
			assertTrue(c.isEnabled());
		}
		
		Record record;
		
		assertTrue(timer.trigger(1));	//Racer 3
		record = run.getRecord(3);
		testStarted(record);
		
		assertTrue(timer.trigger(3));	//Racer 925
		record = run.getRecord(925);
		testStarted(record);
		
		assertTrue(timer.trigger(1));	//Racer 26
		record = run.getRecord(26);
		testStarted(record);
		
		assertTrue(timer.doNotFinish());	//DNF Racer 3
		record = run.getRecord(3);
		assertTrue(record.getStartTime() > -1);
		assertTrue(record.getFinishTime() == -1);
		assertTrue(record.didNotFinish());
		
		record = run.getRecord(26);		// Make sure 26 is still in the race.
		testStarted(record);
		
		assertTrue(timer.trigger(3));	//Racer 86
		record = run.getRecord(86);
		testStarted(record);
		
		assertTrue(timer.trigger(4));	//Racer 925 finishes!
		record = run.getRecord(925);
		testFinished(record);
		
		assertTrue(timer.trigger(1));	//Racer 99
		record = run.getRecord(99);
		testStarted(record);
		
		assertTrue(timer.trigger(2));	//Racer 26 finishes!
		record = run.getRecord(26);
		testFinished(record);
		
		assertTrue(timer.trigger(4));	//Racer 86 finishes!
		record = run.getRecord(86);
		testFinished(record);
		
		assertTrue(timer.trigger(2));	//Racer 99 finishes!
		record = run.getRecord(99);
		testFinished(record);
		
		
	}
	
	private void testStarted(Record record) {
		assertTrue(record.getStartTime() > -1);
		assertTrue(record.getFinishTime() == -1);
		assertFalse(record.didNotFinish());
	}
	
	private void testFinished(Record record) {
		assertTrue(record.getStartTime() > -1);
		assertTrue(record.getFinishTime() > -1);
		assertFalse(record.didNotFinish());
		assertTrue(record.isFinished());
	}
	
}
