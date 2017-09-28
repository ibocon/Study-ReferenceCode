using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iboconPCFExporter
{
    public class Component : Writable
    {
        private static int elementIdentificationNumber = 0;
        private PCFData Data;
        /*
         * SKEY와 달리 구현된 Component일 때, Connection Type과 Gender가 반드시 필요하다. 
         * 
         */
        #region Component Attributes
        //필수항목
        public string Component_Type;
        public int Component_Identifier;

        public IList<DataType.KeyPoint> KeyPoints;
        public int Material_Identifier;
        //부가항목
        public Attributes Attributes;
        public string AssociatedItem;
        #endregion

        #region Allowable Attributes
        public DataType.CATEGORY Category;
        public DataType.STATUS Status;
        public string Piping_Spec;
        public string Tracing_Spec;
        public string Insulation_Spec;
        public string Painting_Spec;
        public IList<string> Misc_Spec;
        public bool Insulation;
        public DataType.TRACING Tracing;
        public DataType.FLOW Flow;
        public string Unique_Component_Identifier;
        public DataType.Material_List Material_List;
        #endregion
        
        //Component가 Element로 부터 정보를 얻는다!
        public Component(PCFData data, Autodesk.Revit.DB.Element element)
        {
            this.Data = data;

            //초기화
            this.Misc_Spec = new List<string>(5);
            this.KeyPoints = new List<DataType.KeyPoint>();

            //파라미터 값을 추가하는 과정
            this.Component_Type = RevitParam.GetParameter(element, RevitParam.ParameterName.Type).AsString();
            this.Component_Identifier = ++elementIdentificationNumber;

            //Material 처리
            this.Material_Identifier = this.Data.GetMaterialData(RevitParam.GetParameter(element, RevitParam.ParameterName.Item_Code).AsString()).Material_Identifier;

            this.Category = DataType.EnumString.CATEGORY.GetBySecond(RevitParam.GetParameter(element, RevitParam.ParameterName.Category).AsString());
            this.Piping_Spec = RevitParam.GetParameter(element, RevitParam.ParameterName.Piping_Spec).AsString();
            this.Unique_Component_Identifier = element.UniqueId;

        }

        //Component의 정보를 어떻게 PCF로 기록할지 정의한다!
        public virtual void Write(StringBuilder writer)
        {
            writer.Append(this.Component_Type.ToUpper()).AppendLine();
            writer.Append(PCFWriter.TAB).Append("COMPONENT-IDENTIFIER " + this.Component_Identifier).AppendLine();

            foreach (DataType.KeyPoint kp in this.KeyPoints)
            {
                kp.Write(writer);
                writer.AppendLine();
            }

            writer.Append(PCFWriter.TAB).Append("MATERIAL-IDENTIFIER " + this.Material_Identifier).AppendLine();
            writer.Append(PCFWriter.TAB).Append("CATEGORY " + this.Category).AppendLine();
            writer.Append(PCFWriter.TAB).Append("PIPING-SPEC " + this.Piping_Spec.ToUpper()).AppendLine();
            writer.Append(PCFWriter.TAB).Append("UCI " + this.Unique_Component_Identifier.ToUpper()).AppendLine();

        }

        protected void getCentrePoint(Autodesk.Revit.DB.Element element)
        {
            this.KeyPoints.Add(new DataType.InternalKeyPoint(DataType.KeypointType.CENTRE, ((element as FamilyInstance).Location as LocationPoint).Point));
        }

        protected DataType.ConnectionType getEndtype(Autodesk.Revit.DB.Element element, RevitParam.ParameterName parameter)
        {
            DataType.ConnectionType endtype = DataType.ConnectionType.NONE;
            Parameter param = RevitParam.GetParameter(element, parameter);
            if (param != null && !string.IsNullOrEmpty(param.AsString()))
            {
                endtype = DataType.EnumString.ConnectionType.GetBySecond(param.AsString().ToUpper());
            }
            return endtype;
        }

        protected IList<Connector> getPrimaryandSecondaryPoint(Autodesk.Revit.DB.Element element)
        {
            #region Reference Code
            /* 레퍼런스용 코드
            if (element is Autodesk.Revit.DB.MEPCurve)
            {
                LocationCurve lc = (element as Autodesk.Revit.DB.MEPCurve).Location as LocationCurve;
                Debug.Assert(null != lc, "expected element(" + element.Name + ") to have valid location curve.");

                if (null != lc)
                {
                    Curve c = lc.Curve;

                    XYZ startEndPoint = c.GetEndPoint(0);
                    this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, startEndPoint, element.LookupParameter("Diameter").AsDouble()));

                    XYZ finishEndPoint = c.GetEndPoint(1);
                    this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, finishEndPoint, element.LookupParameter("Diameter").AsDouble()));

                }
            }
            */
            #endregion

            ConnectorSet connectorSet = connectorSet = (element as FamilyInstance).MEPModel.ConnectorManager.Connectors;
            
            IList<Connector> leftOver = new List<Connector>();

            foreach (Connector connector in connectorSet)
            {
                //End Point Primary
                if (connector.GetMEPConnectorInfo().IsPrimary)
                {
                    this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connector.Origin, connector.Radius, this.getEndtype(element, RevitParam.ParameterName.Primary_Endtype)));
                }
                //End Point Secondary
                else if (connector.GetMEPConnectorInfo().IsSecondary)
                {
                    this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connector.Origin, connector.Radius, this.getEndtype(element, RevitParam.ParameterName.Secondary_Endtype)));
                }
                else
                {
                    leftOver.Add(connector);
                }
            }
            //만약, Revit Family의 Connector 설정을 잘못 작업했을 때를 대비한 코드
            IList<Connector> result = new List<Connector>(leftOver);
            if (this.KeyPoints.Count < 2)
            {
                foreach (Connector connector in leftOver)
                {
                    if (this.KeyPoints.Count == 0)
                    {
                        this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connector.Origin, connector.Radius, this.getEndtype(element, RevitParam.ParameterName.Primary_Endtype)));
                        result.Remove(connector);
                    }
                    else if (this.KeyPoints.Count == 1)
                    {
                        this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connector.Origin, connector.Radius, this.getEndtype(element, RevitParam.ParameterName.Secondary_Endtype)));
                        result.Remove(connector);
                    }
                }
            }

            return result;
        }

        protected void getEndPointsWithSingleConnector(Autodesk.Revit.DB.Element element)
        {
            //First End Point
            ConnectorSet connectorSet = connectorSet = (element as FamilyInstance).MEPModel.ConnectorManager.Connectors;

            Connector connector = connectorSet.Cast<Connector>().First();

            this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connector.Origin, connector.Radius, this.getEndtype(element, RevitParam.ParameterName.Primary_Endtype)));

            //Second End Point
            XYZ endPointOrigin = connector.Origin;
            double connectorSize = connector.Radius;

            //Connector의 Second End Point를 구하기 위해, Connector의 Normal Vector를 활용한다.
            XYZ normalVector = connector.CoordinateSystem.BasisZ.Negate();
            Line normalLine = Line.CreateUnbound(endPointOrigin, normalVector);

            //Geometry와 Normal Line이 교차하는 지점이 있는지 확인한다.
            Options opt = new Options();
            opt.DetailLevel = ViewDetailLevel.Fine;
            GeometryElement geometryElement = (element as FamilyInstance).get_Geometry(opt);

            //Prepare resulting point
            XYZ epSecondary = null;

            foreach (GeometryObject geometry in geometryElement)
            {
                GeometryInstance instance = geometry as GeometryInstance;
                if (null == instance) continue;

                foreach (GeometryObject instanceObject in instance.GetInstanceGeometry())
                {
                    Solid solid = instanceObject as Solid;
                    if (solid == null || 0 == solid.Faces.Size) continue;

                    //각 면을 체크하며, Connector의 Normal Vector과 겹치는지 확인
                    foreach (Face face in solid.Faces)
                    {
                        IntersectionResultArray results = null;
                        SetComparisonResult result = face.Intersect(normalLine, out results);
                        if (result == SetComparisonResult.Overlap)
                        {
                            foreach (IntersectionResult intersectionResult in results)
                            {
                                //Connector Point가 아니라면!
                                if (intersectionResult.XYZPoint.IsAlmostEqualTo(endPointOrigin) == false)
                                {
                                    epSecondary = intersectionResult.XYZPoint;
                                    break;
                                }
                            }
                            if (epSecondary != null) break;
                        }
                    }
                    if (epSecondary != null) break;
                }
            }

            //만약, EndPoint를 찾지 못했다면!
            if (epSecondary == null) throw new Exception("Fail: Get Secondary End Point from Single Connector Calculation Error.");

            this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, epSecondary, connectorSize, this.getEndtype(element, RevitParam.ParameterName.Secondary_Endtype)));
        }
    }

    public class Pipe : Component
    {
        public Pipe(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            ConnectorSet connectorSet = (element as Autodesk.Revit.DB.Plumbing.Pipe).ConnectorManager.Connectors;
            IList < Connector > connectorEnd = (from Connector connector in connectorSet where connector.ConnectorType == ConnectorType.End select connector).ToList();

            //Primary
            this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connectorEnd.First().Origin, connectorEnd.First().Radius, this.getEndtype(element, RevitParam.ParameterName.Primary_Endtype)));
            
            //Secondary
            this.KeyPoints.Add(new DataType.ExternalKeyPoint(DataType.KeypointType.END, connectorEnd.Last().Origin, connectorEnd.Last().Radius, this.getEndtype(element, RevitParam.ParameterName.Secondary_Endtype)));
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Element : Component
    {
        public string Skey;

        public Element(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.Skey = RevitParam.GetParameter(element, RevitParam.ParameterName.Skey).AsString();
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
            writer.Append(PCFWriter.TAB).Append("SKEY " + this.Skey.ToUpper()).AppendLine();
        }
    }

    public class Elbow : Element
    {
        public double Angle;

        public Elbow(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {

            this.getPrimaryandSecondaryPoint(element);

            this.getCentrePoint(element);

            //TODO: (하) 패밀리의 변수 이름 "각도"가 한글이면 한글로 변수 값을 찾아야 하는 문제가 있다.
            if (element.LookupParameter("Angle") == null)
            {
                this.Angle = element.LookupParameter("각도").AsDouble();
            }
            else
            {
                this.Angle = element.LookupParameter("Angle").AsDouble();
            }
            
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
            writer.Append(PCFWriter.TAB).Append("ANGLE " + String.Format("{0:0}", this.Angle * PCFWriter.UnitAngleToDeg)).AppendLine();
        }
    }

    public class Tee : Element
    {
        public Tee(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {

            //남은 Connector은 모두 Branch Point로 취급한다.
            IList<Connector> leftOver = this.getPrimaryandSecondaryPoint(element);

            int branchNumber = 0;
            foreach (Connector connector in leftOver)
            {
                this.KeyPoints.Add(new DataType.BranchPoint(++branchNumber, DataType.KeypointType.BRANCH, connector.Origin, connector.Radius, this.getEndtype(element, RevitParam.ParameterName.Branch_Endtype)));
            }

            this.getCentrePoint(element);

        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Flange : Element
    {
        //TODO:어떻게 LeftLoose 값을 element를 통해 결정할지 모름. Primary나 Secondary 중 isConnected로 추정.
        //bool isLeftLoose = false; 

        public Flange(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
            //writer.Append(PCFWriter.TAB).Append("FLANGE-LEFT-LOOSE " + (isLeftLoose ? "ON": "OFF")).AppendLine();
        }
    }

    public class Flange_Blind : Element
    {
        public Flange_Blind(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getEndPointsWithSingleConnector(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Valve : Element
    {
        public Valve(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
            this.getCentrePoint(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Reducer_Concentric : Element
    {
        public Reducer_Concentric(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Reducer_Eccentric : Element
    {
        public Reducer_Eccentric(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Cap : Element
    {
        public Cap(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getEndPointsWithSingleConnector(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Coupling : Element
    {
        public Coupling(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
        }
    }

    public class Filter : Element
    {
        public Filter(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
        }
    }

    public class Instrument : Element
    {
        public Instrument(PCFData data, Autodesk.Revit.DB.Element element) : base(data, element)
        {
            this.getPrimaryandSecondaryPoint(element);
            this.getCentrePoint(element);
        }
    }

    //TODO: (개념) 예제 PCF파일로 어떻게 활용되는지 확인하고 구현할 필요가 있다.
    public class AssociatedComponent : Component
    {
        //TODO: (하) 모든 변수가 고려되지 않았다. PCF 문서의 Associated Components를 참고하라.
        public int Mater_Component_Identifier;
        public double Size;
        public string Item_Code;
        public int Repeat_Part_Number;
        public double Weight;
        public double Quantity;
        public double Length;

        public AssociatedComponent(PCFData data, Autodesk.Revit.DB.Element element, IList<Component> components) : base(data, element)
        {
            this.Mater_Component_Identifier = this.GetMaterComponentIdentifier(element, components);
        }

        public override void Write(StringBuilder writer)
        {
            base.Write(writer);
            writer.Append(PCFWriter.TAB).Append("MASTER-COMPONENT-IDENTIFIER " + this.Mater_Component_Identifier).AppendLine();

        }

        public int GetMaterComponentIdentifier(Autodesk.Revit.DB.Element element, IList<Component> components)
        {
            Autodesk.Revit.DB.Element materElement = (element as FamilyInstance).SuperComponent;

            Component materComponent = (from Component c in components where c.Unique_Component_Identifier == materElement.UniqueId select c).First();

            if(materComponent == null)
            {
                throw new ArgumentNullException("No Mater Component for "+ element.Name + ".\n");
            }

            return materComponent.Component_Identifier;
        }

    }

    public class Gasket : AssociatedComponent
    {
        public Gasket(PCFData data, Autodesk.Revit.DB.Element element, IList<Component> components) : base(data, element, components)
        {
            this.getPrimaryandSecondaryPoint(element);
        }
    }
}
