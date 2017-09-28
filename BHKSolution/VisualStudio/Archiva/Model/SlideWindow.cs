using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Model
{
    class Slidewindow
    {
        public Data.Cord Location;
        public Data.Rot Rotation;
        public Data.Size Size;

        public double Frame;

        //public Dictionary<String, String> Materials;

        public Slidewindow(Data.Cord loc, Data.Rot rot, Data.Size size, double frame)
        {
            this.Location = loc;
            this.Rotation = rot;
            this.Size = size;
            this.Frame = frame;
        }
    }
}
