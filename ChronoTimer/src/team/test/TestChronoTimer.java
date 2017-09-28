package team.test;
import static org.junit.Assert.*;

import java.util.List;
import java.util.LinkedList;
import org.junit.Test;

import team.Channel;
import team.ChronoTimer;
import team.Racer;
import team.SensorType;
import team.TChronoTimer;
import team.TRacer;
import team.TimeManager;


public class TestChronoTimer {

	@Test
	public void testPower() {
		TChronoTimer timer = new TChronoTimer();
		assertTrue(!timer.isOn());
		assertTrue(timer.togglePower());
		assertTrue(timer.togglePower());
		assertTrue(timer.powerOn());
		assertTrue(timer.powerOff());
	}
	
	@Test
	public void testReset() {
		TChronoTimer timer = new TChronoTimer();
		assertTrue(timer.powerOn());
		assertTrue(timer.connect(SensorType.PAD, 1));
		assertTrue(timer.connect(SensorType.GATE, 2));
		assertNotNull(timer.getLatestRun().addRacer(5));
		assertTrue(timer.toggleChannel(1));
		timer.getTimeManager().setTime("00:00:00");
		assertTrue(timer.trigger(1));
		assertRange(timer.getLatestRun().getRacers().get(0).getRecords().get(timer.getLatestRun().getID()).getStartTime(), 0, 3);
		assertEquals(timer.getLatestRun().getRacers().get(0).getRecords().get(timer.getLatestRun().getID()).getFinishTime(), -1);
		assertTrue(timer.reset());
		assertEquals(0,timer.getLatestRun().getRacers().size());
	}
	
	@Test
	public void testChannels() {
		TChronoTimer timer = new TChronoTimer();
		assertTrue(timer.powerOn());
		
		List<Channel> channels = timer.getChannels();
		assertTrue(channels != null);
		assertTrue(channels.size() == ChronoTimer.MAXIMUM_CHANNELS);
		int i=1;
		for(Channel c : channels) {
			assertTrue(c != null);
			assertEquals(i,c.getID());
			assertEquals(SensorType.NONE, c.getSensorType());
			assertFalse(c.isEnabled());
			assertTrue(c.enable());
			//Test Channel enable/disable/connect
			for(int j=0; j<10; ++j) {
				SensorType s = SensorType.values()[(int)(Math.random()*SensorType.values().length)];
				c.setSensorType(s);
				assertEquals(s, c.getSensorType());
				c.setSensorType(SensorType.NONE);
				assertEquals(SensorType.NONE, c.getSensorType());
				assertTrue(timer.disconnect(i));
				assertFalse(c.isEnabled());
			}
			//Test ChronoTimer connect/disconnect
			for(int j=0; j<10; ++j) {
				SensorType s = SensorType.values()[(int)(Math.random()*SensorType.values().length)];
				assertTrue(timer.connect(s, i));
				assertFalse(c.isEnabled());
				assertTrue(c.enable());
				assertTrue(timer.disconnect(i));
			}
			++i;
		}
	}
	
	@Test
	public void testTrigger() {
		TChronoTimer timer = new TChronoTimer();
		for(int i=1; i<ChronoTimer.MAXIMUM_CHANNELS+1; ++i) {
			assertFalse(timer.trigger(i));
		}
		assertTrue(timer.powerOn());
		for(int i=1; i<ChronoTimer.MAXIMUM_CHANNELS+1; ++i) {
			assertFalse(timer.trigger(i));
		}
		assertTrue(timer.connect(SensorType.EYE, 1));
		assertTrue(timer.connect(SensorType.EYE, 2));
		assertFalse(timer.trigger(1));
		assertFalse(timer.trigger(2));
		assertTrue(timer.toggleChannel(1));
		assertTrue(timer.getChannels().get(0).isEnabled());
		assertTrue(timer.toggleChannel(2));
		assertTrue(timer.getChannels().get(1).isEnabled());
		Racer second = timer.getLatestRun().addRacer(25);
		assertNotNull(second);
		Racer first = timer.getLatestRun().addRacer(72);
		assertNotNull(first);
		timer.getTimeManager().setTime("00:00:00");
		assertTrue(timer.trigger(1));
		assertEquals(72, first.getID());
		assertRange(0,first.getRecords().get(timer.getLatestRun().getID()).getStartTime(), 3);
		timer.getTimeManager().setTime("00:01:00");
		assertTrue(timer.trigger(2));
		assertRange(60*1000, first.getRecords().get(timer.getLatestRun().getID()).getFinishTime(), 3);
		assertRange(60*1000, first.getRecords().get(timer.getLatestRun().getID()).getFinishTime(), 3);
		//assertFalse(timer.trigger(2));
		assertEquals(-1,second.getRecords().get(timer.getLatestRun().getID()).getStartTime());
		assertEquals(-1,second.getRecords().get(timer.getLatestRun().getID()).getFinishTime());
		timer.getTimeManager().setTime("00:01:30");
		assertTrue(timer.trigger(1));
		timer.getTimeManager().setTime("00:02:20");
		assertTrue(timer.trigger(2));
		assertRange(second.getRecords().get(timer.getLatestRun().getID()).getStartTime(), 60*1500, 3);
		assertRange(second.getRecords().get(timer.getLatestRun().getID()).getFinishTime(), 60*2000 + 20*1000, 3);
		assertTrue(timer.endRun());
		assertFalse(timer.endRun());
	}
	
	public void testRunner() {
		TChronoTimer timer = new TChronoTimer();
		timer.setPower(true);
		assertTrue(timer.isOn());
		timer.getTimeManager().setTime("00:00:00");
		timer.toggleChannel(1); timer.toggleChannel(2);
		LinkedList<TRacer> racers = new LinkedList<TRacer>();
		
		for(int i = 0; i < 10; i++) {
			racers.add(new TRacer(i));
			
			assertEquals(racers.get(i).getID(), i);
			assertEquals(racers.get(i).getRecords().get(timer.getLatestRun().getID()).getStartTime(), -1);
			assertEquals(racers.get(i).getRecords().get(timer.getLatestRun().getID()).getFinishTime(), -1);
			assertFalse(racers.get(i).getRecords().get(timer.getLatestRun().getID()).isFinished());
			
			timer.getLatestRun().addRacer(i);
		}
		
		List<Channel> channels = timer.getChannels();
		for(int i = 0; i < 10; i++) {
			long st, fin;
			timer.trigger(channels.get(0));
			st = timer.getLatestRun().getRacers().get(i).getRecords().get(timer.getLatestRun().getID()).getStartTime();
			
			assertFalse(st == -1);
			assertFalse(timer.getLatestRun().getRacers().get(i).getRecords().get(timer.getLatestRun().getID()).didNotFinish());
			
			timer.trigger(channels.get(1));
			fin = timer.getLatestRun().getRacers().get(i).getRecords().get(timer.getLatestRun().getID()).getFinishTime();
			
			assertFalse(fin == -1);
			assertTrue(fin-st > 0);	
			assertFalse(timer.getLatestRun().getRacers().get(i).getRecords().get(timer.getLatestRun().getID()).didNotFinish());
			
		}
	}
	
	/**
	 * Assert a range if the timings were off very slightly.
	 * @param result - The result
	 * @param expected - the Expected value
	 * @param range - The accepted range.
	 */
	public static void assertRange(long result, long expected, long range) {
		assertFalse(Math.abs(result-expected) > range);
	}
	
}
