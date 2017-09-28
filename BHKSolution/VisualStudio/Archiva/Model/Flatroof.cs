using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    class Flatroof : Modelable
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public double RoofHeight;
        public double SideRafter;
        public double MainRafter;
        public Dictionary<String, String> Materials;

        public Flatroof(Data.Cord loc, Data.Rot rot, double roofH, double sideR, double mainR, Dictionary<String, String> materials)
        {
            this.Location = loc;
            this.Rotation = rot;
            this.RoofHeight = roofH;
            this.SideRafter = sideR;
            this.MainRafter = mainR;
            this.Materials = materials;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("FlatRoof");

            xml.WriteVector("Location", Location);
            xml.WriteRotation(Rotation);

            xml.Writer.WriteElementString("RoofHeight", XmlConvert.ToString(RoofHeight));
            xml.Writer.WriteElementString("SideRafter", XmlConvert.ToString(SideRafter));
            xml.Writer.WriteElementString("MainRafter", XmlConvert.ToString(MainRafter));

            xml.WriteMaterials(Materials);

            xml.Writer.WriteEndElement();
        }
    }
}
