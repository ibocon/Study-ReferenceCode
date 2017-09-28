import java.util.*;
import javax.swing.*;
import javax.swing.Timer;
import java.awt.*;
import java.awt.event.*;

public class MoleButton extends JButton {
	private boolean active;
	private boolean up;
	private Timer timer;
	private int delay;
	private int upTime;
	private static int nActiveMoles;
	private static int nMissed;
	private static int nWhacked;
	
	public MoleButton(){
		this.setIcon(new ImageIcon("mole-in-64.png"));
		addActionListener(new WhackListener());
		up=false;
		active=false;
	}
	
	public boolean isActive(){
		return active;
	}
	public boolean isUp(){
		return up;
	}
	public static int getNActiveMoles(){
		return nActiveMoles;
	}
	public static int getNMissed(){
		
		return nMissed;
	}
	public static int getNWhacked(){
	
		return nWhacked;
	}
	public static void clearCounters(){
		nActiveMoles=0;
		nMissed=0;
		nWhacked=0;
	}
	
	public void activate(int delay, int upTime){
		this.upTime=upTime;
		this.delay=delay;
		active=true;
		nActiveMoles++;
		timer=new Timer(this.delay,new DelayListener());
		timer.start();
	}
	public void hide(){
		setIcon(new ImageIcon("mole-in-64.png"));
		if(active){
			timer.stop();
			nActiveMoles--;
			active=false;
			up=false;
		}
		//It makes Every Error!!!
		
	}
	
	private class DelayListener implements ActionListener{
		public void actionPerformed(ActionEvent e){
			timer.stop();
			timer=new Timer(upTime, new TimeOutListener());
			up=true;
			setIcon(new ImageIcon("mole-out-64.png"));
			timer.start();
		}
	}
	private class WhackListener implements ActionListener{
		public void actionPerformed(ActionEvent e){
			if(up){
				nWhacked++;
				hide();
				//timer.stop();
			}
			
		}
	}
	private class TimeOutListener implements ActionListener{
		public void actionPerformed(ActionEvent e){
			nMissed++;
			hide();
		}
	}
	

}
