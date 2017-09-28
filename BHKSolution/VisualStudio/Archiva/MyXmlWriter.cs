using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Archiva
{
    class MyXmlWriter
    {
        private string FilePath;
        public XmlTextWriter Writer;

        public MyXmlWriter(string path)
        {
            this.FilePath = path;
            try
            {
                Writer = new XmlTextWriter(path, Encoding.UTF8);
                Writer.Formatting = Formatting.Indented;
                Writer.Indentation = 4;
            }
            catch (System.UnauthorizedAccessException ex)
            {
                Console.WriteLine("\nError\n\t" + ex.Message +"\n");
            }

        }

        public void StartToWrite()
        {
            Writer.WriteStartDocument();
            Writer.WriteStartElement("Architecture");
        }

        /*
        public void WriteRoom(Space.Room room)
        {
            writer.WriteStartElement("Room");

            
            this.WriteFloor(room.Floor);

            foreach(Model.Wall wall in room.Walls)
            {
                this.WriteWall(wall);
            }
            

            foreach (Model.Modelable model in room.Components)
            {
                model.WriteModel(this);
            }

            writer.WriteEndElement();
        }

        public void WriteWall(Model.Wall wall)
        {
            writer.WriteStartElement("Wall");
            this.WriteVector("Start", wall.Start);
            this.WriteVector("End", wall.End);
            writer.WriteElementString("Width", XmlConvert.ToString(wall.Width));
            writer.WriteElementString("Height", XmlConvert.ToString(wall.Height));

            writer.WriteStartElement("Holes");
            foreach (Data.Hole hole in wall.Holes)
            {
                writer.WriteStartElement("Hole");

                writer.WriteElementString("x", XmlConvert.ToString(hole.X));
                writer.WriteElementString("y", XmlConvert.ToString(hole.Y));
                writer.WriteElementString("l", XmlConvert.ToString(hole.L));
                writer.WriteElementString("h", XmlConvert.ToString(hole.H));

                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            this.WriteMaterials(wall.Materials);

            writer.WriteEndElement();
        }
        
        public void WriteFloor(Model.Floor floor)
        {
            writer.WriteStartElement("Floor");

            this.WriteVector("Location", floor.Location);

            writer.WriteStartElement("Move");
            writer.WriteElementString("x", "0");
            writer.WriteElementString("y", "0");
            writer.WriteElementString("z", XmlConvert.ToString(floor.Thickness));
            writer.WriteEndElement();

            this.WriteVertices(floor.Vertices);

            this.WriteMaterials(floor.Materials);

            writer.WriteEndElement();
        }
        */

        public void WriteVector(string vectorName, Data.Cord vector)
        {
            Writer.WriteStartElement(vectorName);
            Writer.WriteElementString("x", XmlConvert.ToString(vector.X));
            Writer.WriteElementString("y", XmlConvert.ToString(vector.Y));
            Writer.WriteElementString("z", XmlConvert.ToString(vector.Z));
            Writer.WriteEndElement();
        }

        public void WriteRotation(Data.Rot rotation)
        {
            Writer.WriteStartElement("Rotation");
            Writer.WriteElementString("r", XmlConvert.ToString(rotation.Roll));
            Writer.WriteElementString("p", XmlConvert.ToString(rotation.Pitch));
            Writer.WriteElementString("y", XmlConvert.ToString(rotation.Yaw));
            Writer.WriteEndElement();
        }

        public void WriteSize(Data.Size size)
        {
            Writer.WriteStartElement("Size");
            Writer.WriteElementString("l", XmlConvert.ToString(size.Length));
            Writer.WriteElementString("w", XmlConvert.ToString(size.Width));
            Writer.WriteElementString("h", XmlConvert.ToString(size.Height));
            Writer.WriteEndElement();
        }

        public void WriteVertices(List<Data.Cord> vertices)
        {
            Writer.WriteStartElement("Vertices");
            foreach (Data.Cord vert in vertices)
            {
                Writer.WriteStartElement("Vertice");
                Writer.WriteElementString("x", XmlConvert.ToString(vert.X));
                Writer.WriteElementString("y", XmlConvert.ToString(vert.Y));
                Writer.WriteElementString("z", XmlConvert.ToString(vert.Z));
                Writer.WriteEndElement();
            }
            Writer.WriteEndElement();
        }

        public void WriteMaterials(Dictionary<String, String> materials)
        {
            Writer.WriteStartElement("Materials");

            foreach (KeyValuePair<string, string> material in materials)
            {
                Writer.WriteStartElement("Material");
                Writer.WriteElementString("part",material.Key);
                Writer.WriteElementString("path", material.Value);
                Writer.WriteEndElement();
            }

            Writer.WriteEndElement();
        }

        public void EndToWrite()
        {
            Writer.WriteEndElement();
            Writer.WriteEndDocument();
            Writer.Flush();
        }
    }

}   
