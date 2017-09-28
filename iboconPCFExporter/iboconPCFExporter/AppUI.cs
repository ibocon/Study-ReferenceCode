using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace iboconPCFExporter
{
    public partial class AppUI : System.Windows.Forms.Form
    {
        public ExternalCommandData Revit;
        public string Message;

        private PCFWriter Writer;
        private PCFData Paramters;
        private DataCtrl.DataCtrlInterface DataCtrl;

        public AppUI(ExternalCommandData revit, ref string message)
        {
            InitializeComponent();
            this.Revit = revit;
            this.Message = message;
            this.Paramters = new PCFData();
            this.Writer = new PCFWriter();

            //Excel 파일만 열 수 있도록 한정
            this.openExcelDialog.Filter = "Excel files (*.xlsx)|*.xlsx";

            if (!string.IsNullOrEmpty(Properties.Settings.Default.ExcelPath))
            {
                this.ExcelName.Text = Properties.Settings.Default.ExcelPath;
                this.DataCtrl = new DataCtrl.ExcelCtrl(Revit, Properties.Settings.Default.ExcelPath);
            }
            

        }

        private void ExportPCF_Click(object sender, EventArgs e)
        {
            if (DataCtrl != null)
            {
                //PCF파일을 만들기 전에, Excel 파일을 현재 Revit 상태로 업데이트 해준다.
                try
                {
                    DataCtrl.Export();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail: exporting current revit pcf status to excel file. \n\t" + Properties.Settings.Default.ExcelPath + "\n" + ex.Message);
                }
            }

            //TODO: (중) PCF 파일 이름을 원하는 형식대로 지정할 수 있도록 변경할 필요가 있다.
            //MyDocuments/iboconPCFExporter 라는 폴더에 오픈된 문서의 이름과 시간으로 파일이 저장된다.
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\iboconPCFExporter";
            string documentname = Revit.Application.ActiveUIDocument.Document.ProjectInformation.Name;
            string timestamp = DateTime.Now.ToString();
            timestamp = timestamp.Replace(" ", "_");
            timestamp = timestamp.Replace(":", "-");
            string filename = path + "\\" + documentname + "_" + timestamp + ".pcf";

            Result success = Result.Cancelled;

            success = this.Paramters.Create(Revit, ref Message, DataCtrl);
            if (success == Result.Succeeded)
            {
                success = this.Writer.WriteFile(filename, Paramters);

                if (success == Result.Succeeded)
                {
                    MessageBox.Show("Success: PCF data exported. \n" + filename);
                }
                else
                {
                    MessageBox.Show("Fail: PCF data export failed at writing File\n" + Message);
                }
            }
            else
            {
                MessageBox.Show("Fail: PCF data export failed at initializing Parameters.\n" + Message);
            }
        }

        //TODO: (중) 프로그램이 시작될 때, 프로젝트 내부에 PCF Paramter가 모두 존재하는지 체크하고 자동으로 추가하는게 좋은 UX!
        private void GenPCFParamBinding_Click(object sender, EventArgs e)
        {
            ParamBinding binding = new ParamBinding();
            binding.Generate(this.Revit, ref this.Message);
        }
        
        //(중) Excel File 선택만 아니라, Export PCF 버튼을 비활성화에서 활성화로 바꾸는 것이 좋은 UX!
        private void SelectExcelFile_Click(object sender, EventArgs e)
        {

            if (openExcelDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.ExcelName.Text = Properties.Settings.Default.ExcelPath = openExcelDialog.FileName;
                    this.DataCtrl = new DataCtrl.ExcelCtrl(this.Revit, openExcelDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail: to read Excel file. \n" + ex.Message);
                }
                
            }
            
        }
    }
}
