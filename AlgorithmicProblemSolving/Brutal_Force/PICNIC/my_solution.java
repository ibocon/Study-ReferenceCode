import java.util.Scanner;

public class my_solution {
	public static void main(String[] args){

		Scanner sc = new Scanner(System.in);
		int cases = sc.nextInt();
		//case start
		while(cases-- > 0){
			//input
			int n = sc.nextInt();
			int m = sc.nextInt();

			boolean[][] areFriends = new boolean[n][n];
			for(int i=0; i<m;i++){
				int a = sc.nextInt();
				int b = sc.nextInt();
				areFriends[a][b]=areFriends[b][a]=true;
			}
			//end input
			//start algorithm
			boolean[] taken = new boolean[n];

			System.out.println(countPairings(taken, areFriends));
		}
	}

	public static int countPairings(boolean[] taken, boolean[][] areFriends){
		int firstFree = -1;
		for(int i=0; i<taken.length;++i){
			if(!taken[i]){
				firstFree=i;
				break;
			}
		}

		if(firstFree == -1) return 1;

		int ret = 0;
		for(int pairWith = firstFree+1; pairWith<taken.length;++pairWith){
			if(!taken[pairWith] && areFriends[firstFree][pairWith]){
				taken[firstFree] = taken[pairWith] = true;
				ret += countPairings(taken,areFriends);
				taken[firstFree] = taken[pairWith] =false;
			}
		}
		return ret;
	}
}
