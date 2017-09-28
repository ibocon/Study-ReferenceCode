
public class Rectangle extends NamedShape {
	private Point ul;
	private Point lr;
	
	public Rectangle(){
		this("DefaultRectangle", new Point(-1,1), new Point(1,-1));
	}
	public Rectangle(String name,Point ul,Point lr){
		super(name);
		this.ul=ul;
		this.lr=lr;
	}
	public Point getUL(){
		return ul;
	}
	public Point getUR(){
		return new Point(lr.getX(),ul.getY());
	}
	public Point getLL(){
		return new Point(ul.getX(),lr.getY());
	}
	public Point getLR(){
		return lr;
	}
	public Point center(){
		double x=(ul.getX()+lr.getX())/2;
		double y=(ul.getY()+lr.getY())/2;
		return new Point(x,y);
	}
	public boolean contains(Point k){
		return (ul.getX()<k.getX()&&k.getX()<lr.getX())
				&&(lr.getY()<k.getY()&&k.getY()<ul.getY());
	}
	public double area(){
		return (lr.getX()-ul.getX())*(ul.getY()-lr.getY());
	}
	public String toString(){
		return super.toString()+":R("+ul.toString()+","+lr.toString()+")";
	}

}
