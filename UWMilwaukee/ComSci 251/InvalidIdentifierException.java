
public class InvalidIdentifierException extends RuntimeException {
	private String identifierText;
	public InvalidIdentifierException(String text){
		identifierText=text;
	}
	public String GetIDText(){
		return identifierText;
	}
}
