package team.test;
import static org.junit.Assert.*;

import org.junit.Test;

import team.SensorType;
import team.TChannel;
import team.TChronoTimer;

public class TestChannel {

	@Test
	public void testSet1() {		//tests the first channel constructor as well as isEnabled, toggle, getID, getSensorType, setSensorType
		TChronoTimer timer = new TChronoTimer();
		timer.powerOn();
		timer.connect(SensorType.PAD,1);
		TChannel chan = new TChannel(timer, 1);
		assertFalse(chan.isEnabled());
		assertTrue(chan.toggle());
		assertTrue(chan.isEnabled());
		assertTrue(chan.getID() == 1);
		chan.setSensorType(null);
		assertTrue(chan.getSensorType() == SensorType.NONE);
		chan.setSensorType(SensorType.PAD);
		assertTrue(chan.getSensorType() == SensorType.PAD);
	}
	
	@Test
	public void testSet2() {		//tests the second channel constructor
		TChronoTimer timer = new TChronoTimer();
		timer.powerOn();
		TChannel chan = new TChannel(timer, 1, false);
		timer.connect(SensorType.GATE, chan);
		assertTrue(chan.setEnabled(true));
		timer.newRun();
		timer.getLatestRun().addRacer(1);
		
		assertTrue(chan.trigger());
	}
	
	@Test
	public void testSet3() {
		TChronoTimer timer = new TChronoTimer();
		timer.powerOn();
		TChannel chan = new TChannel(timer, 1, SensorType.NONE);
		
		assertFalse(chan.isEnabled());
		assertTrue(chan.getID() == 1);
		assertTrue(chan.getSensorType() == SensorType.NONE);
		
	}
	
	@Test
	public void testSet4() {
		TChronoTimer timer = new TChronoTimer();
		timer.powerOn();
		TChannel chan = new TChannel(timer, 1, SensorType.EYE, false);
		
		assertFalse(chan.isEnabled());
		assertTrue(chan.getID() == 1);
		assertTrue(chan.getSensorType() == SensorType.EYE);
		
	}

}
