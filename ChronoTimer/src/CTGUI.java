import java.awt.BorderLayout;
import java.awt.Color;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.ItemEvent;
import java.awt.event.ItemListener;
import java.util.Iterator;
import java.util.List;
import java.awt.ItemSelectable;

import javax.swing.JButton;
import javax.swing.JCheckBox;
import javax.swing.JComboBox;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JScrollPane;
import javax.swing.JTextArea;
import javax.swing.ScrollPaneConstants;

import team.ChronoTimer;
import team.EventType;
import team.PlaceHolderRecord;
import team.RacerRecord;
import team.Record;
import team.Run;
import team.SensorType;
import team.TChronoTimer;
import team.TRun;

public class CTGUI {
	
	protected ChronoTimer timer;
	protected String inputCmd = "";
	private JTextArea screen;
	private String[] functions;
	private int selectedCmd=0;
	private JButton[] toggleChannel;
	JComboBox[] inputCB;
	
	public CTGUI(){
		//create the window
		timer = new TChronoTimer();
		JFrame window = new JFrame("Chrono Timer 1009 (by Hello, cs361!) ");
		window.setLayout(new GridLayout(2, 3));
		//create function buttons
		JPanel functionPanel = new JPanel(new GridLayout(2,1));
			JButton powerButton = new JButton("POWER");
			powerButton.setBackground(Color.RED);
			powerButton.setOpaque(true);
			JPanel buttonPanel = new JPanel(new GridLayout(3,1));
				JButton functionButton = new JButton("FUNCTION");
				JPanel arrowButtonPanel = new JPanel(new GridLayout(1,4));
					JButton leftButton = new JButton("left < (Backspace)");
					JButton rightButton = new JButton("right >");
					JButton downButton = new JButton("down V");
					JButton upButton = new JButton("up ^");
				JButton swapButton = new JButton("SWAP");
		functionPanel.add(powerButton);
		functionPanel.add(buttonPanel);
			buttonPanel.add(functionButton);
			buttonPanel.add(arrowButtonPanel);
				arrowButtonPanel.add(leftButton);
				arrowButtonPanel.add(rightButton);
				arrowButtonPanel.add(downButton);
				arrowButtonPanel.add(upButton);
			buttonPanel.add(swapButton);

		powerButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				System.out.println("Toggle Power!");
				if(timer.isOn()){
					timer.powerOff();
					powerButton.setBackground(Color.RED);
					for(int i=1;i<=timer.MAXIMUM_CHANNELS;i++ ){
						toggleChannel[i-1].setBackground(Color.BLACK);
					}
				}else{
					timer.powerOn();
					powerButton.setBackground(Color.GREEN);
					
					for(int i=1;i<=timer.MAXIMUM_CHANNELS;i++ ){
						if(timer.getChannel(i).isEnabled()){
							toggleChannel[i-1].setBackground(Color.GREEN);
						}else{
							toggleChannel[i-1].setBackground(Color.RED);
						}
						
					}
				}
			}
		});
		
		functions = new String[]{
				"time <HH*MM*SS>- Sets the current time.\n",
				"reset - Resets the chrono timer back to initial state.\n",
				"num <number> - Sets <number> as the next competetor to start.\n",
				"clear <number> - Clear <number> as the next competetor.\n",
				"dnf - The next competetor to finish will not finish.\n",
				"event <type> - Sets the current run with the give event type.\n\t(IND:1, PARIND:2, GRP:3, PARGRP:4)\n",
				"newrun - Creates a new run.(Must end a run first)\n",
				"endrun - Done with the current run.\n",
				"print <run> - Prints the given run.\n",
				"export <run> - Exports the given run.\n"};
		functionButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				if(inputCmd.contains("#")){
					processCmd();
				}else{
					screen.setText("");
					for(int i=0; i<10;i++){
						if(i==selectedCmd){screen.append(">>");}
						screen.append(functions[i]);
					}
					screen.append("INPUT : "+inputCmd);
				}
			}
		});
		leftButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				if(!inputCmd.isEmpty()){
					if(inputCmd.contains("#")){inputCmd=inputCmd.replace("#", "");}
					if(!inputCmd.isEmpty()){inputCmd=inputCmd.substring(0, inputCmd.length()-1);}
					functionButton.getActionListeners()[0].actionPerformed(new ActionEvent(event, selectedCmd, inputCmd));
				}	
			}
		});
		rightButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				System.out.println("Deprecated!");
			}
		});
		downButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				if(selectedCmd<9) selectedCmd+=1;
				functionButton.getActionListeners()[0].actionPerformed(new ActionEvent(event, selectedCmd, inputCmd));
			}
		});
		upButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				if(selectedCmd>0)selectedCmd-=1;
				functionButton.getActionListeners()[0].actionPerformed(new ActionEvent(event, selectedCmd, inputCmd));
			}
		});
		swapButton.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				timer.swap();
			}
		});
			
		//create signal buttons
		JPanel signalPanel = new JPanel(new GridLayout(2,1));
			JPanel startPanel = new JPanel(new GridLayout(3,5));
			JPanel finishPanel = new JPanel(new GridLayout(3,5));
			
				JLabel empty=new JLabel("     ");
				JLabel empty2=new JLabel("     ");
				// creating signal's Label
				JLabel[] signalLabel = new JLabel[8];
				for(int i=0; i<8; i++){
					signalLabel[i] = new JLabel(Integer.toString(i+1));
				}
				
				JLabel startLabel=new JLabel("Start");
				JLabel finishLabel=new JLabel("Finish");
				JButton[] signalButton = new TriggerButton[8];
				for(int i=0;i<8;i++){
					signalButton[i] = new TriggerButton(i+1);
				}
				
				JLabel EDLabel = new JLabel("Enable/Disable");
				JLabel EDLabel2 = new JLabel("Enable/Disable");
				toggleChannel = new ChannelButton[timer.MAXIMUM_CHANNELS];
				for(int i=0;i<timer.MAXIMUM_CHANNELS;i++){
					toggleChannel[i] = new ChannelButton(i+1);
					toggleChannel[i].setBackground(Color.BLACK);
					toggleChannel[i].setOpaque(true);
				}
				//add startPanel components to startPanel
					startPanel.add(empty);
					for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
						startPanel.add(signalLabel[i*2]);
					}
					startPanel.add(startLabel);
					for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
						startPanel.add(signalButton[i*2]);
					}
					startPanel.add(EDLabel);
					for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
						startPanel.add(toggleChannel[i*2]);
					}
				//add start panel to signal panel
					signalPanel.add(startPanel);
					
				//add components to finishPanel
					finishPanel.add(empty2);
					for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
						finishPanel.add(signalLabel[i*2+1]);
					}
					finishPanel.add(finishLabel);
					for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
						finishPanel.add(signalButton[i*2+1]);
					}
					finishPanel.add(EDLabel2);
					for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
						finishPanel.add(toggleChannel[i*2+1]);
					}
				//add finish panel to signal Panel
					signalPanel.add(finishPanel);
				
		//create printer
		JPanel printerPanel = new JPanel(new GridLayout(2,1));

			JButton printButton = new JButton("Printer Button");
			JTextArea printer = new JTextArea(80,100);
			printer.setEditable(false);
			
			//create scrollbar for printer text area
			JScrollPane pScroll = new JScrollPane(printer, ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS, ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);
			
		//add components to printer panel
			printerPanel.add(printButton);
			printerPanel.add(pScroll);

			printButton.addActionListener(new ActionListener(){
				@Override
				public void actionPerformed(ActionEvent event){
					// print <run> - Prints the given run.
					printer.setText("");
					List<Run> runs = timer.getRuns();
					Iterator<Run> it = runs.iterator();
					while(it.hasNext()){
						Run run = it.next();
						printer.append("Run " + run.getID()+"\n");
						printer.append("===== ID : START - FINISH =====\n");
						for(Record r : run.getRecords()) {
							if(r instanceof RacerRecord) {
								RacerRecord rec = (RacerRecord) r;
								printer.append("Racer " + rec.getRacer().getID() + ": ");
							} else if(r instanceof PlaceHolderRecord) {
								PlaceHolderRecord rec = (PlaceHolderRecord) r;
								printer.append("PlaceHolder " + rec.getPlaceHolder() + ": ");
							}
							if(r.didNotFinish()) {
								printer.append(timer.getTimeManager().formatTime(r.getStartTime()) + " - DNF\n");
							} else {
								printer.append(timer.getTimeManager().formatTime(r.getStartTime()) + " - " + timer.getTimeManager().formatTime(r.getFinishTime())+"\n");
							}
						}
					}
				}
			});
			
		//create numpad input
		JPanel numpadPanel = new JPanel(new GridLayout(4,3));
		JButton[] numpadButton = new NumberPadButton[10];
			for(int i=0;i<10;i++){
				numpadButton[i] = new NumberPadButton(Integer.toString(i));
			}
			JButton numpadButtonStar = new NumberPadButton("*");
			JButton numpadButtonHash = new NumberPadButton("#");
		
		//add components to numpadPanel
			for(int i=1; i<10;i++){
				numpadPanel.add(numpadButton[i]);
			}
			numpadPanel.add(numpadButtonStar);
			numpadPanel.add(numpadButton[0]);
			numpadPanel.add(numpadButtonHash);
			
		//create backview's ports
		JPanel backViewPanel = new JPanel(new GridLayout(4,1));
		JLabel backViewLabel = new JLabel("Sensor Connetcion");
		JPanel inputGrid = new JPanel(new GridLayout(4,4));
		JLabel[] inputLabel = new JLabel[8];
		for(int i=0; i<8; i++){
			inputLabel[i] = new JLabel(Integer.toString(i+1));
		}
		inputCB = new BackViewBox[8];
		for(int i=0;i<timer.MAXIMUM_CHANNELS; i++){
			inputCB[i] = new BackViewBox(i+1);
		}
			
			//Add components to inputGrid panel
		for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
			inputGrid.add(inputLabel[i*2]);
		}
		for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
			inputGrid.add(inputCB[i*2]);
		}
		for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
			inputGrid.add(inputLabel[i*2+1]);
		}
		for(int i=0;i<(timer.MAXIMUM_CHANNELS/2);i++){
			inputGrid.add(inputCB[i*2+1]);
		}
			
			//add inputGrid to backViewPanel
			backViewPanel.add(backViewLabel);
			backViewPanel.add(inputGrid);
			
		JLabel usbLabel = new JLabel("USB Port");
		JCheckBox usbPort = new JCheckBox();
		
		backViewPanel.add(usbLabel);
		backViewPanel.add(usbPort);
		
		usbPort.addActionListener(new ActionListener(){
			@Override
			public void actionPerformed(ActionEvent event){
				//What should do with usbPort?
			}
		});
		
		//create screen to show result
		JPanel screenPanel = new JPanel(new GridLayout(2,1));
		JLabel screenLabel = new JLabel("Queue/Running/Final Time");
			screen = new JTextArea(300,300);
			screen.setEditable(false);
		//add components to screenPanel
			
		//add scrollbar to screen
			JScrollPane screenScroll = new JScrollPane(screen, ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS, ScrollPaneConstants.HORIZONTAL_SCROLLBAR_NEVER);
			screenPanel.add(screenLabel);
			screenPanel.add(screenScroll);
			
		//add to window and display
			window.setSize(1500, 1000);
			window.add(functionPanel);
			window.add(signalPanel);
			window.add(printerPanel);
			window.add(numpadPanel);
			window.add(backViewPanel);
			window.add(screenPanel);
			window.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
			window.setVisible(true);
	}
	
	protected class BackViewBox extends JComboBox{
		private static final long serialVersionUID = -1061884623532972956L;
		private final String[] sensorTypes = {"NONE","EYE","GATE","PAD"};
		private final int id;
		
		BackViewBox(int id){
			this.id = id;
			for(int i=0; i<sensorTypes.length; i++)
				this.addItem(sensorTypes[i]);
			ItemListener itemListener = new ItemListener(){
				public void itemStateChanged(ItemEvent itemEvent){
		          int state = itemEvent.getStateChange();
		          String selectedSensor = "NONE";
		          if(state == ItemEvent.SELECTED){
		        	  selectedSensor = (String)itemEvent.getItem();
		        	  SensorType s= SensorType.NONE;
		        	  //change sensor type
		        	  if(!timer.isOn()){
		        		  System.out.println("Timer's Power is OFF!"); 
		        		  inputCB[id-1].setSelectedIndex(0);
		        	  }
		        	  else if(selectedSensor=="EYE") s=SensorType.EYE;
		        	  else if(selectedSensor=="GATE") s=SensorType.GATE;
		        	  else if(selectedSensor=="PAD") s=SensorType.PAD;
		        	  timer.connect(s, id);
		        	  //change color
		        	  if(!timer.isOn()){toggleChannel[id-1].setBackground(Color.BLACK);}
		        	  else if(timer.getChannel(id).isEnabled()){toggleChannel[id-1].setBackground(Color.GREEN);}
		        	  else{toggleChannel[id-1].setBackground(Color.RED);}
		        	  System.out.println(timer.getChannel(id).getSensorType() + " is connected with " + id +" Channel.");
		          }
				}
			};
			this.addItemListener(itemListener);	
		}
	}
	protected class ChannelButton extends JButton {
		private static final long serialVersionUID = 2412443805772599043L;
		private final int id;
		ChannelButton(int id) {
			this.id = id;
			this.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					if(!timer.isOn()){
						toggleChannel[id-1].setBackground(Color.BLACK);
						System.out.println("Channel " + id+" does not changed. STAT: "+ timer.getChannel(id).isEnabled());
					}else if(timer.getChannel(id).isEnabled()){
						timer.getChannel(id).disable();
						toggleChannel[id-1].setBackground(Color.RED);
						System.out.println("Channel " + id+" is disabled.");
					}else{
						timer.getChannel(id).enable();
						toggleChannel[id-1].setBackground(Color.GREEN);
						System.out.println("Channel " + id+" is enabled.");
					}
				}
			});
		}
		
	}
	protected class NumberPadButton extends JButton {
		private static final long serialVersionUID = 2412443805772599043L;
		private final String id;
		NumberPadButton(String id) {
			this.id = id;
			this.setText(id);
			this.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					inputCmd+=id;
					screen.append(id);
				}
			});
		}
		
	}
	protected class TriggerButton extends JButton {
		
		private static final long serialVersionUID = 2412443805772599043L;
		private final int id;
		
		TriggerButton(int id) {
			super(Integer.toString(id));
			this.id = id;
			this.addActionListener(new ActionListener() {
				@Override
				public void actionPerformed(ActionEvent e) {
					System.out.println("Triggering chan " + id);
					if(!timer.trigger(id))
						System.out.println("Failed to trigger channel " + id);
					else
						System.out.println("Successfully triggered channel " + id);
				}
			});
		}
	}
	public void processCmd(){
		try{
			inputCmd=inputCmd.substring(1);
			System.out.println(inputCmd);
			int number;
			switch(selectedCmd){
				case 0:
					// time - Sets the current time.
					String[] t=inputCmd.split("\\*");
					screen.setText("\nExecuting : TIME <"+t[0]+"*"+t[1]+"*"+t[2]+"> \n");
					timer.getTimeManager().setTime(t[0]+":"+t[1]+":"+t[2]);
					screen.append("Set time to " + t[0]+":"+t[1]+":"+t[2] + "\n");
					break;
				case 1:
					// reset - Resets the chrono timer back to initial state.
					screen.setText("\nExecuting : RESET \n");
					timer.reset();
					screen.append("Reset done\n");
					break;
				case 2:
					// num <number> - Sets <number> as the next competetor to start.
					number = Integer.parseInt(inputCmd);
					screen.setText("\nExecuting : NUMBER "+number+" \n");
					if(timer.getLatestRun().addRacer(number) != null)
						screen.append("Successfully added racer " + number+"\n");
					else
						screen.append("[!!]Failed to add racer " + number+"\n");
					break;
				case 3:
					// clear <number> - Clear <number> as the next competetor.
					number = Integer.parseInt(inputCmd);
					screen.setText("\nExecuting : CLEAR "+number+" \n");
					if(timer.getLatestRun().removeRacer(number))
						screen.append("Successfully removed racer " + number+"\n");
					else
						screen.append("Failed to remove racer " + number+"\n");
					break;
				case 4:
					// dnf - The next competetor to finish will not finish.
					screen.setText("\nExecuting : DNF \n");
					timer.doNotFinish();
					screen.append("DNF done\n");
					break;
				case 5:
					// event <type> - Sets the current run with the give event type.
					// (EYE:1, GATE:2,PAD:3,NONE:4)
					number = Integer.parseInt(inputCmd);
					screen.setText("\nExecuting : EVENT "+number+" - ( ");
					for(int i=0; i<EventType.values().length;i++){
						screen.append((i+1)+":"+EventType.values()[i]+" ");
					}
					screen.append(")\n");
					if(number<1 || number>EventType.values().length){System.out.println("[!!]Invalid event number!");break;}
					EventType event = EventType.values()[number-1];
					if(timer.setEvent(event))
						screen.append("Successfully set event to " + event + "\n");
					else
						screen.append("Failed to set event to " + event + "\n");
					break;
				case 6:
					// newrun - Creates a new run.(Must end a run first)
					screen.setText("\nExecuting : NEWRUN \n");
					timer.newRun();
					screen.append("add new run done\n");
					break;
				case 7:
					// endrun - Done with the current run.
					screen.setText("\nExecuting : ENDRUN \n");
					timer.endRun();
					screen.append("end run done\n");
					break;
				case 8:
					// print <run> - Prints the given run.
					int rid = Integer.parseInt(inputCmd)-1;
					screen.setText("\nExecuting : PRINT "+rid+" \n");
					if(rid < 0 || rid >= timer.getRuns().size()) {
						screen.append("[!!]No run found.\n");
						break;
					}
					Run run = timer.getRuns().get(rid);
					if(run == null) {
						screen.append("No run found with id of " + rid+"\n");
						break;
					}
					screen.append("Run " + run.getID()+"\n");
					screen.append("===== ID : START - FINISH =====\n");
					for(Record r : run.getRecords()) {
						if(r instanceof RacerRecord) {
							RacerRecord rec = (RacerRecord) r;
							screen.append("Racer " + rec.getRacer().getID() + ": ");
						} else if(r instanceof PlaceHolderRecord) {
							PlaceHolderRecord rec = (PlaceHolderRecord) r;
							screen.append("PlaceHolder " + rec.getPlaceHolder() + ": ");
						}
						if(r.didNotFinish()) {
							screen.append(timer.getTimeManager().formatTime(r.getStartTime()) + " - DNF\n");
						} else {
							screen.append(timer.getTimeManager().formatTime(r.getStartTime()) + " - " + timer.getTimeManager().formatTime(r.getFinishTime())+"\n");
						}
					}
					break;
				case 9:
					// export <run> - Exports the given run.
					number = Integer.parseInt(inputCmd);
					screen.setText("\nExecuting : EXPORT "+number+" \n");
					if(number < 1 || number > timer.getRuns().size()) {
						screen.append("No run found with that id.\n");
						break;
					}
					timer.getExporter().export((TRun) timer.getRuns().get(number-1));
					screen.append("export run done\n");
					break;
				default:
					screen.append("\n[!!]Invalid command number!");
					break;
			}
			inputCmd="";
		}catch(Exception e){
			//!!
		}
		
	}
	public static void main(String[] args){
		CTGUI gui = new CTGUI();
	}
}
