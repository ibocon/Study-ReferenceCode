import java.util.Scanner;
import java.util.ArrayList;
public class TTTBoardDriver {

	public static void main(String[] args) 
	{
		Scanner stdIn=new Scanner(System.in);
		System.out.println("Welcome to the Tic-Tac-Toe Board Generator\n");
		System.out.print("Enter board size (at least 2): ");
		int size=stdIn.nextInt();
		
		while(size<2)
		{
			System.out.println("ERROR: Board size must be at least 2");
			System.out.print("Enter board size (at least 2): ");
			size=stdIn.nextInt();
		}
		
		System.out.print("Enter number of boareds to generate: ");
		int numOfGen=stdIn.nextInt();
		while(numOfGen<1)
		{
			System.out.println("ERROR: Must generate at least one board");
			System.out.print("Enter number of boareds to generate: ");
			numOfGen=stdIn.nextInt();
		}
		TTTBoard[] board = new TTTBoard[numOfGen];
		for(int i=0;i<numOfGen;i++)
		{
			board[i]= new TTTBoard(size);
			board[i].fillWithRandomXAndO();
		}
		
		System.out.println();
		ArrayList<TTTBoard> uniqueBoard = new ArrayList<>();
		uniqueBoard.add(board[0]);
		//----------------------------------------
		for(int i=1;i<numOfGen;i++)//generated unique board 
		{
			boolean C=true;
			for(int j=0; j<uniqueBoard.size(); j++)//problem of changing uniqueBoard.size;
			{
				if(board[i].equals(uniqueBoard.get(j)))
				{
					C=false;
				}
			}
			if(C)
			{
				uniqueBoard.add(board[i]);
			}
		}
		//----------------------------------------
		
		System.out.println("Number of unique boards generated = "+uniqueBoard.size());
		System.out.print("Would you like to see all of the unique boards (y/n): ");
		String ans=stdIn.next();
		
		while(!(ans.equalsIgnoreCase("y")||ans.equalsIgnoreCase("n")))
		{
			System.out.println("Only 'y'/'n' can be answer.");
			System.out.print("Would you like to see all of the unique boards (y/n): ");
			ans=stdIn.next();
		}
		
		if(ans.equalsIgnoreCase("y"))
		{
			
			
			for(int i=0;i<uniqueBoard.size();i++)
			{
				int loc=0;
				System.out.println("Unique Board #"+i);
				for(int j=0; j<size; j++)
				{
					System.out.println(uniqueBoard.get(i).toString().substring(loc, loc+size));
					loc+=size;
				}
				System.out.println();
			}

		}
		
		System.out.println("Goodbye!");
	

	}

}
