/******************
 * 
 * @author yegunkim
 * Program08
 *a program converts decimal numbers to binary using user-defined methods
 *
 */

import java.util.Scanner;
public class Program08 
{

	public static void main(String[] args) 
	{
		Scanner stdIn= new Scanner(System.in);
		System.out.println("Welcome to BinaryCalc 3000!\n");
		String ans;
		do
		{
			ans = getDecimal(stdIn);
			
			if(isDecimal(ans))
			{
				String binary = toBinary(ans);
				System.out.println( ans + " in binary is: " + binary);
			}
		
		}while(!ans.equalsIgnoreCase("q"));
			
		System.out.println("There are 10 kinds of people in this world,");
		System.out.println("Those who understand binary, and those who don't.");
		System.out.println("\nThank hyou for using BinaryCalc 3000!");
	
		stdIn.close();
	}
	
	public static boolean isDecimal(String ans)
	{
		int cont=0;
		for(int i=0; i<ans.length(); i++)
		{
			if(Character.isDigit(ans.charAt(i)))
				cont++;
		}
		if(cont==ans.length())
			return true;
		else
			return false;
	}
	
	public static String getDecimal(Scanner stdIn)
	{
		String k;
		do
		{
			System.out.print("Please input a decimal integer or 'q' to quit: ");
			k= stdIn.nextLine();
		}while(!k.equalsIgnoreCase("q") && !isDecimal(k));
		
		return k;
	}
	
	public static String toBinary(String ans)
	{
		int number = Integer.parseInt(ans);
		String binary = "";
		
		while(number!=0)
		{
			if(number%2==1)
			{
				binary= "1" + binary;
			}
			else
			{
				binary= "0" + binary;
			}
			
			number/=2;
			
		}
		
				
		return binary;
	}

}
	


