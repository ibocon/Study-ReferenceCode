using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Model
{
    class Static : Modelable
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public string Mesh;

        public Static(Data.Cord loc, Data.Rot rot, string mesh)
        {
            this.Location = loc;
            this.Rotation = rot;
            this.Mesh = mesh;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("Static");
            xml.WriteVector("Location", Location);
            xml.WriteRotation(Rotation);
            xml.Writer.WriteElementString("Mesh", Mesh);
            xml.Writer.WriteEndElement();
        }
    }
}
