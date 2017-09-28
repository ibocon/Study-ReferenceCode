using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iboconPCFExporter
{
    public class ParamBinding
    {
        public Result Generate(ExternalCommandData commandData, ref string message)
        {
            Document document = commandData.Application.ActiveUIDocument.Document;

            System.Text.StringBuilder log = new System.Text.StringBuilder();

            Transaction trans = new Transaction(document, "Generate PCF parameters binding");
            trans.Start();
            try
            {
                

                DefinitionFile sharedParamFile = document.Application.OpenSharedParameterFile();
                if (sharedParamFile == null)
                {
                    string ExecutingAssemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string OriginalFile = document.Application.SharedParametersFilename;
                    string CustomFile = ExecutingAssemblyDirectory + "\\PCFSharedParameters.txt";
                    using (File.Create(CustomFile)) { }
                    document.Application.SharedParametersFilename = CustomFile;
                    sharedParamFile = document.Application.OpenSharedParameterFile();

                    if (sharedParamFile == null)
                    {
                        throw new Exception("Fail: Create Shared Paramters for PCF Exporting.");
                    }
                }

                string groupname = "PCF_SharedParameter";

                DefinitionGroup grp = sharedParamFile.Groups.FirstOrDefault(g => g.Name == groupname);
                if (grp == null)
                {
                    grp = sharedParamFile.Groups.Create(groupname);
                }

                CategorySet categories = document.Application.Create.NewCategorySet();
                categories.Insert(document.Settings.Categories.get_Item(BuiltInCategory.OST_PipingSystem));
                categories.Insert(document.Settings.Categories.get_Item(BuiltInCategory.OST_PipeCurves));
                categories.Insert(document.Settings.Categories.get_Item(BuiltInCategory.OST_PipeFitting));
                categories.Insert(document.Settings.Categories.get_Item(BuiltInCategory.OST_PipeAccessory));

                IList<Definition> defs = new List<Definition>();

                foreach (RevitParam.ParameterDefinition pd in RevitParam.ParamterList.Values)
                {
                    if (!this.isParamExist(grp.Definitions, pd))
                    {
                        ExternalDefinitionCreationOptions edco = new ExternalDefinitionCreationOptions(pd.Name, pd.Type);
                        edco.GUID = pd.Id;
                        defs.Add(grp.Definitions.Create(edco));
                    }
                    else
                    {
                        defs.Add(grp.Definitions.get_Item(pd.Name));
                    }
                }
                
                BindingMap bindingMap = document.ParameterBindings;
                Autodesk.Revit.DB.Binding binding = document.Application.Create.NewInstanceBinding(categories);

                foreach (Definition def in defs)
                {
                    if (bindingMap.Contains(def))
                    {
                        log.Append("Parameter " + def.Name + " already exists.\n");
                    }
                    else
                    {
                        bindingMap.Insert(def, binding, BuiltInParameterGroup.PG_ANALYTICAL_MODEL);
                        if (bindingMap.Contains(def))
                        {
                            log.Append("Parameter " + def.Name + " added to project.\n");
                        }
                        else
                        {
                            log.Append("Creation of parameter " + def.Name + " failed for some reason.\n");
                        }
                    }
                }
                
                trans.Commit();
            }
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            catch (Exception ex)
            {
                trans.RollBack();
                message = ex.Message;
                return Result.Failed;
            }

            MessageBox.Show(log.ToString());

            return Result.Succeeded;
        }

        private bool isParamExist(Definitions ds, RevitParam.ParameterDefinition pd)
        {

            foreach (ExternalDefinition d in ds)
            {
                if (d.GUID == pd.Id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
