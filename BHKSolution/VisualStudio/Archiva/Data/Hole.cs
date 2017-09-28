using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Data
{
    class Hole
    {
        public double X;
        public double Y;
        public double L;
        public double H;

        public Hole(double x, double y, double l, double h)
        {
            this.X = x;
            this.Y = y;
            this.L = l;
            this.H = h;
        }
    }
}
