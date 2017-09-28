import java.util.*;
public class PA7 {
	
	public static void main(String[] args)
	{
		Scanner stdIn=new Scanner(System.in);
		ArrayList<Shape> shape=new ArrayList<>();
		int index=0;
		
		System.out.println("@@@Defualt Testing@@@\n");
		shape.add(new Point());
		shape.add(new Circle());
		shape.add(new Rectangle());
		for(int i=0; i<shape.size();i++)
			System.out.println("Shape "+i+"="+shape.get(i).toString());

		shape.clear();
		System.out.println("\nShape.clear()\n" +"shape.size() = "+shape.size());
		
		System.out.println("\n@@@Start@@@");
		boolean exit=false;
		
		while(!exit)
		{
			try
			{
				System.out.print("\nChoose\n"
					+ "\t1.Point\n"
					+ "\t2.Circle\n"
					+ "\t3.Rectangle\n"
					+ "\t4.Exit\n"
					+ "Answer : ");
				int answer=stdIn.nextInt();
				switch(answer)
				{
					case 1:
							System.out.println("<Point>");
							System.out.print("Point's X: ");
							double px=stdIn.nextDouble();
							System.out.print("Point's Y: ");
							double py=stdIn.nextDouble();
							shape.add(new Point(px,py));
							System.out.println("Shape ["+index+"] = " + shape.get(index).toString());
							index++;
							break;
					case 2:
							System.out.println("<Circle>");
							System.out.print("Circle's name: ");
							String name=stdIn.next();
							System.out.println("Circle's center: ");
							System.out.print("\tCircle's X: ");
							double cx=stdIn.nextInt();
							System.out.print("\tCircle's Y: ");
							double cy=stdIn.nextInt();
							Point center=new Point(cx,cy);
							System.out.print("Circle's radius: ");
							double radius=stdIn.nextDouble();
							shape.add(new Circle(name,center,radius));
							System.out.println("Shape ["+index+"] = " + shape.get(index).toString());
							index++;
							break;
					case 3:
							System.out.println("<Rectangle>");
							System.out.print("Rectangle's name: ");
							String Rname=stdIn.next();
							System.out.println("Rectangle's UL, LR: ");
							System.out.print("\tUL X: ");
							double x1=stdIn.nextDouble();
							System.out.print("\tUL Y: ");
							double y1=stdIn.nextDouble();
							Point UL=new Point(x1,y1);
							System.out.print("\tLR X: ");
							double x2=stdIn.nextDouble();
							System.out.print("\tLR Y: ");
							double y2=stdIn.nextDouble();
							Point LR=new Point(x2,y2);
							shape.add(new Rectangle(Rname,UL,LR));
							System.out.println("Shape ["+index+"] = " + shape.get(index).toString());
							index++;
							break;
					case 4:
							System.out.println("<Exit>");
							System.out.println("Shape.size = "+shape.size());
							for(int i=0; i<shape.size();i++)
								System.out.println("Shape ["+i+"] = "+shape.get(i).toString());
					
							exit=true;
							break;
					}
			}
			catch(Exception e){
				System.out.println("Error!");
				System.out.println(e.getClass().getName());
				stdIn.nextLine();
			}
		}//End while

		//Number 5
		System.out.println("\n@@@Coner@@@\n");
		for(int i=0; i<shape.size();i++)
			if(shape.get(i) instanceof Rectangle)
			{
				Rectangle R = (Rectangle) shape.get(i);
				System.out.println("Rectangle's UL = "+R.getUL());
				System.out.println("Rectangle's UR = "+R.getUR());
				System.out.println("Rectangle's LL = "+R.getLL());
				System.out.println("Rectangle's LR = "+R.getLR());
			}
		
		//Number 6
		System.out.println("\n@@@Area@@@\n");
		for(int i=0;i<shape.size();i++)
			System.out.println("Shape["+i+"]'s Area = "+shape.get(i).area());
		//Number 7
		System.out.println("\n@@@Contains@@@\n");
		System.out.println("Enter a Point: ");
			System.out.print("Contain's X: ");
			double sx=stdIn.nextDouble();
			System.out.print("Contain's Y: ");
			double sy=stdIn.nextDouble();
			Point s=new Point(sx,sy);
			for(int i=0;i<shape.size();i++)
				if(shape.get(i).contains(s))
					System.out.println("Shape["+i+"] contains the point = "+s.toString());
				else
					System.out.println("Shape["+i+"] NOT contains the point = "+s.toString());
		
	}

}

