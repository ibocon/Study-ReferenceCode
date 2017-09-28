package team;

import static org.junit.Assert.*;

import org.junit.Test;

import team.EventType;
import team.Racer;
import team.SensorType;
import team.TChronoTimer;
import team.TRun;

public class TestIND {

	@Test
	public void test() {
		TChronoTimer timer = new TChronoTimer();
		
		assertFalse(timer.isOn());
		assertTrue(timer.powerOn());
		
		TRun currRun = timer.getLatestRun();
		assertTrue(currRun.getID() == 1);
		
		assertTrue(currRun.getEventType() == EventType.IND);
		assertTrue(timer.toggleChannel(1));
		assertFalse(currRun.trigger(timer.getChannel(1)));
		timer.getChannel(1).setSensorType(SensorType.EYE);
		assertTrue(timer.toggleChannel(2));
		assertFalse(currRun.trigger(timer.getChannel(2)));
		timer.getChannel(2).setSensorType(SensorType.EYE);
		
		assertNotNull(currRun.addRacer(666));
		assertNotNull(currRun.addRacer(29));
		assertEquals(currRun.getLast(), ((Racer)timer.getRacer(666)));
		
		assertFalse(currRun.hasStarted());
		assertTrue(currRun.trigger(timer.getChannel(1)));
		assertTrue(currRun.hasStarted());
		
		
		
		
		
		assertTrue(timer.reset());
		currRun = timer.getLatestRun();
		assertTrue(currRun.getEventType() == EventType.IND);
	}

}
