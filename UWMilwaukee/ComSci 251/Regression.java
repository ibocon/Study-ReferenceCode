import java.util.Random;
import java.util.Scanner;
//CompSci251 yegunkim - Regression.java
public class Regression 
{
	
	public static void main(String [] args)
	{
		Scanner stdIn = new Scanner(System.in);
		Random Random = new Random();
		
		System.out.println();
		System.out.print("Enter number of data points or 0 for default : ");
		int data = stdIn.nextInt();
		System.out.print("Enter avergae random error : ");
		double average = stdIn.nextDouble();
		System.out.print("Enter standard deviation of random error : ");
		double deviation = stdIn.nextDouble();
		System.out.println();
		
		int[] fert;
		double[] yield; //I can't be sure that Random.nextGaussian() is 'int' type.
		
		if(data==0)
		{
			fert = new int []{81,14,60,12,99,35,4,23,45,14};
			yield = new double []{131,71,112,53,115,92,71,65,104,25};
		}
		else
		{
			fert = new int [data];
			yield = new double [data];
			
			for(int i=0; i<fert.length; i++)
				fert[i]= Random.nextInt(100);
		
			for(int i=0; i<yield.length; i++)
				yield[i] = (50 + fert[i] + (Random.nextGaussian()*average)+ deviation);
				//yield = 50 + fertilizer + randomNumber(=(Random.nextGaussian()*average)+deviation)
		}
		
		//1 - the basic algorithm for linear regression
		int avgFert = 0;
		for(int i=0; i<fert.length; i++ )
			avgFert+=fert[i];
		avgFert/=fert.length;
		
		double avgYield = 0;
		for(int i=0; i<yield.length; i++ )
			avgYield+=yield[i];
		avgYield/=yield.length;
		
	//fertVariance & coVariance makes problem
		
		double fertVariance=0;
		//2
		for(int i=0; i<fert.length; i++)
			fertVariance+=Math.pow(fert[i]-avgFert,2);
		fertVariance/=fert.length;
		
		//System.out.println("fert_variance " + fertVariance);
		
		//3
		double coVariance=0;
		for(int i=0; i<fert.length; i++)
			coVariance+=(fert[i] - avgFert)*((double)yield[i] - avgYield);
		coVariance/=fert.length;
		//System.out.println("coVariance " + coVariance);
		
		
		//4
		double slope = coVariance/fertVariance;
		//double slope =0.8486061761764042895;
		//5
		double yieldAt0=avgYield - (slope*avgFert); //yield[0] - (slope * fert[0])
		//6
		double yieldAt100=yieldAt0 + (slope*100);
		//7
		
		double[] squareDiff = new double[fert.length];
		for(int i=0; i<fert.length; i++)
		{
			//yieldAt0 + fert[i]*slope = estimatedYield
			squareDiff[i]=((yieldAt0 + fert[i]*slope) - yield[i])*((yieldAt0 + fert[i]*slope)-yield[i]);
		}
		double residualError=0;
		for(int i=0; i<squareDiff.length; i++)
			residualError+=squareDiff[i];
		residualError=Math.sqrt(residualError/(squareDiff.length-2));
		
		System.out.println("Fert Yield");
		for(int i=0; i<10; i++)
			System.out.printf("%-4d %-5d\n", fert[i],(int)yield[i]);
		
		System.out.println("slope = "+ slope);
		System.out.println("yieldAt0 = "+ yieldAt0);
		System.out.println("yieldAtMax = "+ yieldAt100);
		System.out.println("residual error = "+ residualError);
	}
	
}
