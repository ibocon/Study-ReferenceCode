
public class Program02 
{		
	static double LENGTH=7.0, HEIGHT=8.25, WIDTH=9.5; 
	
	static double THE_BOX_VOLUME; 

	public static void main(String[] args) 
	{ 
		System.out.println("Given a length of "+ LENGTH + " inches");
		System.out.println("a width of "+ WIDTH + " inches");
		System.out.println("and a height of "+ HEIGHT + " inches \n");
		
		THE_BOX_VOLUME= LENGTH*HEIGHT*WIDTH; // Volume = length * height * width
		
		System.out.println("The box's volume is : "+ THE_BOX_VOLUME + " inches squared." );
		
	}

}
