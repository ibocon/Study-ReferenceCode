using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Space
{
    class Kitchen : Room
    {
        public Kitchen(Data.Size size, Dictionary<Model.ModelType, Dictionary<string, string>> materials, Dictionary<StaticCompType, string> staticComp) : base(size, materials, staticComp)
        {

        }

        public override object Clone()
        {
            Kitchen clone = new Kitchen(Size, Materials, StaticComponents);
            clone.Location = (Data.Cord)this.Location.Clone();
            clone.id = this.id;
            Dictionary<Data.SideCardinalDirection, List<Data.Side>> tempSides = new Dictionary<Data.SideCardinalDirection, List<Data.Side>>();

            foreach (Data.SideCardinalDirection sidePosition in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                tempSides[sidePosition] = new List<Data.Side>();
                foreach (Data.Side part in Sides[sidePosition])
                {
                    tempSides[sidePosition].Add((Data.Side)part.Clone());
                }
            }
            clone.Sides = tempSides;
            return clone;
        }

        protected override void CreateFurniture()
        {
            base.CreateFurniture();
        }

        protected override void CreateLight()
        {
            int red = 100;
            int blue = 255;
            int green = 100;

            double lightIntensity = (this.Size.Length * this.Size.Width) / 16;

            Model.Pointlight pointLight = new Model.Pointlight(new Data.Cord(Location.X + (0.5 * Size.Length), Location.Y + (0.5 * Size.Width), Location.Z + Size.Height), lightIntensity, red, green, blue);
            this.Components.Add(pointLight);
        }
    }
}
