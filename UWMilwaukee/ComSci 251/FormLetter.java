import java.io.*;
import java.util.*;

public class FormLetter {
	private String filename;
	
	public FormLetter(String filename){
		this.filename = filename;
	}
	
	public String getFilename(){
		return this.filename;
	}
	
	public boolean fileForm(DbEntry e, String outputFilename){
		int line=0;
		
		boolean success=false;
		
		try{
			PrintWriter fileOut = new PrintWriter(outputFilename);
			Scanner fileIn = new Scanner(new FileReader(filename));

			while(fileIn.hasNextLine()){
				
				String outLine="",inLine="";
				inLine=fileIn.nextLine();
				line++;
				int number=0;
					
					if(inLine.indexOf('#')!=-1)
					{
						outLine+=inLine.substring(0,inLine.indexOf('#'));
						String blanks=inLine.substring(inLine.indexOf('#'), inLine.lastIndexOf('#')+1);
						String[] aBlank=blanks.split(" ");
						
						for(int index=0;index<aBlank.length;index++){
							String num="";
							
							//System.out.println("before num="+num);
							try{
							num=aBlank[index].substring(3,4);
							//System.out.println("after num="+num);
							if(!Character.isDigit(num.charAt(0))){
								//number=-1;
								throw new StringIndexOutOfBoundsException();
							}
							
							}catch(StringIndexOutOfBoundsException y){
								System.out.println(line+": Poorly formatted blank marker; Skipping.");
								outLine+=" ";
								//System.out.println("StringIndexOutOfBoundsException");
							}
							
							try{
								number=Integer.parseInt(num);
								try{
									
									if(e.getField(number)==null)
										throw new IndexOutOfBoundsException();
									
									outLine+=" "+e.getField(number);
								}catch(IndexOutOfBoundsException w){
									System.out.println(line+": No field #"+number+" in entry "+e.toString()+" Using \"\"");
									outLine+=" ";
								}
								//System.out.println(number);
							}catch(NumberFormatException z){
								System.out.println(line+": Poorly formatted blank number; Skipping.");
								outLine+=" ";
							}
							
							
						}
						
					}
					else
						outLine+=inLine;
					
					if(inLine.lastIndexOf('#')!=-1)
						outLine+=inLine.substring(inLine.lastIndexOf('#')+1);
			
					fileOut.println(outLine);
					success=true;
			}
			fileIn.close();
			fileOut.close();
		}catch(FileNotFoundException x){
			System.out.println("Could not open file! "+filename+"(No such file or directory)");
			success=false;
		}
		finally{
			return success;
		}
	}
}
