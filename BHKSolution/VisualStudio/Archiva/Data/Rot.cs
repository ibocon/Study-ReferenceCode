using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Data
{
    class Rot : ICloneable
    {
        public double Roll;
        public double Pitch;
        public double Yaw;

        public Rot()
        {
            this.Roll = 0;
            this.Pitch = 0;
            this.Yaw = 0;
        }

        public Rot(double r, double p, double y)
        {
            this.Roll = r;
            this.Pitch = p;
            this.Yaw = y;
        }

        public object Clone()
        {
            return new Rot(Roll, Pitch, Yaw);
        }
    }
}
