using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    class Wall : Modelable
    {
        public Data.Cord Start;
        public Data.Cord End;

        public double Width;
        public double Height;

        public List<Data.Hole> Holes;

        public Dictionary<String, String> Materials;

        public Wall(Data.Cord startPoint, Data.Cord endPoint, double width, double height, List<Data.Hole> holes, Dictionary<String, String> materials)
        {
            this.Start = startPoint;
            this.End = endPoint;
            this.Width = width;
            this.Height = height;
            this.Holes = holes;
            this.Materials = materials;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("Wall");
            xml.WriteVector("Start", Start);
            xml.WriteVector("End", End);
            xml.Writer.WriteElementString("Width", XmlConvert.ToString(Width));
            xml.Writer.WriteElementString("Height", XmlConvert.ToString(Height));

            xml.Writer.WriteStartElement("Holes");
            foreach (Data.Hole hole in Holes)
            {
                xml.Writer.WriteStartElement("Hole");

                xml.Writer.WriteElementString("x", XmlConvert.ToString(hole.X));
                xml.Writer.WriteElementString("y", XmlConvert.ToString(hole.Y));
                xml.Writer.WriteElementString("l", XmlConvert.ToString(hole.L));
                xml.Writer.WriteElementString("h", XmlConvert.ToString(hole.H));

                xml.Writer.WriteEndElement();
            }
            xml.Writer.WriteEndElement();

            xml.WriteMaterials(Materials);

            xml.Writer.WriteEndElement();
        }
    }
}
