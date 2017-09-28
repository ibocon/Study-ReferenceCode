package team;

public class TRacerRecord extends TRecord implements RacerRecord {

	protected TRacer racer;
	
	public TRacerRecord(TRun r, TRacer rc) {
		super(r);
		racer = rc;
		// TODO Auto-generated constructor stub
	}

	@Override
	public Racer getRacer() {
		// TODO Auto-generated method stub
		return racer;
	}

}
