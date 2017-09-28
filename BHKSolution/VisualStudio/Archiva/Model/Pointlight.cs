using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    class Pointlight : Modelable
    {
        public Data.Cord Location;
        public double Intensity;
        public int Red, Green, Blue;
        
        public Pointlight(Data.Cord location, double intensity, int red, int green, int blue)
        {
            this.Location = location;
            this.Intensity = intensity;
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("PointLight");

            xml.WriteVector("Location", Location);
            xml.Writer.WriteElementString("Intensity", XmlConvert.ToString(this.Intensity));

            xml.Writer.WriteStartElement("Color");
            xml.Writer.WriteElementString("r", XmlConvert.ToString(this.Red));
            xml.Writer.WriteElementString("g", XmlConvert.ToString(this.Green));
            xml.Writer.WriteElementString("b", XmlConvert.ToString(this.Blue));
            xml.Writer.WriteEndElement();

            xml.Writer.WriteEndElement();
        }
    }
}
