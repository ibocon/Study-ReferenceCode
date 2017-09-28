
public abstract class NamedShape extends Shape {
	private String name; //no white space > next()
	
	public NamedShape(String n){
		super();
		name=n;
	}
	public String getName() {
		return name;
	}
	public void setName(String n){
		name=n;
	}
	//an overriding method for the Object.toString method
	@Override
	public String toString(){
		return name;
	}
	
}
