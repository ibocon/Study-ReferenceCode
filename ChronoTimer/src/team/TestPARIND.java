package team;

import static org.junit.Assert.*;
import org.junit.Test;

public class TestPARIND {
	@Test
	public void test_AddRacer() {
		ParallelIndependentRun race =new ParallelIndependentRun(new TChronoTimer(), 1);
		race.timer.powerOn();
		assertNotNull(race.addRacer(1));
		assertTrue(race.allRacers.getFirst().id==1);
		//checking add same racer's id
		assertNull(race.addRacer(1));
		assertTrue(race.allRacers.size()==1);
		assertNotNull(race.addRacer(2));
		assertTrue(race.allRacers.getFirst().id==2);
	}
	@Test
	public void test_endRun() {
		ParallelIndependentRun race =new ParallelIndependentRun(new TChronoTimer(), 1);
		race.timer.powerOn();
		race.addRacer(1);
		race.addRacer(2);
		//start testing
		assertTrue(race.endRun());
		assertFalse(race.endRun());
	}
	@Test
	public void test_doNotFinish() {
		ParallelIndependentRun race =new ParallelIndependentRun(new TChronoTimer(), 1);
		race.timer.powerOn();
		race.addRacer(1);
		race.addRacer(2);
		//start testing
		assertTrue(race.doNotFinish());
		assertTrue(race.ended.getFirst().records.get((Integer)1).ended);
		assertEquals(race.ended.getFirst().records.get((Integer)1).finish,-1);
		assertTrue(race.doNotFinish());
		assertTrue(race.ended.getFirst().records.get((Integer)1).ended);
		assertEquals(race.ended.getFirst().records.get((Integer)1).finish,-1);
		assertFalse(race.doNotFinish());
		race.finished=false;
		race.track1S.clear();
		race.track2S.clear();
		assertFalse(race.doNotFinish());
	}
	@Test
	public void test_RemoveRacer() {
		ParallelIndependentRun race =new ParallelIndependentRun(new TChronoTimer(), 1);
		race.timer.powerOn();
		TRacer r1 = race.addRacer(1);
		TRacer r2 = race.addRacer(2);
		//start testing
		assertEquals(2,race.allRacers.size());
		assertEquals(2,race.toStart.size());
		assertTrue(race.allRacers.contains(r1));
		assertTrue(race.allRacers.contains(r2));
		
		assertTrue(race.removeRacer(1));
		assertEquals(1,race.allRacers.size());
		assertFalse(race.allRacers.contains(r1));
		assertTrue(race.allRacers.contains(r2));
		
		assertFalse(race.removeRacer(1));
		assertEquals(1,race.allRacers.size());
		assertFalse(race.allRacers.contains(r1));
		assertTrue(race.allRacers.contains(r2));
		
		assertTrue(race.removeRacer(2));
		assertEquals(0,race.allRacers.size());
		assertFalse(race.allRacers.contains(r1));
		assertFalse(race.allRacers.contains(r2));
	}
	@Test
	public void test_Trigger() {
		ParallelIndependentRun race =new ParallelIndependentRun(new TChronoTimer(), 1);
		race.timer.powerOn();
		race.addRacer(1);
		race.addRacer(2);
		//start testing
		assertTrue(race.safeTrigger(new TChannel(race.timer,3)));
		assertTrue(race.safeTrigger(new TChannel(race.timer,4)));
	}

}
