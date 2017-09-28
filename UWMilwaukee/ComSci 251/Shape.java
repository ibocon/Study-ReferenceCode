
public abstract class Shape {
	//super class for Point & NamedShape
	public Shape(){}
	
	public abstract Point center();
	
	public abstract boolean contains(Point x);
	
	public abstract double area();
	
}
