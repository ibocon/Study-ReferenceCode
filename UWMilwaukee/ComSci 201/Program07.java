/*************************************
 * Program07.java
 * @author Ye gun Kim
 * 
 * the user plays a simple two dice 
 * game of chance against the computer.
 *************************************/

import java.util.Scanner;
public class Program07 
{
	public static void main(String[] args)
	{
		Scanner stdIn= new Scanner(System.in);
		String answer;
		int pd1, pd2;
		int cd1, cd2;
		int win=0, lose=0;
		
		System.out.println("\tWelcome to Computer Dice");
		System.out.println("-----------------------------------------------");
		System.out.println("You will be playing dice against the computer");
		System.out.println();
		System.out.println("you can only win with a Pair or a Straight");
		System.out.println("any Pair beats any Straight");
		System.out.println("any Pair beats any Junker");
		System.out.println("any Straight beats any Junker");
		System.out.println("in case of two Pairs - high pair wins");
		System.out.println("in case of two Straights - high Straight wins");
		System.out.println("in the case of a tie, you lose");
		System.out.println("-----------------------------------------------");
		System.out.println();
		
		do
		{
			pd1=(int)((Math.random()*6)+1);
			pd2=(int)((Math.random()*6)+1);
			cd1=(int)((Math.random()*6)+1);
			cd2=(int)((Math.random()*6)+1);
			
			System.out.println();
			System.out.println(" Player");
			System.out.println("--------");
			System.out.println("  "+ pd1 + "  " + pd2);
			System.out.println();
			System.out.println(" Opponent");
			System.out.println("--------");
			System.out.println("  "+ cd1 + "  " + cd2);
			System.out.println();
			
			if(pd1==pd2)
			{
				if(cd1==cd2)
				{
					if(pd1<=cd1)
					{
						lose++;
						System.out.println("Sorry, you lose!");
					}
					else
					{
						win++;
						System.out.println("Congrats, you win!");
					}
					
				}
				else
				{
					win++;
					System.out.println("Congrats, you win!");
				}
				
				
			}
			else if(Math.abs(pd1-pd2)==1) //how to use abs here?
			{
					if(cd1==cd2)
					{
						lose++;
						System.out.println("Sorry, you lose!");
					}
					else if(Math.abs(pd1-pd2)==1)
					{
						if(Math.max(pd1, pd2) <= Math.max(cd1, cd2))
						{
							lose++;
							System.out.println("Sorry, you lose!");
						}
						else
						{
							win++;
							System.out.println("Congrats, you win!");
						}
					}
					else
					{
						win++;
						System.out.println("Congrats, you win!");
					}
			}
			else
			{
				lose++;
				System.out.println("Sorry, you lose!");
			}
			
			System.out.println();
			
			do
			{
				System.out.print("Do you wish to play again [y, n] : ");
				answer = stdIn.nextLine();
			
			}while(!(answer.length()==1 && (answer.equalsIgnoreCase("y")||answer.equalsIgnoreCase("n"))));
			
			
		}while(answer.equalsIgnoreCase("y"));
		
		System.out.println();
		System.out.println("Computer Dice Results");
		System.out.println("---------------------");
		System.out.println("You Played " + (win+lose) + " rounds");
		System.out.println();
		System.out.println();
		System.out.println("Rounds won : " + win);
		System.out.println("Rounds lost : " + lose);
		System.out.println("---------------------");
		
		
		
		stdIn.close();
	}
}
