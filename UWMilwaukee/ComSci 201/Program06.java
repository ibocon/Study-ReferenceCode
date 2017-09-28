
/*********************************
 * Program06.java
 * Ye gun Kim
 *
 * a single Player (the user) to play a simple two dice game of chance against
"The Odds".
***********************************/

import java.util.Scanner;

public class Program06 
{
	public static void main (String[] args)
	{
		Scanner stdIn= new Scanner(System.in);
		
		int rounds=0, win=0, lose=0;
		int die1, die2;
		String exit = "";
		
		System.out.println("     Wehlcome to Computer Dice");
		System.out.println("-------------------------------------");
		System.out.println("You will first roll your dice");
		System.out.println();
		System.out.println("be determined: ");
		System.out.println();
		System.out.println("any pair and you Win");
		System.out.println("anything else and you Lose");
		System.out.println("--------------------------------------");
		
		do
		{
			rounds++;
			die1 = (int) (Math.random() * 6) +1;
			die2 = (int) (Math.random() * 6) +1;
			
			System.out.println("\n\nPlayer");
			System.out.println("--------");
			System.out.println("  "+die1+"  "+die2);
			System.out.println();
			
			if(die1==die2)
			{
				win++;
				System.out.println("Congrates, you win!");
				
			}
			else if(die1!=die2)
			{
				if(die1==die2+1 || die1==die2-1)
				{
					System.out.println("Its a tie!");
				}
				else
				{
					lose++;
					System.out.println("Sorry, you lose!");
				}
			}
			else
				System.out.println("Logical error");
			
			System.out.println();
			
			do
			{
				System.out.print("Do you wish to play again [y, n] : ");
				exit=stdIn.next();
			}while(!(exit.length()==1 && (exit.equalsIgnoreCase("y") || exit.equalsIgnoreCase("n"))));
			
		}while(exit.equalsIgnoreCase("y"));
		
		System.out.println();
		System.out.println();
		System.out.println("Computer Dice Results");
		System.out.println("---------------------");
		System.out.println("You played "+rounds+" rounds");
		System.out.println();
		System.out.println("Rounds won\t: "+ win);
		System.out.println("Rounds lost\t: "+ lose);
		System.out.println("---------------------");
		
		stdIn.close();
			
	}

}
