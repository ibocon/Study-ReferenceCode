
public class MissingColonException extends RuntimeException {
	private String identifierText;
	public MissingColonException(String text){
		identifierText=text;
	}
	public String GetIDText(){
		return identifierText;
	}
}
