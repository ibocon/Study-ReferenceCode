import java.util.Scanner;
public class RangeDriver 
{

	public static void main(String[] args) 
	{
		Scanner stdIn=new Scanner(System.in);
		System.out.println("Welcome to the Range testing program!\n");
		System.out.println("Starting Range entry and testing of constructors\n");
		
		System.out.print("Enter min and max values for Range 1: ");
		Range r1 = new Range (stdIn.nextDouble(),stdIn.nextDouble());
		System.out.println("Range 1 created as "+r1.toString());
		Range r2 = new Range ();
		
		System.out.println("Created Range 2 with default constructor: "+r2.toString());
		System.out.print("Enter new max for Range 2: ");
		r2.setMax(stdIn.nextDouble());
		System.out.println("Range 2 changed by setMax(): "+r2.toString());
		System.out.print("Enter new min for Range 2: ");
		r2.setMin(stdIn.nextDouble());
		System.out.println("Range 2 changed by setMin(): "+r2.toString());
		System.out.print("Enter new min and max for Range 2: ");
		r2.setRange(stdIn.nextDouble(), stdIn.nextDouble());
		System.out.println("Range 2 changed by setRange(): " + r2.toString());
		System.out.println();
		System.out.println("Ending Range entry and testing of constructors\n");
		System.out.println("Strarting overlap and containment testing\n");
		
		System.out.print("Enter value X for containment testing: ");
		double x=stdIn.nextDouble();
		if(r1.contains(x))
			System.out.println("Range 1 does contain X");
		else
			System.out.println("Range 1 does not contain X");
		if(r2.contains(x))
			System.out.println("Range 2 does contain X");
		else
			System.out.println("Range 2 does not contain X");
		
		if(r1.overlaps(r2))
			System.out.println("Range 1 and Range 2 overlap");
		else
			System.out.println("Range 1 and Range 2 not overlap");
		
		if(r1.contains(r2))
			System.out.println("Rnage 1 contain Range 2");
		else
			System.out.println("Rnage 1 does not contain Range 2");
		if(r2.contains(r1))
			System.out.println("Rnage 2 contain Range 1");
		else
			System.out.println("Rnage 2 does not contain Range 1");
		System.out.println("\nEnding overlap and containment testing\n");
		System.out.println("Starting combine and equality testing\n");
		
		Range r3 =r1.combineWith(r2);
		Range r4 =r2.combineWith(r1);
		
		if(r3==null && r4==null)
		{
			System.out.println("Range 3 = Range 1 combined with Range 2: null");
			System.out.println("Range 4 = Range 2 combined with Range 1: null");
		}
		else
		{
			System.out.println("Range 3 = Range 1 combined with Range 2: "+r3.toString());
			System.out.println("Range 4 = Range 2 combined with Range 1: "+r4.toString());
		}
		
		if(r1.equals(r2))
			System.out.println("Range 1 and Range 2 are equal");
		else
			System.out.println("Range 1 and Range 2 are not equal");
		
		if(r3==null && r4==null)
			System.out.println("Range 3 and Range 4 are null");
		else if(r3.equals(r4))
			System.out.println("Range 3 and Range 4 are equal");
		else
			System.out.println("Range 3 and Range 4 are not equal");
		
		System.out.println("\nEnding combine and equality testing\n");
		System.out.println("Goodbye!\n");

	}

}
