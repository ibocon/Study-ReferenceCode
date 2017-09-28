

	/**
	 * Program09
	 * @author Ye gun Kim
	 *
	 *There is a "fun" children's game where one child thinks of a "common phrase", then the second child
	repeatedly makes guesses as to the letters that it contains.
	 **/

	import java.util.Scanner;

	public class Program09
	{
		public static void main(String[] args)
		{
			Scanner stdIn=new Scanner(System.in);
			String cp;
			char guess;
			int space=0, consonant=0, vowel=0;
			
			System.out.print("Please enter the phrase to guess at : ");
			cp = stdIn.nextLine();
			char template [] = new char [cp.length()];
		
			space = initTemplateArray(cp, template);
		
			printTemplateArray(template);
			System.out.println();
			guess = getConsonant(stdIn);
			System.out.println();
			
			consonant=updateTemplateArray(template, cp, guess);
			
			while(!cp.equals(new String(template)))
			{
				printTemplateArray(template);
				System.out.println();
				String ans;
				do
				{
					System.out.print("Would you like to buy a vowel : ");
					ans = stdIn.next();
					
				}while(!ans.equals("y") && !ans.equals("n"));
				
				System.out.println();
				
				if(ans.equals("y"))
				{
					guess = getVowel(stdIn);
					vowel+=updateTemplateArray(template, cp, guess);
				}
				else if(ans.equals("n")) // readability
				{
					guess = getConsonant(stdIn);
					consonant+=updateTemplateArray(template, cp, guess);
				}
					
			}
			
			printTemplateArray(template);
			System.out.println();
			System.out.println("The common phrase contained: " + space + " space(s), "+ consonant +" consonsant(s) and "+ vowel +" vowel(s)." );
			stdIn.close();
		}
		
		public static int updateTemplateArray(char [] tmpArr, String sPhrase, char guess)
		{
			int count=0, loc=sPhrase.indexOf(guess);
			
			while(loc!=-1)
			{
				count++;
				tmpArr [loc] = guess;
				loc = sPhrase.indexOf(guess, loc+1);
			}
			System.out.println();
			return count;
		}
			
		public static int initTemplateArray(String sPhrase, char [] tmpArr)
		{
			int space=0;
			for(int i=0; i< sPhrase.length(); i++)
			{
				if(Character.isWhitespace(sPhrase.charAt(i)))
				{
					tmpArr[i] = ' ';
					space++;
				}
				else
					tmpArr[i]='?';
			}
			return space;
		}
		
		public static void printTemplateArray(char [] tmpArr)
		{
			
			System.out.println();
			System.out.println("Common Phrase");
			System.out.println("-------------");
			
			for(int i=0; i<tmpArr.length; i++)
				System.out.print(tmpArr[i]);
			
			System.out.println();
			
		}
		
		public static boolean isVowel(char guess)
		{
			return(guess=='a'||guess=='e'||guess=='i'||guess=='o'||guess=='u');
		}
		
		public static char getVowel(Scanner stdIn) 
		{
			
			String guess;
			
			do
			{
				System.out.print("Enter a lowercase Vowel guess : ");
				guess=stdIn.next();
			}while(guess.length()!=1 || !(guess.charAt(0)>='a')|| !(guess.charAt(0)<='z') ||!isVowel(guess.charAt(0)));
			
			return guess.charAt(0);
		}
		
		public static char getConsonant(Scanner stdIn)
		{
			String guess;
			
			do
			{
				System.out.print("Enter a lowercase consonant guess : ");
				guess=stdIn.next();
			}while(guess.length()!=1 || !(guess.charAt(0)>='a')|| !(guess.charAt(0)<='z') ||isVowel(guess.charAt(0)));
			
			return guess.charAt(0);
		}
			
			
	}


