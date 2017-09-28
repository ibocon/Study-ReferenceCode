using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Xml;

namespace Archiva.Model
{
    class Floor : Modelable
    {
        public Data.Cord Location;
        public double Thickness;
        //순서대로 삽입되어야 한다. Y축으로 이동이 우선이다.
        //TODO: 순서상관없이 정점을 입력할 수 있도록, 개선하자!
        public List<Data.Cord> Vertices;

        public Dictionary<string, string> Materials;

        public Floor(Data.Cord location, double thickness, List<Data.Cord> verts, Dictionary<string, string> materials)
        {
            this.Location = location;
            this.Thickness = thickness;
            this.Vertices = verts;
            this.Materials = materials;

        }
        public bool IntersectWith(Floor that)
        {
            return false;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("Floor");

            xml.WriteVector("Location", Location);

            xml.Writer.WriteStartElement("Move");
            xml.Writer.WriteElementString("x", "0");
            xml.Writer.WriteElementString("y", "0");
            xml.Writer.WriteElementString("z", XmlConvert.ToString(Thickness));
            xml.Writer.WriteEndElement();

            xml.WriteVertices(Vertices);

            xml.WriteMaterials(Materials);

            xml.Writer.WriteEndElement();
        }
    }
}