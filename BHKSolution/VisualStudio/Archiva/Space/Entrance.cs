using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Space
{
    class Entrance : Room
    {
        public Entrance(Data.Size size, Dictionary<Model.ModelType, Dictionary<string, string>> materials, Dictionary<StaticCompType, string> staticComp) : base(size, materials, staticComp)
        {

        }

        public override object Clone()
        {
            Entrance clone = new Entrance(Size, Materials, StaticComponents);
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
    }
}
