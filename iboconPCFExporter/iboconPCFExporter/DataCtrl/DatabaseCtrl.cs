using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iboconPCFExporter.DataCtrl
{
    public class DatabaseCtrl : DataCtrlInterface
    {
        public DatabaseCtrl()
        {

        }

        public void Export(PCFData data)
        {
            throw new NotImplementedException();
        }

        public IDictionary<int, MaterialData> GetMaterials()
        {
            Dictionary<int, MaterialData> materials = new Dictionary<int, MaterialData>();

            /* Temp
            foreach (DataRow row in DT_Materials.Rows)
            {
                int material_identifier = Convert.ToInt32(row.Field<double>("MATERIAL-IDENTIFIER"));
                string item_code = row.Field<string>("ITEM-CODE");
                string description = row.Field<string>("DESCRIPTION");

                materials.Add(material_identifier, new MaterialData(material_identifier, item_code, description));
            }
            */

            return materials;
        }

        public void Import(PCFData data)
        {
            throw new NotImplementedException();
        }
    }
}
