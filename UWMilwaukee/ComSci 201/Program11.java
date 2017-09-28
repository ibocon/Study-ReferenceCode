import java.util.Scanner;
import java.util.Arrays;
public class Program11 
{
	public static void main(String[] args) 
	{
		Scanner stdIn = new Scanner (System.in);
		String [] words = new String [100];
		System.out.println("Welcome to WordList!");
		System.out.println("--------------------\n");
		int answer, numWords = 0;
		String word;
		boolean rooping;
		do
		{
			answer = getMenuChoice(stdIn);
			if(answer == 1)
			{
				do
				{
					System.out.print("Enter a word to add to the wordList: ");
					word = stdIn.next();
			
					if(addWord(words, numWords, word))
					{
						System.out.println(word+" is has been added");
						numWords++;
						rooping = false;
					}
					else
					{
						System.out.println(word+" is already present");
						rooping = true;
					}
				}while(rooping);
				
			}
			else if(answer == 2)
			{
				do
				{
					System.out.print("Enter a word to remove to the wordList: ");
					word = stdIn.next();
			
					if(removeWord(words, numWords, word))
					{
						System.out.println(word+" is has been removed");
						numWords--;
						rooping = false;
					}
					else
					{
						System.out.println(word+" is not present");
						rooping = true;
					}
				}while(rooping);
				
			}
			else if(answer == 3)
			{
				printWords(words, numWords);
				System.out.println();
			}
			System.out.println();
		}while(answer!=4);
				
		
	}
	public static boolean addWord(String[] words, int numWords, String word)
	{
		if(findWord(words, numWords, word) == -1)
		{
			words[numWords] = word;
			return true;
			
		}
		else
		{
			return false;
		}
	}
	public static boolean removeWord(String[] words, int numWords, String word)
	{
		
		if(findWord(words, numWords, word)!= -1)
		{
			for(int j = findWord(words, numWords, word); 
					j<numWords;
					j++)
			{
				words[j] = words[j+1];
			}
			return true;
		}
		else
			return false;
	}
	public static void printWords(String[] words, int numWords)
	{
		
		sort(words, numWords);
		
	System.out.print("[");
		for(int i = 0; i<numWords-1; i++)
			System.out.print(words[i] + ", ");
		System.out.print(words[numWords-1]+"]");
	}
	private static int findWord(String[] words, int numWords, String word)
	{
		//Problem
		for(int index=0; index<numWords; index++)
		{
			if(words[index].equals(word))
			{
				return index;
			}
		}
		
		return -1;
	}
	private static int getMenuChoice(Scanner stdIn)
	{
		int answer;
		System.out.println("1. Add Word");
		System.out.println("2. Remove Word");
		System.out.println("3. Print Words");
		System.out.println("4. Quit");
		do
		{
			System.out.print("Choose an option(1-4) : ");
			answer = stdIn.nextInt();
		}while(answer < 1 || answer > 4);
		
		return answer;
	}
	
	public static void sort(String[] words, int numWords)
	{
		int j; // index of smallest value
		for (int i=0; i<numWords; i++)
		{
			j = indexOfNextSmallest(words, i, numWords);
			swap(words, i, j);
		}
	} // end sort
	private static void swap(String[] words, int i, int j)
	{
		String temp; // temporary holder for number
		temp = words[i];
		words[i] = words[j];
		words[j] = temp;
	} // end swap
	private static int indexOfNextSmallest(String[] words, int startIndex, int numWords)
	{
		int minIndex = startIndex; // index of smallest value
		for (int i=startIndex+1; i<numWords; i++)
		{
			if(words[i].compareTo(words[minIndex]) < 0)
			{
				minIndex = i;
			}
		} // end for
		return minIndex;
	} // end indexOfNextSmallest
	
}
