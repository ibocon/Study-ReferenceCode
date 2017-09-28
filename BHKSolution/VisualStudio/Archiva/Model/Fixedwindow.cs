using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    class Fixedwindow : Modelable
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public Data.Size Size;

        public double Frame;

        public Dictionary<String, String> Materials;

        public Fixedwindow(Data.Cord loc, Data.Rot rot, Data.Size size, double frame, Dictionary<String, String> materials)
        {
            this.Location = loc;
            this.Rotation = rot;
            this.Size = size;
            this.Frame = frame;
            this.Materials = materials;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("Fixedwindow");
            xml.WriteVector("Location", Location);
            xml.WriteRotation(Rotation);
            xml.WriteSize(Size);
            xml.Writer.WriteElementString("frame", XmlConvert.ToString(this.Frame));
            xml.WriteMaterials(Materials);

            xml.Writer.WriteEndElement();
        }
    }
}
