import java.util.Scanner;
public class IntSetDriver {

	public static void main(String[] args) {
		Scanner stdIn=new Scanner(System.in);
		System.out.println("IntSet class Program");
		System.out.println("======================\n");
		System.out.print("Enter IntSet 1: ");
		IntSet first = new IntSet();
		stdIn.next();
		first.readSet(stdIn);
		System.out.println("IntSet 1: " + first.toString());
		System.out.println("Size of IntSet 1: "+first.size());
		System.out.println();
		
		System.out.print("Enter index between 0 and "+(first.size()-1)+ ": ");
		int ans=stdIn.nextInt();
		
		System.out.println("The value of the 1'th element of IntSet 1 is "+first.get(ans));
		System.out.println();
		
		System.out.print("Enter one additional value for IntSet 1: ");
		first.add(stdIn.nextInt());
		
		System.out.println("IntSet 1: "+first.toString());
		
		System.out.print("\nEnter value to remove from IntSet 1: ");
		first.remove(stdIn.nextInt());
		
		System.out.println("IntSet 1: "+first.toString());
		
		System.out.print("\nEnter IntSet 2: ");
		IntSet second = new IntSet();
		second.readSet(stdIn);
		System.out.println("IntSet 2 : " +second.toString());
		System.out.println();
		
		System.out.println("Return value of \"IntSet1.eqals(Intset2)\": "+first.equals(second));

		System.out.println("Uniton of IntSets 1 and 2: "+first.plus(second).toString());
		System.out.println("Intersection of IntSets 1 and 2: "+first.intersection(second).toString());
		System.out.println("IntSet 1 minus IntSet 2: "+first.minus(second).toString());
		System.out.println();
		
		System.out.print("IntSet 3 (initialized with default constructor) : ");
		IntSet third = new IntSet();
		System.out.println(third.toString());
		
		System.out.print("IntSet 4 (initialized with copy constructor from IntSet 1) : ");
		IntSet fourth = new IntSet(first);
		System.out.println(fourth.toString());
		
		System.out.print("IntSet 5 (initialized with copy constructor from IntSet 1) : ");
		IntSet fifth = new IntSet(stdIn.nextInt());
		System.out.println(fifth.toString());
		
		System.out.println("\nDone.");
		
		
		
		
	}

}
