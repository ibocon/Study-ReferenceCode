import java.util.Scanner;
public class Filter 
{
	Scanner stdIn = new Scanner(System.in);
	//-------------------------------------------------------
	 double 	D0 = 6.58, A1 = -0.696, A2 = 0.378, x =	0.0, x1 = 0.0, x2 = 0.0, y, y1 = 0.0, y2 = 0.0;
	//-------------------------------------------------------
	public void initialize()
	{
		System.out.print("Enter D0 : ");
		this.D0 = stdIn.nextDouble();
		
		if(D0 == 0)
		{
			this.D0 = 6.57;
			this.A1 = -0.696;
			this.A2 = 0.378;
		}
		else
		{
			System.out.print("Enter A1 : ");
			this.A1 = stdIn.nextDouble();
			System.out.print("Enter A2 : ");
			this.A2 = stdIn.nextDouble();
		}
		
		return;
	}
	//-------------------------------------------------------
	public double findY(double input)
	{
		return ((input+2*this.x1+this.x2)/this.D0)-this.A1*this.y1-this.A2*this.y2;
	}
	//-------------------------------------------------------
	public void step()
	{
		x2=x1;
		x1=x;
		
		y2=y1;
		y1=y;
		return;
	}
}