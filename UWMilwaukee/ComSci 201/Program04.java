/*******************************************************
 * Program04.java
 * Yegun Kim
 * 
 * This program checks a valid lock combination
 *******************************************************/


import java.util.Scanner;

public class Program04 
{
	public static void main (String [] args)
	{
		Scanner stdIn= new Scanner(System.in);
		
		String password;
		
		System.out.print("Please enter a lock combination ( ddRddLddR ): ");
		password=stdIn.nextLine();
		
		if(password.length()==9)
		{
			if(password.charAt(2)=='R' && password.charAt(8) =='R' && password.charAt(5)=='L')
			{
				if(password.charAt(0)<'0'||password.charAt(0)>'9')
					System.out.print("\n" + password +" is not a valid lock combination!");
				else if(password.charAt(1)<'0'||password.charAt(1)>'9')
					System.out.print("\n" + password +" is not a valid lock combination!");
				else if(password.charAt(3)<'0'||password.charAt(3)>'9')
					System.out.print("\n" + password +" is not a valid lock combination!");
				else if(password.charAt(4)<'0'||password.charAt(4)>'9')
					System.out.print("\n" + password +" is not a valid lock combination!");
				else if(password.charAt(6)<'0'||password.charAt(6)>'9')
					System.out.print("\n" + password +" is not a valid lock combination!");
				else if(password.charAt(7)<'0'||password.charAt(7)>'9')
					System.out.print("\n" + password +" is not a valid lock combination!");
				else
					System.out.print("\n" + password +" is a valid lock combination!");
					
			}
			else
			{
				System.out.print("\n" + password +" is not a valid lock combination!");
			}
		}
	
		else
		{
			System.out.print("\n" + password +" is not a valid lock combination!");
		}
		
		stdIn.close();
	}

}
