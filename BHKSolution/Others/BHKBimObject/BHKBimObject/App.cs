using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;
using Autodesk.Windows;
using System.IO;

namespace BHKBimObject
{
    public class App
    {
        private AppUI userinterface;

        [CommandMethod("BHKBimObject")]
        public void BHKBimObject()
        {
            this.userinterface = new AppUI(this);

            RibbonControl ribbon = ComponentManager.Ribbon;
            if(ribbon != null)
            {
                //초기화할 때, 호출하지 않으면 비정상적인 접근이라는 예외가 호출된다.
                //RibbonTab addin = ribbon.Tabs.First(tab => tab.Name.Equals("Add-ins"));

                RibbonTab addin = ribbon.FindTab("TEST");
                if (addin != null)
                {
                    ribbon.Tabs.Remove(addin);
                }
                addin = new RibbonTab();
                addin.Title = "TEST";
                addin.Id = "test";
                //Add the Tab
                ribbon.Tabs.Add(addin);

                RibbonPanel panel = new RibbonPanel();

                RibbonPanelSource rps = new RibbonPanelSource();
                rps.Title = "BHK's Bim Object";
                panel.Source = rps;

                RibbonButton rb = new RibbonButton();
                rb.Name = "Open Browser";
                rb.Text = "Open Browser";
                rb.ShowText = true;
                
                rb.CommandHandler = new MyRibbonButtonCommandHandler();
                rb.CommandParameter = this.userinterface;
                
                //Add the Button to the Tab
                rps.Items.Add(rb);

                addin.Panels.Add(panel);
            }
            
        }

        public class MyRibbonButtonCommandHandler : System.Windows.Input.ICommand
        {
            public bool CanExecute(object parameter)
            {
                return true; //return true means the button always enabled
            }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter)
            {
                RibbonCommandItem rci = parameter as RibbonCommandItem;
                (rci.CommandParameter as AppUI).ShowDialog();
            }
        }
    }
}
