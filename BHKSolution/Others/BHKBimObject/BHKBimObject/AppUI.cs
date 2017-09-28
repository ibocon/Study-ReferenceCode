using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace BHKBimObject
{
    public partial class AppUI : Form
    {
        private App App;
        private string filepath;

        public AppUI(App application)
        {
            InitializeComponent();
            this.WebBrowser.ScriptErrorsSuppressed = true;
            this.WebBrowser.Navigate(@"https://knowledge.autodesk.com/support/autocad/downloads/caas/downloads/content/autocad-sample-files.html");
            this.App = application;

            filepath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\models";
            if (!Directory.Exists(filepath))
            {
                Directory.CreateDirectory(filepath);
            }

        }
        
        
        private void WebBrowser_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            //URL을 검사해서, 다운로드를 위한 URL인지 확인하는 작업
            if (e.Url.Segments[e.Url.Segments.Length - 1].EndsWith(".dwg"))
            {
                e.Cancel = true;

                string filename = e.Url.Segments[e.Url.Segments.Length - 1];
                string file = filepath + "\\" + filename;
                WebClient client = new WebClient();

                DocumentCollection acDocMgr = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager;

                //var SameDocument = from Document document in acDocMgr where filename.EndsWith(document.Name) select document;
                bool isOpened = false;
                foreach (Document doc in acDocMgr)
                {
                    if (file.Equals(doc.Name))
                    {
                        isOpened = true;
                        break;
                    }
                }

                if (isOpened == false)
                {
                    if (File.Exists(file))
                    {
                        DialogResult dialogResult = MessageBox.Show("File " + filename + " already exist. Do you want to override it?", "Download", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                        {
                            client.DownloadFile(e.Url, file);
                        }
                    }
                    else
                    {
                        client.DownloadFile(e.Url, file);
                    }

                    acDocMgr.Open(file, false);
                }
                else
                {
                    MessageBox.Show("File " + filename + " already opened. Can not download file!");
                }
            }
        }

        private void WebBrowser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            WebBrowser.Navigate(WebBrowser.StatusText);
        }
    }
}
