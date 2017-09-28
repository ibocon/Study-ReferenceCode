package team;

public class TRecord implements Record {
	
	protected TRun run;
	protected long start,finish;
	protected boolean ended;

	@Override
	public Run getRun() {
		return run;
	}


	@Override
	public long getStartTime() {
		return start;
	}

	@Override
	public long getFinishTime() {
		return finish;
	}

	@Override
	public boolean isFinished() {
		return ended;
	}
	
	public TRecord(TRun r) {
		run = r;
		start = -1;
		finish = -1;
	}
	
}
