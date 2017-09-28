import java.util.Scanner;
import java.util.ArrayList;
import java.util.Iterator;

public class Main {

	public static void main(String[] args){
		Scanner sc = new Scanner(System.in);
		int cases = sc.nextInt();
		sc.nextLine();
		while(cases-- > 0){
			String input = sc.nextLine();
			ArrayList<Character> chList=new ArrayList<Character>();
			for(int i=0;i<input.length();i++){
				chList.add(input.charAt(i));
			}
			Iterator<Character> it = chList.iterator();

			System.out.println(reverse(it));
		}
		sc.close();
	}

	public static String reverse(Iterator<Character> it){
		Character pos;
		if(!it.hasNext())
			return "";
		pos = it.next();
		if(pos == 'b' || pos == 'w')
			return ""+pos;
		String UpLeft=reverse(it);
		String UpRight=reverse(it);
		String DownLeft=reverse(it);
		String DownRight=reverse(it);
		return "x"+DownLeft+DownRight+UpLeft+UpRight;
	}
}
