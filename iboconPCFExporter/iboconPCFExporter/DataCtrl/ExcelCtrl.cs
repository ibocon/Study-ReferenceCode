using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using Autodesk.Revit.DB;
using System.Configuration;

namespace iboconPCFExporter.DataCtrl
{
    //(필수) Excel과 관련된 작업을 위한 클래스
    public class ExcelCtrl : DataCtrlInterface
    {
        private string ExcelPath;
        ExternalCommandData Revit;
        private DataSet DataSet;
        //관리해야 할 테이블이 많아지면, Dictionary를 활용한다. 테이블 이름은 BiDic으로 enum 처리한다.
        //public IDictionary<string, DataTable> DataTables;
        public DataTable DT_Components;
        public DataTable DT_Materials;

        public ExcelCtrl(ExternalCommandData revit, string path)
        {
            ExcelPath = path;
            Revit = revit;

            DataSet = this.GetDataSets(this.ExcelPath);
            this.CheckData();
            this.UpdateRevitbyExcel();

        }

        private bool CheckData()
        {
            string message = "";

            if (DT_Components == null)
            {
                throw new Exception("Fail: 'Components' Sheet is missing in selected Excel file!");
            }
            else if (DT_Materials == null)
            {
                throw new Exception("Fail: 'Materials' Sheet is missing in selected Excel file!");
            }

            bool comp = this.CheckCompData(DT_Components, ref message);
            bool mat = this.CheckMatData(DT_Materials, ref message);

            if (comp && mat)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Warning: Importing Excel: Excel does not have mandatory column. \n" + message);
                return false;
            }
        }

        private void UpdateExcelbyRevit()
        {
            FilteredElementCollector collector = PCFData.CollectPipeElements(this.Revit.Application.ActiveUIDocument.Document);
            foreach (DataRow data in DT_Components.Rows)
            {
                Autodesk.Revit.DB.Element element = null;
                IEnumerable<Autodesk.Revit.DB.Element> search = from Autodesk.Revit.DB.Element e in collector where e.get_Parameter(BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM).AsValueString().Equals(data.Field<string>("FAMILYTYPE")) select e;

                if (search.Any())
                {
                    element = search.First();
                }

                if (element != null)
                {
                    string familyType = element.get_Parameter(BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM).AsValueString();
                    IEnumerable<RevitParam.ParameterDefinition> FamilySharedParam = from RevitParam.ParameterDefinition pd in RevitParam.ParamterList.Values where pd.Scope == RevitParam.Scope.Share select pd;
                    foreach (RevitParam.ParameterDefinition rvtParam in FamilySharedParam)
                    {
                        string rvtParamName = rvtParam.Name.Replace(RevitParam.Prefix, "");
                        if (element.get_Parameter(rvtParam.Id) != null)
                        {
                            switch (rvtParam.Type)
                            {
                                case (ParameterType.Text):
                                    data[rvtParamName] = element.get_Parameter(rvtParam.Id).AsString();
                                    break;
                                case (ParameterType.Integer):
                                    data[rvtParamName] = element.get_Parameter(rvtParam.Id).AsInteger();
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private bool UpdateRevitbyExcel()
        {
            bool rt = false;

            FilteredElementCollector collector = PCFData.CollectPipeElements(this.Revit.Application.ActiveUIDocument.Document);

            Transaction trans = new Transaction(this.Revit.Application.ActiveUIDocument.Document, "Input Excel data into Family");
            trans.Start();
            try
            {
                foreach (Autodesk.Revit.DB.Element element in collector)
                {
                    string familyType = element.get_Parameter(BuiltInParameter.ELEM_FAMILY_AND_TYPE_PARAM).AsValueString();

                    DataRow data = null;

                    foreach (DataRow row in DT_Components.Rows)
                    {
                        if (familyType.Equals(row.Field<string>("FAMILYTYPE")))
                        {
                            data = row;
                            break;
                        }
                    }

                    if (data != null)
                    {
                        IEnumerable<RevitParam.ParameterDefinition> FamilySharedParam = from RevitParam.ParameterDefinition pd in RevitParam.ParamterList.Values where pd.Scope == RevitParam.Scope.Share select pd;
                        foreach (RevitParam.ParameterDefinition rvtParam in FamilySharedParam)
                        {
                            string rvtParamName = rvtParam.Name.Replace(RevitParam.Prefix, "");
                            if (!(data[rvtParamName] is DBNull) && element.get_Parameter(rvtParam.Id) != null)
                            {
                                switch (rvtParam.Type)
                                {
                                    case (ParameterType.Text):
                                        element.get_Parameter(rvtParam.Id).Set(data.Field<string>(rvtParamName));
                                        break;
                                    case (ParameterType.Integer):
                                        element.get_Parameter(rvtParam.Id).Set((double)data[rvtParamName]);
                                        break;
                                }
                            }
                            
                        }
                        //Commit이 되어야 제대로 Revit이 업데이트가 된 것이다.
                        
                        rt = true;
                    
                    }
                    else
                    {
                        data = DT_Components.NewRow();
                        data.SetField<string>("FAMILYTYPE", familyType);
                        DT_Components.Rows.Add(data);
                    }
                }
                trans.Commit();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return rt;
            }
            catch (Exception ex)
            {
                trans.RollBack();
                MessageBox.Show("Fail: Excel Update: " + ex.Message);
                return false;
            }

            return rt;
        }

        private bool CheckCompData(DataTable table, ref string message)
        {
            bool rt = true;

            if (!table.Columns.Contains("FAMILYTYPE"))
            {
                rt = false;
                message += "Components : FAMILYTYPE\n";

                table.Columns.Add("FAMILYTYPE", typeof(string));
            }

            IEnumerable<RevitParam.ParameterDefinition> FamilySharedParam = from RevitParam.ParameterDefinition pd in RevitParam.ParamterList.Values where pd.Scope == RevitParam.Scope.Share select pd;
            foreach (RevitParam.ParameterDefinition rvtParam in FamilySharedParam)
            {
                //Prefix를 제거한 PCF 변수 이름
                string rvtParamName = rvtParam.Name.Replace(RevitParam.Prefix, "");

                //만약, Excel에 해당 Column이 없으면 추가한다.
                if (!table.Columns.Contains(rvtParamName))
                {
                    rt = false;
                    message += "Components : " + rvtParamName + "\n";

                    Type type = null;
                    switch (rvtParam.Type)
                    {
                        case (ParameterType.Text):
                            type = typeof(string);
                            break;
                        case (ParameterType.Integer):
                            type = typeof(int);
                            break;
                    }

                    if (type != null)
                    {
                        table.Columns.Add(rvtParamName, type);
                    }
                    else
                    {
                        table.Columns.Add(rvtParamName);
                    }
                }

            }

            return rt;
        }

        //하드코딩된 함수. 일단, 확장하거나 변경할 경우가 없거나 적을 것으로 예상된다.
        private bool CheckMatData(DataTable table, ref string message)
        {
            bool rt = true;

            if (!table.Columns.Contains("MATERIAL-IDENTIFIER"))
            {
                rt = false;
                message += "Materials : MATERIAL-IDENTIFIER\n";

                table.Columns.Add("MATERIAL-IDENTIFIER", typeof(int));
            }

            if (!table.Columns.Contains("ITEM-CODE"))
            {
                rt = false;
                message += "Materials : ITEM-CODE\n";
                table.Columns.Add("ITEM-CODE", typeof(string));
            }

            if (!table.Columns.Contains("DESCRIPTION"))
            {
                rt = false;
                message += "Materials : DESCRIPTION\n";
                table.Columns.Add("DESCRIPTION", typeof(string));
            }

            return rt;
        }

        public IDictionary<int, MaterialData> GetMaterials()
        {
            Dictionary<int, MaterialData> materials = new Dictionary<int, MaterialData>();

            foreach (DataRow row in DT_Materials.Rows)
            {
                int material_identifier = Convert.ToInt32(row.Field<double>("MATERIAL-IDENTIFIER"));
                string item_code = row.Field<string>("ITEM-CODE");
                string description = row.Field<string>("DESCRIPTION");

                materials.Add(material_identifier, new MaterialData(material_identifier, item_code, description));
            }
            
            return materials;
        }

        /* 디버깅용 코드
        private void DisplayData(DataTable table)
        {
            StringBuilder msg = new StringBuilder();
            foreach (DataRow row in table.Rows)
            {
                foreach (DataColumn col in table.Columns)
                {
                    msg.AppendLine(col.ColumnName + " = " + row[col]);
                }
                msg.AppendLine("==================================");
            }
            MessageBox.Show(msg.ToString());
        }
        */

        private DataSet GetDataSets(string path)
        {
            string connectOpt = string.Format("provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"", path);
            DataSet data = new DataSet();

            using (OleDbConnection connection = new OleDbConnection(connectOpt))
            {
                connection.Open();

                foreach (string sheetName in GetExcelSheetNames(connection))
                {
                    var dataTable = new DataTable();
                    string query = "SELECT * FROM [" + sheetName + "]";

                    OleDbDataAdapter adapter = new OleDbDataAdapter(query, connection);
                    adapter.Fill(dataTable);

                    //Remove ' and $ from sheetName
                    Regex rgx = new Regex("[^a-zA-Z0-9 _-]");
                    string tableName = rgx.Replace(sheetName, "");

                    dataTable.TableName = tableName;

                    //필수로 필요한 데이터 테이블은 따로 관리
                    if (tableName == "Components")
                    {
                        DT_Components = dataTable;
                    }
                    else if (tableName == "Materials")
                    {
                        DT_Materials = dataTable;
                    }

                    data.Tables.Add(dataTable);
                }

                connection.Close();
            }

            //제대로 Excel 파일이 세팅이 되어 있는지 확인.
            if (data == null)
            {
                throw new Exception("Fail: Data set is null.");
            }
            else if (data.Tables.Count < 1)
            {
                throw new Exception("Fail: Table count in DataSet is 0.");
            }
            else if (DT_Components == null)
            {
                throw new Exception("Fail: Excel file does not have Components sheet.");
            }
            else if (DT_Materials == null)
            {
                throw new Exception("Fail: Excel file does not have Materials sheet.");
            }

            return data;
        }

        private IList<string> GetExcelSheetNames(OleDbConnection connection)
        {
            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {
                return null;
            }

            IList<string> excelSheetNames = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                excelSheetNames.Add(row["TABLE_NAME"].ToString());
            }

            return excelSheetNames;
        }

        /*
         * TODO: (중) 현재 PCFData를 거치지 않고, 바로 Revit과 상호작용하여 정보를 처리하는 중이다.
         * 이는 데이터를 표현하는 방식이 2개가 되어 같은 정보를 두번 처리하는 비효율을 초래하게 된다.
         * 그러므로, 이미 데이터를 해석하여 저장한 PCFData를 활용하길 권고하는 바이다.
        */
        //Excel에 현재까지 작업한 데이터를 쓰는 함수
        public void Export(PCFData data)
        {

            this.UpdateExcelbyRevit();

            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook excelWorkBook = excelApp.Workbooks.Open(this.ExcelPath);

            foreach (DataTable table in this.DataSet.Tables)
            {
                Excel.Worksheet excelWorkSheet = (from Excel.Worksheet ws in excelWorkBook.Sheets where ws.Name.Equals(table.TableName) select ws).First();

                if (excelWorkSheet == null)
                {
                    excelWorkSheet = excelWorkBook.Sheets.Add();
                    excelWorkSheet.Name = table.TableName;
                }

                for (int i = 1; i < table.Columns.Count + 1; i++)
                {
                    excelWorkSheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
                }

                for (int j = 0; j < table.Rows.Count; j++)
                {
                    for (int k = 0; k < table.Columns.Count; k++)
                    {
                        excelWorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                    }
                }
            }

            excelWorkBook.Save();
            excelWorkBook.Close();
            excelApp.Quit();
        }

        /*
         * TODO: (중) PCFData가 Revit정보를 업데이트하는 방식으로 변경할 필요가 있다. 
         * 즉, DataCtrl이 직접적으로 Revit과 접촉하여 정보를 업데이트하는 방식이 아니라,
         * DataCtrl이 PCFData를 변경하면, PCFData가 Revit 정보를 업데이트하는 방식을 채택할 필요가 있다.
         */
        public void Import(PCFData data)
        {

        }
    }
}
