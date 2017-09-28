
public class Point extends Shape {
	private double x;
	private double y;
	
	public Point()
	{
		this(0.0,0.0);
	}
	public Point(double x, double y)
	{
		super();
		this.x=x;
		this.y=y;
	}
	public double getX(){
		return x;
	}
	public double getY(){
		return y;
	}
	public boolean equals(Object point2){
		if(point2 instanceof Point)
		{
			Point k=(Point)point2;
			return k.getX()==this.x&&k.getY()==this.y;
		}
		else
			return false;
		
	}
	public String toString(){
		return "("+this.x+","+this.y+")";
	}
	public Point center(){
		return this;
	}
	public boolean contains(Point k){
		return (k.x==this.x&&k.y==this.y);
	}
	public double area(){
		return 0.0;
	}
	public double distance(Point k){
		return Math.sqrt(Math.pow(this.x-k.x, 2)+Math.pow(this.y-k.y, 2));
	}
}
