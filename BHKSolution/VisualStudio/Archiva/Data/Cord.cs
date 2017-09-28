using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Data
{
    /// <summary>
    /// Model의 좌표를 저장하는 데이터 모델.
    /// Model을 손쉽게 배치하기 위해, 상대적인 좌표와 절대적인 좌표를 변환하는 기능이 있다.
    /// </summary>
    class Cord : ICloneable
    {
        //if this is "Cord(0, 0, 0)" or "null", then absolute cord
        public double X;
        public double Y;
        public double Z;

        /// <summary>
        /// Basic Cord Creation
        /// </summary>
        public Cord()
        {
            this.X = 0;
            this.Y = 0;
            this.Z = 0;
        }
        /// <summary>
        /// define Cord to Absolute
        /// </summary>
        public Cord(double absX, double absY, double absZ)
        {
            this.X = absX;
            this.Y = absY;
            this.Z = absZ;
        }
        /// <summary>
        /// define Cord to Relative
        /// </summary>
        public Cord(Cord Base, double relX, double relY, double relZ)
        {
            this.X = Base.X + relX;
            this.Y = Base.Y + relY;
            this.Z = Base.Z + relZ;
        }

        public object Clone()
        {
            return new Cord(X, Y, Z);
        }
    }
}
