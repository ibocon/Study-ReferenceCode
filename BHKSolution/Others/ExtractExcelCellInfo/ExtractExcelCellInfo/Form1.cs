using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace ExtractExcelCellInfo
{
    public partial class Form1 : Form
    {
        Excel.Application xlApp = null;
        Excel.Workbook xlWorkBook = null;
        Excel.Worksheet xlWorkSheet = null;
        Excel.Window xlWindow = null;
        Excel.Range myRange = null;

        delegate void SetTextCallback(TextBox box, string text);
        delegate void GetCellInfoCallback(Excel.Range cell);
        public Form1()
        {
            InitializeComponent();
            try
            {
                xlApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
                xlApp.Visible = true;
            }
            catch
            {
                xlApp = new Excel.Application();
                xlApp.Visible = true;
                xlApp.Workbooks.Add();
            }

            xlWorkBook = xlApp.ActiveWorkbook;
            xlWorkSheet = xlApp.ActiveSheet;

            xlWorkSheet.SelectionChange += WorkSheet_SelectionChange;
        }

        private void WorkSheet_SelectionChange(Excel.Range Target)
        {
            //myRange = Target.Copy();

            foreach (Excel.Range cell in Target.Cells)
            {
                Console.WriteLine("Cell " + cell.Address);
                string fontname = ((Excel.Range)cell).Font.Name.ToString();
                Console.WriteLine("\t Font Name = " + fontname);
                Excel.XlHAlign horizontalAlignment = (Excel.XlHAlign)cell.HorizontalAlignment;
                Console.WriteLine("\t Horizontal Alignment = " + horizontalAlignment);
                Excel.XlVAlign verticalalignment = (Excel.XlVAlign)cell.VerticalAlignment;
                Console.WriteLine("\t Vertical Alignment = " + verticalalignment);


                Console.WriteLine("\t Borders Line Style >");

                Console.WriteLine("\t\t Left = "+(Excel.XlLineStyle)cell.Borders[Excel.XlBordersIndex.xlEdgeLeft].LineStyle);
                Console.WriteLine("\t\t Right = " + (Excel.XlLineStyle)cell.Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle);
                Console.WriteLine("\t\t Top = " + (Excel.XlLineStyle)cell.Borders[Excel.XlBordersIndex.xlEdgeTop].LineStyle);
                Console.WriteLine("\t\t Bottom = " + (Excel.XlLineStyle)cell.Borders[Excel.XlBordersIndex.xlEdgeBottom].LineStyle);

                //xlRgbColor에 기록된 컬러만 이용해야 한다.
                Excel.XlRgbColor cellColor = (Excel.XlRgbColor)cell.Interior.Color;
                Console.WriteLine("\t Cell Color = " + cellColor);

                Excel.XlRgbColor fontColor = (Excel.XlRgbColor)cell.Font.Color;
                Console.WriteLine("\t Font Color = " + fontColor);

                bool merged = (bool)cell.MergeCells;
                Console.WriteLine("\tMerged?\t"+merged);
                if (merged)
                {
                    Excel.Range mergeArea = (Excel.Range)cell.MergeArea;
                    Console.WriteLine("\tMerged Area = "+mergeArea.Address);
                }

                Excel.XlOrientation orientation = (Excel.XlOrientation)cell.Orientation;
                Console.WriteLine("\tOrientation(Text Angle) = "+ orientation);
            }

            SetText(this.textBox1, Target.Address);
        }

        /// <summary>
        /// Change targeted text box's text message
        /// </summary>
        /// <param name="box"> target box to change </param>
        /// <param name="text"> text to place in text box </param>
        private void SetText(TextBox box, string text)
        {
            if (box.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { box, text });
            }
            else
            {
                box.Text = text;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            releaseObject(myRange);
            releaseObject(xlWindow);
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                if(obj != null)
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            try
            {
                xlApp = System.Runtime.InteropServices.Marshal.GetActiveObject("Excel.Application") as Excel.Application;
                //xlApp.Visible = true;
            }
            catch
            {
                xlApp = new Excel.Application();
                //xlApp.Visible = true;
                xlApp.Workbooks.Add();
            }

            xlWorkBook = xlApp.ActiveWorkbook;
            xlWorkSheet = xlApp.ActiveSheet;
            xlWorkSheet.SelectionChange += WorkSheet_SelectionChange;
        }

        private void Form1_Deactivate(object sender, EventArgs e)
        {
            xlWorkSheet.SelectionChange -= WorkSheet_SelectionChange;
            releaseObject(myRange);
            releaseObject(xlWindow);
            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
    }
}
