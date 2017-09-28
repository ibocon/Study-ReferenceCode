
/******************************************************
 * Program05.java
 * Ye gun Kim
 * 
 * This program05 emulates a simple calculator.
 ******************************************************/

import java.util.Scanner;
public class Program05 
{
	public static void main(String[] args)
	{
		Scanner stdIn=new Scanner(System.in);
		double leftOperand, rightOperand, result;
		String operator;
		
		System.out.println("------------------------------");
		System.out.println();
		System.out.print("Enter the left operand : ");
		leftOperand= stdIn.nextDouble();
		System.out.print("Enter the right operand : ");
		rightOperand= stdIn.nextDouble();
		System.out.println();
		System.out.println("------------------------------");
		System.out.println();
		System.out.println("\t1 -> Multiplication");
		System.out.println("\t2 -> Divison");
		System.out.println("\t3 -> Addition");
		System.out.println("\t4 -> Subtraction");
		System.out.println();
		System.out.println("------------------------------");
		System.out.println();
		
		do
		{
			System.out.print("Choose an perator from the above menu : ");
			operator=stdIn.next();
		}while(!(operator.equals("1")|| operator.equals("2")|| operator.equals("3")||operator.equals("4")));
		
		System.out.println();
		System.out.println("------------------------------");
		System.out.println();
		
		if(operator.equals("1"))
		{
			result=leftOperand*rightOperand;
			System.out.println(leftOperand +" * "+rightOperand+" = " + result);
		}
		else if(operator.equals("2"))
		{
			result=leftOperand/rightOperand;
			System.out.println(leftOperand +" / "+rightOperand+" = " + result);
		}
		else if(operator.equals("3"))
		{
			result=leftOperand+rightOperand;
			System.out.println(leftOperand +" + "+rightOperand+" = " + result);
		}
		else if(operator.equals("4"))
		{
			result=leftOperand-rightOperand;
			System.out.println(leftOperand +" - "+rightOperand+" = " + result);
		}
		else
		{
			System.out.println("System Syntex error");
		}
		
		System.out.println();
		System.out.print("-------------------------------");
		
		stdIn.close();
	}
}