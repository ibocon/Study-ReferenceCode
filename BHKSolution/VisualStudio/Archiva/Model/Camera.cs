using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva.Model
{
    class Camera : Modelable
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public int CameraNumber;

        public Camera(Data.Cord location, Data.Rot rotation, int cameraNumber)
        {
            this.Location = location;
            this.Rotation = rotation;
            this.CameraNumber = cameraNumber;
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("View");

            xml.WriteVector("Location", Location);
            xml.WriteRotation(this.Rotation);

            xml.Writer.WriteElementString("CameraNumber", XmlConvert.ToString(this.CameraNumber));

            xml.Writer.WriteEndElement();
        }
    }
}
