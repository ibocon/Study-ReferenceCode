//Windows
using System;
using System.Reflection;
using System.Windows.Media.Imaging;

//AutoDesk
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.ApplicationServices;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace iboconPCFExporter
{
    //Revit과 iboconPCFExporter를 연결하기 위한 클래스
    public class App : IExternalApplication
    {
        // Both OnStartup and OnShutdown must be implemented as public method
        public Result OnStartup(UIControlledApplication application)
        {
            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("ibocon's PCF Exporter");

            // Create a push button to trigger a command add it to the ribbon panel.
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;
            PushButtonData buttonData = new PushButtonData("Export", "Start PCF Exporting", thisAssemblyPath, "iboconPCFExporter.RunUserInterface");

            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;

            // Optionally, other properties may be assigned to the button
            // a) tool-tip
            pushButton.ToolTip = "This Add-in exports current project into Pipeline Component Format file.";

            // b) large bitmap
            Uri uriImage = new Uri(@"C:\Users\wlfka\Source\Repos\project-iboconPCFExporter\iboconPCFExporter\button.png");
            BitmapImage largeImage = new BitmapImage(uriImage);
            pushButton.LargeImage = largeImage;

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            // nothing to clean up in this simple case
            return Result.Succeeded;
        }
    }
    /// <remarks>
    /// 실행가능한 외부 명령어다.
    /// </remarks>
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class RunUserInterface : IExternalCommand
    {
        // The main Execute method (inherited from IExternalCommand) must be public
        public Autodesk.Revit.UI.Result Execute(ExternalCommandData revit, ref string message, ElementSet elements)
        {
            try
            {
                AppUI userinterface = new AppUI(revit, ref message);
                userinterface.ShowDialog();
                userinterface.Close();

                return Result.Succeeded;
            }

            catch (Autodesk.Revit.Exceptions.OperationCanceledException) { return Result.Cancelled; }

            catch (Exception exception)
            {
                message = exception.Message;
                return Result.Failed;
            }
        }
    }
}
