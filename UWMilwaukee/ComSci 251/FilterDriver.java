import java.util.Scanner;
public class FilterDriver 
{
	public static void main(String [] args)
	{
		Scanner stdIn = new Scanner(System.in);
		Filter filter = new Filter();
		System.out.println("2nd Order Chebyshev or Butterworth low-pass filter\n");
		filter.initialize();
		//filter.D0 = 5.22; filter.A1 = -.491; filter.A2 = .302;
		
		System.out.println("Enter number of steps to simulate: ");
		int time = stdIn.nextInt();
		//int time = 10;
		
		for(int i=0; i<time; i++)
		{
			//System.out.printf("F x = %3.0f %3.0f %3.0f\n", filter.x,filter.x1,filter.x2);
			//System.out.printf("F y = %3.0f %3.0f %3.0f\n\n", filter.y,filter.y1,filter.y2);
			
			System.out.printf("time = %3d,  input = ", i);
			filter.x = stdIn.nextDouble();
			
			//System.out.printf("X x = %3.0f %3.0f %3.0f\n", filter.x,filter.x1,filter.x2);
			//System.out.printf("X y = %3.0f %3.0f %3.0f\n", filter.y,filter.y1,filter.y2);
			//System.out.println();
			
			filter.y = Math.round(filter.findY(filter.x));
			
			//System.out.printf("Y x = %3.0f %3.0f %3.0f\n", filter.x,filter.x1,filter.x2);
			//System.out.printf("Y y = %3.0f %3.0f %3.0f\n", filter.y,filter.y1,filter.y2);
			
			System.out.printf("            output = %.0f\n", filter.y);
			
			filter.step();
			
			//System.out.printf("S x = %3.0f %3.0f %3.0f\n", filter.x,filter.x1,filter.x2);
			//System.out.printf("S y = %3.0f %3.0f %3.0f\n", filter.y,filter.y1,filter.y2);
			//System.out.println();
		}
		
		System.out.println("Goodbye!");
	}

}