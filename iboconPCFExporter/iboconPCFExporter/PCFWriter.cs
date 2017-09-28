using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iboconPCFExporter
{
    interface Writable
    {
        void Write(StringBuilder writer);
    }
    //Paramters를 받아 PCF를 작성하여 저장하는 클래스
    public class PCFWriter
    {
        readonly static public string TAB = "     ";
        private StringBuilder Writer;
        readonly static public double UnitAngleToDeg = (180.0 / Math.PI) * 100;
        //Inch로도 변환하고 싶다면, 이 값을 변경하면 된다.
        readonly static public double UnitFootToStd = 304.8;

        public PCFWriter()
        {
            this.Writer = new StringBuilder();
        }

        public static string FormatDecimal(double value)
        {
            return string.Format("{0:0.0000}", value);
        }

        public Result WriteFile(string filename, PCFData param)
        {
            try
            {
                param.BasicHeader.Write(this.Writer);
                param.PipelineHeader.Write(this.Writer);

                foreach(Writable cp in param.Components)
                {
                    cp.Write(this.Writer);
                }

                this.Writer.Append("MATERIALS").AppendLine();
                foreach(MaterialData md in param.Materials.Values)
                {
                    md.Write(this.Writer);
                }

                System.IO.File.WriteAllBytes(filename, new byte[0]);
                using (StreamWriter w = File.AppendText(filename))
                {
                    w.Write(this.Writer);
                    w.Close();
                }

                return Result.Succeeded;
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }

            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                return Result.Failed;
            }
        }
    }
}
