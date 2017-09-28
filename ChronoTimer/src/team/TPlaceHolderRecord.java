package team;

public class TPlaceHolderRecord extends TRecord implements PlaceHolderRecord {

	protected int placeHolder;
	
	public TPlaceHolderRecord(TRun r, int ph) {
		super(r);
		placeHolder = ph;
		// TODO Auto-generated constructor stub
	}

	@Override
	public int getPlaceHolder() {
		// TODO Auto-generated method stub
		return placeHolder;
	}

}
