import java.util.Scanner;

public class Main {

	static int[] clocks=new int[16];
	static boolean[][] connection={
			{true,true,true,false,false,false,false,false,false,false,false,false,false,false,false,false},
			{false,false,false,true,false,false,false,true,false,true,false,true,false,false,false,false},
			{false,false,false,false,true,false,false,false,false,false,true,false,false,false,true,true},
			{true,false,false,false,true,true,true,true,false,false,false,false,false,false,false,false},
			{false,false,false,false,false,false,true,true,true,false,true,false,true,false,false,false},
			{true,false,true,false,false,false,false,false,false,false,false,false,false,false,true,true},
			{false,false,false,true,false,false,false,false,false,false,false,false,false,false,true,true},
			{false,false,false,false,true,true,false,true,false,false,false,false,false,false,true,true},
			{false,true,true,true,true,true,false,false,false,false,false,false,false,false,false,false},
			{false,false,false,true,true,true,false,false,false,true,false,false,false,true,false,false},
	};

	public static void main(String[] args){
		Scanner sc = new Scanner(System.in);
		int cases = sc.nextInt();
		while(cases-- > 0){

			for(int i=0; i<16;i++){
				clocks[i]=sc.nextInt();
			}
			//input check
			/*
			System.out.println("<Clock status>");
			for(int clock:clocks){
				System.out.print(clock+" ");
			}
			System.out.println("\n<Switch status>");

			for(int n=0;n<10;n++){
				System.out.print("swtich("+n+"): ");
				for(int m=0;m<16;m++){
					if(connection[n][m])
						System.out.print(m+" ");
				}
				System.out.println();
			}
			*/
			//FIN input check
			int count=leastSwitch(0);
			if(count<40){
				System.out.println(count);
			}else{
				System.out.println(-1);
			}
		}
	}
	public static int leastSwitch(int type){
		if(type==10){
			if(checkClock())
				return 0;
			else
				return 40;
		}
		int count=40;
		for(int push=0;push<4;push++){
			int cal=push+leastSwitch(type+1);
			if(cal<count)
				count=cal;
			pushSwitch(type);
		}
		return count;
	}

	public static boolean checkClock(){
		for(int i=0;i<16;i++){
			if(clocks[i]!=12)
				return false;
		}
		return true;
	}
	public static void rotateClock(int pos){
		if(clocks[pos]==12)
			clocks[pos]=3;
		else
			clocks[pos]+=3;
	}
	public static void pushSwitch(int type){
		for(int i=0;i<16;i++){
			if(connection[type][i])
			rotateClock(i);
		}
	}
}
