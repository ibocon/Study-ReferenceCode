using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using iboconPCFExporter;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace iboconPCFExporter
{
    //TODO: (진행) PCF 파일을 만들기 위해 필요한 파라미터 값목록을 저장하는 클래스를 구현해야 한다.
    //TODO: (개념) 변수의 사용 목적과 변수 간의 관계 파악이 필요하다.
    //TODO: (하) 빠른 프로토타입을 위해, Information Items을 제외하고 구현했다. 나중에 추가해야 한다.
    public class PCFData
    {
        static public UIApplication Application;
        static public Document Document;
        #region Basic Header
        public BasicHeader BasicHeader;
        #endregion

        #region Pipeline Header
        public PipelineHeader PipelineHeader;
        #endregion

        #region Components
        public IList<Component> Components;
        public IDictionary<int, MaterialData> Materials;
        #endregion

        public PCFData()
        {
            this.BasicHeader = new BasicHeader();
            this.PipelineHeader = new PipelineHeader();
            this.Components = new List<Component>();
            this.Materials = new Dictionary<int, MaterialData>();
        }

        //Revit에서 추출가능한 정보를 Parameter 내부에 저장하도록 구현.
        public Result Create(ExternalCommandData commandData, ref string message, DataCtrl.DataCtrlInterface dataCtrl)
        {
            PCFData.Application = commandData.Application;
            PCFData.Document = PCFData.Application.ActiveUIDocument.Document;

            //PCF를 작성하기 위해 필요한 element를 찾아 낸다.
            FilteredElementCollector collector = PCFData.CollectPipeElements(PCFData.Document);

            try
            {
                //Revit에 있는 정보를 활용하여, PCF에 쓰일 Component 정보를 수집한다.
                foreach (Autodesk.Revit.DB.Element element in collector)
                {
                    //Host 역할을 하는 FamilyInstance
                    if (element is Autodesk.Revit.DB.Plumbing.Pipe || (element is FamilyInstance && (element as FamilyInstance).SuperComponent == null))
                    {
                        this.CreateComponents(element);
                    }
                }

                //Excel에 있는 Material 정보를 수집하여 저장한다.
                this.Materials = dataCtrl.GetMaterials();

            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

            return Result.Succeeded;
        }

        public MaterialData GetMaterialData(string itemCode)
        {
            if (itemCode == null)
            {
                throw new Exception("Item Code does not have value!");
            }
            IEnumerable<MaterialData> search = from MaterialData md in this.Materials.Values where md.Item_Code.Equals(itemCode) select md;

            if (search.Any())
            {
                return search.First();
            }
            else
            {
                throw new Exception("Item Code does not exist!");
            }
        }

        public void CreateComponents(Autodesk.Revit.DB.Element element)
        {
            Component component = null;
            //element가 파이프일 경우
            if (element is Autodesk.Revit.DB.Plumbing.Pipe)
            {
                component = new Pipe(this, element);
            }
            else if (element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_PipeFitting || element.Category.Id.IntegerValue == (int)BuiltInCategory.OST_PipeAccessory)
            {
                //TODO: (하) 타입을 확인하는 다른 방법이 있는지 찾아보자.
                string type = RevitParam.GetParameter(element, RevitParam.ParameterName.Type).AsString().ToUpper();
                switch (type)
                {
                    //TODO: (상) ELBOW SKEY의 결정은 EndType과 각도로 결정된다. 자동으로 결정할 수 있을까?
                    case ("ELBOW"):
                        component = new Elbow(this, element);
                        break;
                    case ("TEE"):
                        component = new Tee(this, element);
                        break;
                    case ("FLANGE"):
                        component = new Flange(this, element);
                        break;
                    case ("VALVE"):
                        component = new Valve(this, element);
                        break;
                    case ("REDUCER-CONCENTRIC"):
                        component = new Reducer_Concentric(this, element);
                        break;
                    case ("REDUCER-ECCENTRIC"):
                        component = new Reducer_Eccentric(this, element);
                        break;
                    case ("FLANGE-BLIND"):
                        component = new Flange_Blind(this, element);
                        break;
                    case ("CAP"):
                        component = new Cap(this, element);
                        break;
                    case ("COUPLING"):
                        component = new Coupling(this, element);
                        break;
                    case ("GASKET"):
                        component = new Gasket(this, element, this.Components);
                        break;
                    case ("FILTER"):
                        component = new Filter(this, element);
                        break;
                    case ("INSTRUMENT"):
                        component = new Instrument(this, element);
                        break;
                    case ("NONE"):
                        return;
                }
            }

            if (component != null)
            {
                this.Components.Add(component);
            }
            else
            {
                throw new Exception("Fail: not supported component type. Sorry! \n" + element.Name);
            }

            if (element is FamilyInstance)
            {
                ICollection<ElementId> SubComponentIds = (element as FamilyInstance).GetSubComponentIds();
                if (SubComponentIds != null)
                {
                    foreach (ElementId id in SubComponentIds)
                    {
                        FamilyInstance subElement = element.Document.GetElement(id) as FamilyInstance;
                        if (subElement != null)
                        {
                            this.CreateComponents(subElement);
                        }
                    }
                }
            }
        }

        public static FilteredElementCollector CollectPipeElements(Document document)
        {
            FilteredElementCollector collector = new FilteredElementCollector(document);

            LogicalOrFilter categoryFilter = new LogicalOrFilter(new List<ElementFilter>()
            {
                new ElementCategoryFilter(BuiltInCategory.OST_PipingSystem),
                new ElementCategoryFilter(BuiltInCategory.OST_FlexPipeCurves),
                new ElementCategoryFilter(BuiltInCategory.OST_PipeFitting),
                new ElementCategoryFilter(BuiltInCategory.OST_PipeAccessory)
            });
            
            LogicalAndFilter instanceFilter = new LogicalAndFilter(categoryFilter, new ElementClassFilter(typeof(FamilyInstance)));

            LogicalOrFilter pipeFilter = new LogicalOrFilter(instanceFilter, new ElementClassFilter(typeof(Autodesk.Revit.DB.Plumbing.Pipe)));

            return collector.WherePasses(pipeFilter);
        }

       
    }

    public class MaterialData : Writable
    {
        public int Material_Identifier;
        public string Item_Code;
        public string Item_Description;

        public MaterialData(int mat_id, string item_code, string item_desc)
        {
            Material_Identifier = mat_id;
            Item_Code = item_code;
            Item_Description = item_desc;
        }

        public void Write(StringBuilder Writer)
        {
            Writer.Append("MATERIAL-IDENTIFIER " + this.Material_Identifier).AppendLine();
            Writer.Append(PCFWriter.TAB).Append("ITEM-CODE " + this.Item_Code).AppendLine();
            Writer.Append(PCFWriter.TAB).Append("DESCRIPTION " + this.Item_Description).AppendLine();
        }
    }

    public class Attributes
    {

    } 
}
