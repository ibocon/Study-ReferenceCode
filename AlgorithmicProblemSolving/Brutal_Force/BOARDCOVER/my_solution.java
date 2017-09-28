import java.util.Scanner;

public class my_solution {
	static int H;
	static int W;
	static int[][] board;

	final static int[][][] block={
			{{0,0},{1,0},{0,1}},
			{{0,0},{0,1},{1,1}},
			{{0,0},{1,0},{1,1}},
			{{0,0},{1,0},{1,-1}}
	};

	public static void main(String[] args){
		Scanner sc = new Scanner(System.in);
		int cases = sc.nextInt();
		while(cases-- > 0){
			//get inputs
			H = sc.nextInt();
			W = sc.nextInt();
			board=new int[H][W];
			for(int y = 0; y<H; y++){
				String line = sc.next();
				for(int x=0;x<W;x++){
					char ch=line.charAt(x);
					if(ch=='#')
						board[y][x] = 1;
					else
						board[y][x] = 0;
				}
			}
			//start algorithm
			System.out.println(cover());
		}
  }
  //setting flag(delta) is not my idea. I get help from the book.
  //I try to use boolean type for board covered or not.
  public static boolean set(int y, int x, int type, int flag){
	  boolean possible = true;
	  for(int pos=0; pos<3; ++pos){
		  int dy=y+block[type][pos][0];
		  int dx=x+block[type][pos][1];

		  if(dy<0 || dy>=H || dx<0 || dx>=W){
			  //if block get out from board
			  possible=false;
		  }else if((board[dy][dx]+=flag)>1){
			  //check if the block is already covered
			  possible=false;
		  }
	  }
	  return possible;
  }

  public static int cover(){
	  int y=-1;
	  int x=-1;
	  for(int iy=0;iy<H;iy++){
		  for(int ix=0;ix<W;ix++){
			  if(board[iy][ix] == 0){
				  //set origin point to check types
				  y=iy;
				  x=ix;
				  break;
			  }
		  }
		  if(y!=-1 || x!=-1) break;
	  }
	  //can not find uncovered block.
	  //It means already covered every single block.
	  if(y==-1 || x==-1) return 1;

	  int result=0;
	  for(int blockType=0;blockType<4;blockType++){
		  if(set(y,x,blockType,1)){
			  result+=cover();
		  }
		  set(y,x,blockType,-1);
	  }
	  return result;
  }
}
