import java.util.Scanner;

public class my_solution {
  public static void main(String[] args){
    Scanner sc = new Scanner(System.in);
    int cases = sc.nextInt();
    while(cases-- > 0){
      int N = sc.nextInt();
      int L = sc.nextInt();

      int[] costList = new int[N];
      for(int i=0; i<costList.length; i++){
        costList[i] = sc.nextInt();
      }
      double minAvg = Double.MAX_VALUE;
      for(int m=L;m<=N;m++){
      	for(int k=0; k<N-m+1;k++){
      		double avarage = 0;
      		double total = 0;
      		for(int count=0;count<m;count++){
      			total+=costList[count+k];
      		}
      		avarage = total/m;
      		if(avarage < minAvg){
      			minAvg = avarage;
      		}
      	}
      }
      System.out.println(minAvg);
    }
  }
}
