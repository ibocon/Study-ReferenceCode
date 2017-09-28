
public class TTTBoard 
{
	private char[][] board;
	private int size;
	
	public TTTBoard(int boardSize)
	{
		this.size=boardSize;
		board=new char[size][size];
		
		for(int i=0; i<size;i++)
			for(int j=0; j<size;j++)
				setCellChar(i,j,'-');
	}
	//---------------------------------------
	public void fillWithRandomXAndO()
	{	
		int numX = (size * size) / 2;
		int numO = numX;
		
		if ((size*size) % 2 == 1) 
		{
			if ((int)(Math.random() * 2) == 0) 
			{
				numX ++;
			} 
			else 
			{
				numO ++;
			}
		}
		
		while (numX > 0) {
			int x = (int)(Math.random() * size);
			int y = (int)(Math.random() * size);
			if (board[x][y] == '-') {
				setCellChar(x,y,'X');
				numX--;
			}
		}
		
		while (numO > 0) {
			int x = (int)(Math.random() * size);
			int y = (int)(Math.random() * size);
			if (board[x][y] == '-') {
				setCellChar(x,y,'O');
				numO--;
			}
		}
	}
	//---------------------------------------
	public char getCellChar(int rowNum,int columnNum)
	{
		return board[rowNum][columnNum];
	}
	public void setCellChar(int rowNum,int columnNum,char xo)
	{
		if(!(xo=='o' || xo=='O' || xo=='-'||xo=='x'||xo=='X'))
			return;
		else
			board[rowNum][columnNum]=xo;
	}
	public boolean equals(TTTBoard t2)
	{
		if(this.size == t2.size)//compare t1.size & t2.size
		{
			for(int i=0; i<size;i++)
				for(int j=0; j<size;j++)
					if(board[i][j]!=t2.getCellChar(i,j))
						return false;
			
			return true;
		}	
		else
			return false;
	}
	public String toString()
	{
		String S="";
		for(int i=0; i<size;i++)
			for(int j=0; j<size;j++)
				S+=board[i][j];
		return S;
	}

}
