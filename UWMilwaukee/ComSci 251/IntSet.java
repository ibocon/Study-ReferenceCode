import java.util.ArrayList;
import java.util.Scanner;
public class IntSet {
	private ArrayList<Integer> members;
	
	public IntSet()
	{
		members=new ArrayList<>();
	}
	public IntSet(int a)
	{
		members=new ArrayList<>();
		members.add(a);
	}
	public IntSet(IntSet original)
	{
		members=new ArrayList<>();
		for(int i=0; i<original.size(); i++)
			members.add(original.get(i));
	}
	
	public int size()
	{
		return members.size();
	}
	public int get (int index)
	{
		if(members.size()>index)
			return members.get(index);
		else
			return Integer.MAX_VALUE;
	}
	
	public void add(int a)
	{
		if(members.isEmpty())
			members.add(a);
		else
		{
			for(Integer x : members)
				if(x==a)
					return;
		//add -> sort
			members.add(a);
			sort();
		}
			
	}
	public void remove(int a)
	{
		if(members.indexOf(a)>-1)
			members.remove(members.indexOf(a));
		else
			return;
	}
	public boolean equals(IntSet other)
	{
		if(members.size()==other.size()) 
		{
			for(int i=0; i<members.size(); i++)
			{
				if(members.get(i)!=other.get(i))
					return false;
			}
			return true;
		}
		else
			return false;
	}
	//-----------------------------------------------
	public IntSet plus (IntSet other)
	{
		IntSet plus = new IntSet(this.minus(other));
		//System.out.println("before plus = " + plus.toString());
		for(int i=0; i<other.size(); i++)
			plus.add(other.get(i));
		plus.sort();
		//System.out.println("after plus = " + plus.toString());
		return plus;
	}
	public IntSet minus (IntSet other)
	{
		IntSet minus=new IntSet();
		for(int i=0; i<members.size();i++)
			minus.add(members.get(i));
		//System.out.println(this.toString()+"="+minus.toString());
		IntSet inter=new IntSet(this.intersection(other));
		//System.out.println("\ninter = " + inter.toString());
		int p=0;
		for(int i=0; i<inter.size(); i++)
		{
			//System.out.println("p = "+p+"\ni = "+ i);
			for(int j=p; j<minus.size();j++)
			{
				//System.out.println("enter j = " + j);
				if(inter.get(i)==minus.get(j))
				{
					//System.out.println("before remove, p = "+p+" j = "+ j);
					//System.out.println("before remove, minus ="+minus.toString()+" with size = "+minus.size());
					minus.remove(inter.get(i));
					//System.out.println("after remove, minus ="+minus.toString()+" with size = "+minus.size());
					p=j;
					j=minus.size();
					//System.out.println("after chage, p = "+p+" j = "+ j);
				}
			}
		}
		//System.out.println("after finish, minus = " + minus.toString());
		return minus;
	}
	public IntSet intersection (IntSet other)
	{
		IntSet inter = new IntSet();
		boolean in = true;
		for(int i=0, j=0;in; )
		{
			if(i==members.size()-1&&j==other.size()-1)
			{
				in= false;
			}
			if(members.get(i)==other.get(j))
			{
				inter.add(members.get(i));
				i=(i<members.size()-1? i+1:i);
				j=(j<other.size()-1? j+1:j);
			}
			else if(members.get(i)>other.get(j))
				j=(j<other.size()-1? j+1:j);
			else if(members.get(i)<other.get(j))
				i=(i<members.size()-1? i+1:i);
			else
				System.out.println("Error");
		}
		return inter;
	}
	//--------------------------------------------------
	
	public void readSet (Scanner s)
	{
		String k="";
		while(!k.equals("}"))
		{
			k=s.next();
			
			if(k.equals("}")||k.equals("{")||k.equals(" "))
				;
			else if(members.indexOf(Integer.parseInt(k))>-1)
				;
			else
				members.add(Integer.parseInt(k));
			
		}
		sort();
	}
	
	public String toString()
	{
		String x="{";
		for(Integer k : members)
			x+=(" "+k);
		x+=" }";		
		return x;
	}
	
	private void sort()
	{
		for(int i=1; i<members.size(); i++)
		{
			int temp = members.get(i);
			int check = i-1;
			
			while ((check>=0)&&(members.get(check)>temp))
			{
				members.set(check+1, members.get(check));
				check--;
			}
			members.set(check+1, temp);
		}
	}

		
}

