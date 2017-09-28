using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Model
{
    class Hiproof
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public double RoofHeight;
        public double SideRafter;
        public double MainRafter;
        public double RidgeBoard;
        public Dictionary<String, String> Materials;

        public Hiproof(Data.Cord loc, Data.Rot rot, double roofH, double sideR, double mainR, double ridgeB, Dictionary<String, String> materials)
        {
            this.Location = loc;
            this.Rotation = rot;
            this.RoofHeight = roofH;
            this.SideRafter = sideR;
            this.MainRafter = mainR;
            this.RidgeBoard = ridgeB;
            this.Materials = materials;
        }
    }
}
