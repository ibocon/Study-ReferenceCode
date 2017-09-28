
/******************************************************************************
 * Program03.java
 * @author yegunkim
 * 
 * This program prints pitcher's ERA, which should be a floating point number.
 *****************************************************************************/

import java.util.Scanner;

public class Program03 
{
	public static void main(String[] args)
	{
		Scanner stdIn= new Scanner(System.in);
		
		String firstname, lastname;
		
		System.out.print("Pitcher's first name: ");
		firstname=stdIn.nextLine();
		
		System.out.print("Pitcher's last name: ");
		lastname=stdIn.nextLine();
		
		int EarnedRuns, InningsPitched;
		
		System.out.print("Number of earned runs: ");
		EarnedRuns=stdIn.nextInt();
		
		System.out.print("Number of innings pitched: ");
		InningsPitched=stdIn.nextInt();
		
		double ERA= EarnedRuns *9.0 /InningsPitched;
		
		System.out.print(firstname+" "+lastname +" has an ERA of "+ ERA);
		
		
	}
}
