
public class Range 
{
	private double min, max;
	
	public Range()
	{
		this (0,0);
	}
	public Range(double min, double max)
	{
		if(max<min)
		{
			this.min=0;
			this.max=0;
		}
		else
		{
			this.min=min;
			this.max=max;
		}
	}
	
	
	public double getMin()
	{
		return min;
	}
	public double getMax()
	{
		return max;
	}
	
	public void setMin(double m)
	{
		if(m>max)
			return;
		else
			this.min=m;
	}
	public void setMax(double m)
	{
		if(m<min)
			return;
		else
			this.max=m;
	}
	public void setRange(double min, double max)
	{
		if(max<min)
			return;
		else
		{
			this.min=min;
			this.max=max;
		}
	}
	
	public boolean overlaps(Range r2) //problem 1
	{
		if(Math.max(this.min, r2.getMin()) <= Math.min(this.max, r2.getMax()))
			return true;
		else
			return false;
	}
	public boolean contains(double x)
	{
		if(x>=min && x<=max)
			return true;
		else
			return false;
	}
	public boolean contains(Range r2)
	{
		if(r2.getMin()>=this.min && r2.getMax()<=this.max)
			return true;
		else
			return false;
	}
	
	public Range combineWith(Range r2)//problem 1
	{
		Range r3=new Range();
		
		if(this.min<r2.getMin())
			r3.setMin(this.min);
		else
			r3.setMin(r2.getMin());
		
		if(this.max>r2.getMax())
			r3.setMax(this.max);
		else
			r3.setMax(r2.getMax());
		
		if(!(Math.max(this.min, r2.getMin()) <= Math.min(this.max, r2.getMax())))
			return null;
		
		return r3;
			
	}
	
	public boolean equals(Range r2)
	{
		if(this.min==r2.getMin() && this.max==r2.getMax())
			return true;
		else
			return false;
	}
	//------------------------------------------
	public String toString()
	{
		String s="("+min+","+max+")";
		return s;
	}


}
