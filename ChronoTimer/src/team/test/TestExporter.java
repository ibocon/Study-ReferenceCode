package team.test;

import static org.junit.Assert.*;

import org.junit.Test;
import team.*;
import java.io.*;

public class TestExporter {
	TChronoTimer ct = new TChronoTimer();
	TRun race, race2;
	
	Exporter ex;
	
	//instantiate two runs with one runner each
	public void setUp() {
		ct.powerOn();
		ex = ct.getExporter();
		ct.toggleChannel(1);
		ct.toggleChannel(2);
		
		race = ct.getLatestRun();
		race.addRacer(11);
		race.trigger(new TChannel(ct, 1));
		race.trigger(new TChannel(ct, 2));
		race.endRun();
		
		ct.newRun();
		race2 = ct.getLatestRun();
		race2.addRacer(22);
		race2.trigger(new TChannel(ct, 1));
		race2.trigger(new TChannel(ct, 2));
		race2.endRun();
	}
	@Test
	public void testNullExport() {
		//tests to be sure null input cannot create a file
		try{
			ex.export(null);
		}
		catch(NullPointerException e) {
			assertFalse(foundFile("Run"+null+".txt"));
		}
	}
	@Test
	public void testTwoRaces() {
		setUp();
		
		//make sure both runs have proper file names and exist
		ex.export(race);
		assertTrue(foundFile("Run"+ct.getRuns().get(0).getID()+".txt"));
		ex.export(race2);
		assertTrue(foundFile("Run"+ct.getRuns().get(1).getID()+".txt"));
	}
	
	@Test
	public void testReset() {
		
		String fileB = "", fileAft = "", line = "";
		setUp();

		ex.export(ct.getLatestRun());

		//read in file
		try {
			FileReader fileBefore = new FileReader("Run"+ct.getLatestRun().getID()+".txt");
			BufferedReader bufReader = new BufferedReader(fileBefore);
			while((line = bufReader.readLine()) != null) fileB += line;
			bufReader.close();
			
		} catch(FileNotFoundException e) {
			e.printStackTrace();
		} catch(IOException e) {
			e.printStackTrace();
		}
		
		//reset the run data
		ct.reset();
		line = "";
		ex.export(ct.getLatestRun());
		
		//read in second file
		try {
			FileReader fileAfter = new FileReader("Run"+ct.getLatestRun().getID()+".txt");
			BufferedReader bufReader = new BufferedReader(fileAfter);
			while((line = bufReader.readLine()) != null) fileAft += line;
			bufReader.close();
			
		} catch(FileNotFoundException e) {
			e.printStackTrace();
		} catch(IOException e) {
			e.printStackTrace();
		}
		
		//A default run has no records to print
		assertTrue(fileB.indexOf("\"records\":[]") == -1);
		assertFalse(fileAft.indexOf("\"records\":[]") == -1);
	}
	
	//private helper to be sure that there exists a file with the given name
	private boolean foundFile(String filename) {
		try {
			FileReader file = new FileReader(filename);
		} catch(FileNotFoundException e) {
			return false;
		}
		return true;
	}
}
