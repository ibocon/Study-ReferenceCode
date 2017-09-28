/**
 * Program10
 * @author Ye gun Kim
 *
 *You are to develop a program to play a variation on a game of chance called single player Poker.
 */
import java.util.Scanner;;
public class Program10 
{

	public static void main(String[] args) 
	{
		Scanner stdIn = new Scanner(System.in);
		String ans;
		int chips = 100, bet = 0, win = 0, lose = 0;
		int [] deck = new int [36];
		int [] hand = new int [4];
		
		System.out.println("Welcome to 4 Card Poker");
		System.out.println("   Your initial bank roll is $100.00");
		System.out.println("++++++++++++++++++++++++++++++++++++++++++++++");
		System.out.println("\n");
		do
		{
			System.out.print("Do you want to play a game? ");
			ans = stdIn.next();
			
			if(ans.equalsIgnoreCase("y"))
			{
				do
				{
					System.out.print("Place your bet [1, "+chips+"] : ");
					bet = stdIn.nextInt();
		
				}while(!(bet>=1 && bet<=chips));
				System.out.println();
				dealHand(deck, hand);
			
				if(isQuad(hand))
				{
					chips+=(bet*6545);
					System.out.println("Congrats: You got a Quad and have won $"+bet*6545);
					win++;
				}
				else if(isTrip(hand))
				{
					chips+=(bet*51);
					System.out.println("Congrats: You got a Trip and have won $"+bet*51);
					win++;
					
				}
				else if(isStraight(hand))
				{
					chips+=(bet*38);
					System.out.println("Congrats: You got a Straight and have won $"+bet*38);
					win++;
				}
				else if(is2Pair(hand))
				{
					chips+=(bet*22);
					System.out.println("Congrats: You got a 2 Pair and have won $"+bet*22);
					win++;
				}
				else if(isPair(hand))
				{
					System.out.println("Congrats: You got a Pair and have won $"+bet);
					win++;
				}
				else
				{
					chips-=bet;
					System.out.println("Sorry: you got Bubkiss and have lost $"+bet);
					lose++;
				}
				System.out.println();
			}
		
		}while((!ans.equalsIgnoreCase("n") || ans.equalsIgnoreCase("y")) && chips!=0);
		
		
		System.out.println("\n++++++++++++++++++++++++++++++++++++++++++++++");
		
		if(chips==0)
			System.out.println("Party's Over: you are out of chips\n");
		
		System.out.println("Thanks for playing ...\n");
		System.out.println("You played a total of "+(win+lose)+" hands.");
		System.out.println("Of which, you won "+win+".");
		System.out.println("And you lost "+lose+".\n");
		if(chips < 100)
			System.out.println("But in the end you lost $"+ (100 - chips) +", and have $"+ chips +".");
		else if(chips > 100)
			System.out.println("In the end you got $"+chips+"!");
		else
			System.out.println("Even, you maintain $100.");
			
		
	}
	public static void initDeck(int[] deck)
	{
		int num = 0;
		for(int i=0; i<=35; i+=4)
		{
			num++;
			for(int j=i; j<=(i+3); j++)
				deck [j] = num;
		}
				
	}
	public static void shuffleDeck(int[] deck, int n) 
	{
		for(int i = 1; i<=n; i++)
		{
			int index1 = (int)(Math.random()*36), 
				index2 = (int)(Math.random()*36);
			
			int tmp = deck[index1];
			deck[index1]= deck[index2];
			deck[index2] = tmp;
		}
	}
	public static void dealHand(int [] deck, int[] hand) 
	{
		initDeck(deck);
		shuffleDeck(deck, 100);
		for(int i=0; i<=3; i++)
			hand[i]=deck[i];
		System.out.println("Let the cards fall where they may ...\n");
		displayHand(hand);
		System.out.println();
		System.out.println("Let's get things in order ...\n");
		sortHand(hand);
		displayHand(hand);
		System.out.println();
		sortHand(hand);
	}
	public static void sortHand(int[] hand)
	{
		for (int i = 0; i < hand.length; ++i)
		{
			int maxLoc = i;
			for (int j = i+1; j < hand.length; ++j)
				if (hand[j] > hand[maxLoc])
					maxLoc = j;
			
			int tmp = hand[i];
			hand[i] = hand[maxLoc];
			hand[maxLoc] = tmp;
		}
	}
	public static void displayHand(int[] hand)
	{
		System.out.println(hand[0] + " "+hand[1] + " "+hand[2] + " "+hand[3]);
	}
	public static boolean isQuad(int[] hand)
	{
		return (hand[0] == hand[1] && hand[1] == hand[2] && hand[2] == hand[3]);	
	}
	public static boolean isTrip(int[] hand)
	{
		return ((hand[0]==hand[1] && hand[1] == hand[2]) 
				|| (hand[1]==hand[2] && hand[2]==hand[3]));
	}
	public static boolean isStraight(int[] hand)
	{
		return (Math.abs(hand[0]-hand[1]) == 1 && Math.abs(hand[1]-hand[2]) == 1 && Math.abs(hand[2]-hand[3]) == 1 );
	}
	public static boolean is2Pair(int[] hand)
	{
		return (hand[0] == hand[1] && hand[2] == hand[3]);
	}
	public static boolean isPair(int[] hand)
	{
		return (hand[0] == hand[1] || hand[2] == hand[3]);
	}
	
	
}
