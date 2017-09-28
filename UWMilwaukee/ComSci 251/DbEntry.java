import java.util.*;

public class DbEntry {
	private ArrayList<String> fields;
	
	public DbEntry(String x){
		//System.out.println("DbEntry Constructor");
		int p=0;
		fields=new ArrayList<>();
		fields.add(null);
		while(p<x.lastIndexOf(':')){
			
			fields.add(x.substring(p+1,x.indexOf(':',p+1)));
			//System.out.println("p="+p+"&& x.indexOf(':'),p+1)="+x.indexOf(':',p+1));
			p=x.indexOf(':',p+1);
			//System.out.println("Changed p ="+ p);
		}
	}
		
	public String getField(int field)throws IndexOutOfBoundsException{
		//System.out.println("DbEntry's getField["+field+"]: " + fields.get(field));
		return fields.get(field);
	}
	public String toString(){
		String k=":";
		for(int index=1; index<fields.size();index++)
			k+=fields.get(index)+":";
		//System.out.println("DbEntry's toString : "+k);
		return k;
	}
}
