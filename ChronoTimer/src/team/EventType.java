package team;

public enum EventType {
	IND(IndependentRun.class),
	PARIND(ParallelIndependentRun.class),
	GRP(GroupRun.class),
	PARGRP(ParallelGroupRun.class);
	
	private final Class<? extends TRun> c;
	
	public final TRun getNewInstance() {
		try {
			return c.newInstance();
		} catch (Exception e) {
			return null;
		}
	}
	
	public final TRun getNewInstanceFromOld(TRun old) {
		try {
			return c.getConstructor(TRun.class).newInstance(old);
		} catch (Exception e) {
			System.out.println("Event Type change failed: " + e.getMessage());
			return null;
		}
	}
	
	EventType(Class<? extends TRun> nc) {
		c = nc;
	}
	
}
