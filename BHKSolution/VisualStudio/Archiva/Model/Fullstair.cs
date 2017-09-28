using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    class Fullstair : Modelable
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public int StepNumber;
        public double StepLength;
        public double StepWidth;
        public double StepHeight;
        public double Foundation;

        public Dictionary<String, String> Materials;

        public Fullstair(Data.Cord location, Data.Rot rotation, int stepNumber, double stepLength, double stepWidth, double stepHeight, double foundation, Dictionary<String, String> materials)
        {
            this.Location = location;
            this.Rotation = rotation;
            this.StepNumber = stepNumber;
            this.StepLength = stepLength;
            this.StepWidth = stepWidth;
            this.StepHeight = stepHeight;
            this.Foundation = foundation;
            this.Materials = materials;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("FullStair");
            xml.WriteVector("Location", this.Location);
            xml.WriteRotation(this.Rotation);
            xml.Writer.WriteElementString("StepNumber", XmlConvert.ToString(this.StepNumber));
            xml.Writer.WriteElementString("StepLength", XmlConvert.ToString(this.StepLength));
            xml.Writer.WriteElementString("StepWidth", XmlConvert.ToString(this.StepWidth));
            xml.Writer.WriteElementString("StepHeight", XmlConvert.ToString(this.StepHeight));

            xml.Writer.WriteElementString("Foundation", XmlConvert.ToString(this.Foundation));

            xml.WriteMaterials(Materials);

            xml.Writer.WriteEndElement();
        }
    }
}
