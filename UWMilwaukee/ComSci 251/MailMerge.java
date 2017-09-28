import java.util.*;
import java.io.*;
public class MailMerge {

	public static void main(String[] args) {
		// TODO Auto-generated method stub
		System.out.println("Mail Merge");
		System.out.println("============");
		Scanner stdIn=new Scanner(System.in);
		boolean exit=false;
		//String data = "";
		System.out.print("Enter name of database file: ");
		String data=stdIn.nextLine();
		//data="names.txt";
		ArrayList<DbEntry> n=new ArrayList<DbEntry>();

		while(!exit){
			try{
				Scanner dataIn = new Scanner(new FileReader(data));
				//int index=0;
				String before="";
				while(dataIn.hasNextLine()){
					before=dataIn.nextLine();
					//System.out.println(before);
					n.add(new DbEntry(before));
					//System.out.println(n.get(index).toString());
					//index++;
				}
				System.out.print("Enter name of form file: ");
				String form=stdIn.nextLine();
				//String form="offer.txt";
				boolean makeFile=false;
				for(int i=0; i<n.size();i++){
					System.out.println("Generating form letter #"+(i+1)+"...");
					FormLetter a = new FormLetter(form);
					makeFile=a.fileForm(n.get(i),form.substring(0,form.indexOf('.'))+(i+1)+".txt");
					//System.out.println("makeFile ="+makeFile);
					if(!makeFile)
						System.out.println("Unable to generate form letter #"+(i+1));
				}
				System.out.println("Merge completed.");
				exit=true;
			}catch(FileNotFoundException e){
				System.out.println("Unable to open file: \""+data+"\"");
				System.out.print("Enter different filename, or 'q' to quit: ");
				data=stdIn.nextLine();
				if(data.equalsIgnoreCase("q"))
					exit=true;
			}
		}//End while
		
		System.out.println("Goodbye!");
	}//End class

}
