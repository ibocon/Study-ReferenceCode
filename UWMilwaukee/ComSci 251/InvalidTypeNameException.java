
public class InvalidTypeNameException extends RuntimeException {
	private String identifierText;
	public InvalidTypeNameException(String text){
		identifierText=text;
	}
	public String GetIDText(){
		return identifierText;
	}
}
