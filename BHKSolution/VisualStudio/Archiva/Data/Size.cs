using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Data
{
    class Size : IComparable<Size>
    {
        public double Length;
        public double Width;
        public double Height;

        public Size(double length, double width, double height)
        {
            Length = length;
            Width = width;
            Height = height;
        }

        public double getSurface()
        {
            return Length * Width;
        }

        public double getSpace()
        {
            return Length * Width * Height;
        }

        public int CompareTo(Size other)
        {
            if (this.getSpace() > other.getSpace())
            {
                return -1;
            }
            else if (this.getSpace() < other.getSpace())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
