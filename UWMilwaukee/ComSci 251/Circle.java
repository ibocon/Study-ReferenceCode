
public class Circle extends NamedShape {
	private Point center;
	private double radius;
	
	public Circle(){
		this("DefaultCircle", new Point(), 1.0);
	}
	public Circle(String name, Point center, double radius){
		super(name);
		this.center=center; //Reference
		this.radius=radius;
	}
	public double getRadius(){
		return radius;
	}
	public Point center(){
		return center;
	}
	public boolean contains(Point k){
		return (Math.pow(k.getX()-center.getX(), 2)+Math.pow(k.getY()-center.getY(), 2))<Math.pow(radius, 2);
	}
	public double area(){
		return Math.PI*Math.pow(radius, 2);
	}
	public String toString(){
		return super.toString()+":C("+center.toString()+","+radius+")";
	}
}
