import java.util.Scanner;
public class PascalDeclParser2 
{

	public static void main(String[] args) 
	{
		System.out.println("Welcome to the Pascall declaration parser!\n");
		System.out.println("Enter one line of Pascall-style declarations : ");
		Scanner stdIn=new Scanner(System.in);
		String decl = stdIn.nextLine();
		System.out.println(decl);
		//String decl = "a ,b, c: integer; d,e :real ;";
		//String decl = "a , b,c :integer ; oneReally_Incredibly_Long_Name_123456789, d , e: real;";
		//String decl = "99,ab-c:float;; d,e:f:boolean ; g,h: junk type";
		
		String[] sentence = decl.split(";");
		System.out.println();
		for(int i=0; i<sentence.length; i++)
		{
			System.out.println("Declaration Statemnet " +(i+1)+":");
			sentence[i]=sentence[i].trim();
			if(decl.charAt(decl.length()-1)!=';'&&i==sentence.length-1)
				System.out.println("ERROR: Missing semicolon");
			if(!sentence[i].equals(""))
				parseAndDescribeDeclStatement(sentence[i]);
			else
				System.out.println("ERROR: Missing colon\n");
				
		}
			
		
		System.out.println("Goodbye!");
		//stdIn.close();
	}
	//-----------------------------------------------------------------
	public static void parseAndDescribeDeclStatement(String decl)
	{
		//handle single declaration statement (decl - removed semicolon and trimmed
		
		//-------------------------

		if(decl.indexOf(":")==-1)
			System.out.println("ERROR: Missing colon");
		//--------------------------
		String typeName = decl.substring(decl.indexOf(":")+1);
		typeName=typeName.trim();
		validateTypeName(typeName);
		if(validateTypeName(typeName).equals("<invalid type name>"))
			System.out.println("ERROR: \""+typeName+"\" is not a valid type name");
			
		String varNames = decl.substring(0, decl.indexOf(":"));
		String[] varName = varNames.split(",");
		for(int j=0; j<varName.length; j++)
		{
			varName[j]=varName[j].trim();
			
			if(!isValidVariableName(varName[j]))
				System.out.println("ERROR: Invalid variable name \""+varName[j]+"\"");
				
			System.out.println("Variable "+(j+1)+": "+varName[j]+" ("+validateTypeName(typeName)+")");
		}
		
		System.out.println();
	}
	//-----------------------------------------------------------------
	public static String validateTypeName (String typeName)
	{
		if(typeName.equals("integer")||typeName.equals("real")||typeName.equals("boolean"))
			return typeName;
		return "<invalid type name>";
	}
	//-----------------------------------------------------------------
	public static boolean isValidVariableName(String varName)
	{
		if(Character.isDigit(varName.charAt(0)))
			return false;
		for(int i=0; i<varName.length(); i++ )
		{
			if(!(Character.isLetter(varName.charAt(i))||Character.isDigit(varName.charAt(i))||varName.charAt(i)=='_'))
			{
				return false;
			}
		}
		return true;
	}
	//-----------------------------------------------------------------
}
