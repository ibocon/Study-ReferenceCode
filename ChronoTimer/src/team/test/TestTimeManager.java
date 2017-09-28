package team.test;


import java.util.Random;
import java.util.concurrent.TimeUnit;

import org.junit.Test;

import team.ChronoTimer;
import team.TChronoTimer;
import team.TimeManager;

public class TestTimeManager {

	@Test
	public void test() {
		
		ChronoTimer timer = new TChronoTimer();
		TimeManager time = timer.getTimeManager();
		
		
		time.setTime("12:00:00");
		long amount = 0, start = time.getTime();
		final Random r = new Random();
		final int iterations = 25;
		for(int i=1; i<iterations; ++i) {
			int add = r.nextInt(15 + 5);
			amount += add;
			try {
				TimeUnit.MILLISECONDS.sleep(add);
			} catch (InterruptedException e) {
				
			}
			TestChronoTimer.assertRange(amount, time.getTime() - start, 10);
		}
		
	}
	
}
