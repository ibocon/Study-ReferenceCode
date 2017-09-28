import java.util.Scanner;

public class DeclParser {
	public static void main(String args[]) {
		Scanner sc = new Scanner(System.in);
		System.out.println("Welcome to the Pascal declaration parser!\n");
		
			System.out.println("Enter one line of Pascal-style declarations:");
			String inputLine = sc.nextLine().trim();
		
		if (inputLine.isEmpty()) {
			System.out.println("ERROR: Input line is empty");
		}
		else {
			String[] statements = inputLine.split(";", -1);

			for (int i=0; i < statements.length - 1; i++) {
				System.out.println("\nDeclaration Statement " + (i+1) + ":");
				TryCatch(statements[i]);
			}	
			if (!statements[statements.length-1].isEmpty()) {
				System.out.println("\nDeclaration Statement " + statements.length + ":");
				System.out.println("ERROR: Missing semicolon");
				TryCatch(statements[statements.length-1]);
			}
		}
		System.out.println("\nGoodbye!");
	}

	public static void parseAndDescribeDeclStatement(String decl) {
		int colonPosition = decl.indexOf(":");
		if (colonPosition == -1) {
			throw new MissingColonException("Exception: Missing colon"); //1 error MissingColonException
		}
		String variableList = decl.substring(0,colonPosition);
		String typeName = decl.substring(colonPosition+1).trim();
		String validatedTypeName = validateTypeName(typeName);
		String[] rawVariableNames = variableList.split(",");
		for (int i = 0; i < rawVariableNames.length; i++) {
			String varName = rawVariableNames[i].trim();
			if (!isValidVariableName(varName)) {
				throw new InvalidIdentifierException("Exception: Invalid variable name \"" + varName +"\""); //1 error InvalidIdentifierException
			}
			System.out.println("Variable " + (i+1) + ": " + varName + " (" +
					validatedTypeName + ")" );
		}
	}

	public static String validateTypeName (String typeName) {
		if (typeName.equals("integer") || typeName.equals("real") || typeName.equals("boolean"))
			return typeName;
		else {
			throw new InvalidTypeNameException("Exception: it is not a valid type name"); //1 error InvalidTypeNameException
		}
	}

	public static boolean isValidVariableName(String varName) {
		if (varName.isEmpty())
			return false;
		char firstChar = varName.charAt(0);
		if (! (Character.isLetter(firstChar) || firstChar == '_'))
			return false;
		for (int i = 1; i < varName.length(); i++) {
			char currentChar = varName.charAt(i);
			if (! (Character.isLetter(currentChar) || Character.isDigit(currentChar) || currentChar == '_'))
				return false;
		}
		return true;
	}
	//Question: Why are two try/catch blocks necessary? Answer: because, after Exception, the program start after catch block. It could skip program code. 
	public static void TryCatch(String dec1){
		try{
			parseAndDescribeDeclStatement(dec1);
		}
		catch(InvalidIdentifierException e){
			System.out.println(e.GetIDText());
		}
		catch(InvalidTypeNameException e){
			System.out.println(e.GetIDText());
		}
		catch(MissingColonException e){
			System.out.println(e.GetIDText());
		}
		catch(Exception e){
			System.out.println("Error in 'TryCatch' Block");
		}
	}
}