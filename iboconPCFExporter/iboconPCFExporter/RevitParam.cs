using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace iboconPCFExporter
{
    public static class RevitParam
    {
        public enum Scope
        {
            Share, Instance
        }
        
        public const string Prefix = "PCF_";

        public class ParameterDefinition
        {
            public Scope Scope;
            public Guid Id;
            public string Name;
            public ParameterType Type;

            public ParameterDefinition(Scope scope, string name, ParameterType type, Guid id)
            {
                this.Scope = scope;
                this.Id = id;
                this.Name = name;
                this.Type = type;
            }

        }

        public enum ParameterName
        {
            Type, Category, Skey, Item_Code, Material_Description, Piping_Spec, Primary_Endtype, Secondary_Endtype, Branch_Endtype
        }

        //새로운 파라미터를 Revit에 추가하고 싶을 때나 제거하고 싶을 때 활용하면 되는 코드
        //Excel과는 Components Sheet과 관련되어 있다.
        public static IDictionary<ParameterName, ParameterDefinition> ParamterList = new Dictionary<ParameterName, ParameterDefinition>()
        {
            #region Share
            { ParameterName.Type, new ParameterDefinition(Scope.Share, Prefix + "TYPE", ParameterType.Text, new Guid("bfc7b779-786d-47cd-9194-8574a5059ec8")) },
            { ParameterName.Category, new ParameterDefinition(Scope.Share, Prefix + "CATAGORY", ParameterType.Text, new Guid("3feebd29-054c-4ce8-bc64-3cff75ed6121")) },
            { ParameterName.Skey, new ParameterDefinition(Scope.Share, Prefix + "SKEY", ParameterType.Text, new Guid("90be8246-25f7-487d-b352-554f810fcaa7")) },
            //Item-Code로 매칭되는 Material Identifier Number를 찾는다.
            { ParameterName.Item_Code, new ParameterDefinition(Scope.Share, Prefix + "ITEM-CODE", ParameterType.Text, new Guid("cbc10825-c0a1-471e-9902-075a41533738")) },
            { ParameterName.Piping_Spec, new ParameterDefinition(Scope.Share, Prefix + "PIPING-SPEC", ParameterType.Text, new Guid("89b1e62e-f9b8-48c3-ab3a-1861a772bda8")) },
            #endregion

            #region Instance
            { ParameterName.Primary_Endtype, new ParameterDefinition(Scope.Instance, Prefix + "PRIMARY-ENDTYPE", ParameterType.Text, new Guid("25f67960-3134-4288-b8a1-c1854cf266c5"))},
            { ParameterName.Secondary_Endtype, new ParameterDefinition(Scope.Instance, Prefix + "SECONDARY-ENDTYPE", ParameterType.Text, new Guid("ecaf3f8a-c28b-4a89-8496-728af3863b09"))},
            { ParameterName.Branch_Endtype, new ParameterDefinition(Scope.Instance, Prefix + "BRANCH-ENDTYPE", ParameterType.Text, new Guid("692e2e97-3b9c-4616-8a03-daa493b01760"))},
            #endregion
        };

        public static Parameter GetParameter(Autodesk.Revit.DB.Element element, RevitParam.ParameterName parameter)
        {
            Guid parameterID = RevitParam.ParamterList[parameter].Id;
            Parameter param = element.get_Parameter(parameterID);
            return param;
        }
    }
}
