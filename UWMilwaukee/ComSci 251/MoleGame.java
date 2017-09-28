import java.util.*;
import javax.swing.*;
import java.awt.*;
import javax.swing.Timer;
import java.awt.event.*;
import javax.swing.border.*;

public class MoleGame extends JFrame {
	private final int boardSize=16;
	private ArrayList<MoleButton> moles;
	private Timer boardCheckTimer;
	private JLabel numWhacked;
	private JLabel numMissed;
	private JTextField activeMolesField;
	
	private JTextField delayText;
	private JTextField upTimeText;
	private Timer limited;
	
	public MoleGame(){
		setTitle("Whack-A-Mole");
		setSize(480,320);
		setLayout(new BorderLayout());
		createContents();
		setVisible(true);
	}
	
	private void createContents(){
		JPanel MPanel = new JPanel(new GridLayout(4,4));
		add(MPanel,BorderLayout.CENTER);
		//MPanel.setBorder(new EmptyBorder(10,10,10,10));
		//setting moles Button
		moles=new ArrayList<MoleButton>();
		for(int n=0; n<boardSize; n++){
			moles.add(new MoleButton());
			MPanel.add(moles.get(n));
		}
	
		JPanel GameParameters = new JPanel(new GridLayout(0,1));
		GameParameters.setBorder(new EmptyBorder(10,10,10,10));
		add(GameParameters,BorderLayout.EAST);
		//setting GameParameters
		
		GameParameters.add(new JLabel("Game Parameters",SwingConstants.CENTER));
		
		JPanel AM=new JPanel(new FlowLayout());
		AM.add(new JLabel("Active Moles "));
		AM.add(activeMolesField=new JTextField("1",5));
		GameParameters.add(AM);
		GameParameters.add(new JLabel("Game Statistics",SwingConstants.CENTER));
		numWhacked=new JLabel("Moles Whacked:            "+"0");
		GameParameters.add(numWhacked);
		numMissed=new JLabel("Moles Missed:                "+"0");
		GameParameters.add(numMissed);
		
		JPanel setDelay=new JPanel(new FlowLayout());
		
		setDelay.add(new JLabel("Set Delay :"));
		setDelay.add(delayText=new JTextField("1000",5));
		GameParameters.add(setDelay);
		
		JPanel setUpTime=new JPanel(new FlowLayout());
		
		setUpTime.add(new JLabel("Set upTime : "));
		setUpTime.add(upTimeText=new JTextField("2000",5));
		GameParameters.add(setUpTime);
		
		JButton start=new JButton("Start Game");
		start.addActionListener(new StartButtonListener());
		GameParameters.add(start);
		JButton stop=new JButton("Stop Game");
		stop.addActionListener(new StopButtonListener());
		GameParameters.add(stop);
		GameParameters.add(new JLabel("Time Limit = 30 seconds"));
	}
	
	private int getActiveMoleCount(){
		return MoleButton.getNActiveMoles();
	}
	
	private void activateNewMole(){
		for(int i=0; i<(Integer.parseInt(activeMolesField.getText())-getActiveMoleCount());i++){
			moles.get((int)(Math.random()*boardSize)).activate(Integer.parseInt(delayText.getText()),Integer.parseInt(upTimeText.getText()));
		}
	}
	
	private class BoardCheckListener implements ActionListener{	
		public void actionPerformed(ActionEvent e){
			activateNewMole();
		}
	}
	
	private class StartButtonListener implements ActionListener{
		public void actionPerformed(ActionEvent e){
			MoleButton.clearCounters();
			numWhacked.setText("Moles Whacked:            "+MoleButton.getNWhacked());
			numMissed.setText("Moles Missed:                "+MoleButton.getNMissed());
			boardCheckTimer=new Timer(20,new BoardCheckListener());
			boardCheckTimer.setRepeats(true);
			boardCheckTimer.start();
			activateNewMole();
			limited = new Timer(30000, new StopButtonListener());
			limited.start();
		}
	}
	
	private class StopButtonListener implements ActionListener{
		public void actionPerformed(ActionEvent e){
			if (boardCheckTimer == null) return;
			boardCheckTimer.stop();
			limited.stop();
			boardCheckTimer = null;
			limited=null;
			for(int n=0; n<boardSize;n++){
				//JOptionPane.showMessageDialog(null,"hide!");
				if(moles.get(n).isActive())
					//JOptionPane.showMessageDialog(null, n+"is hided!");
					moles.get(n).hide();
			}
			numWhacked.setText("Moles Whacked:            "+MoleButton.getNWhacked());
			numMissed.setText("Moles Missed:                "+MoleButton.getNMissed());
			
		}
	}
	
	public static void main(String[] args){
		new MoleGame();
	}
	
}
