using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Data
{
    enum SideCardinalDirection
    {
        West = 0,
        North = 1,
        East = 2,
        South = 3
    }

    enum SideType
    {
        Wall = 0,
        Door = 1,
        Open = 2,
        Window = 3,
        WindowWall = 4
    }

    class Side : ICloneable
    {
        public SideCardinalDirection Position;
        public SideType Type;
        public Cord Start;
        public Cord End;

        public Side(SideCardinalDirection position, SideType type, Cord start, Cord end)
        {
            this.Position = position;
            this.Type = type;

            Cord absP1 = (Cord)start.Clone();
            Cord absP2 = (Cord)end.Clone();

            double x1 = Math.Min(absP1.X, absP2.X);
            double x2 = Math.Max(absP1.X, absP2.X);
            double y1 = Math.Min(absP1.Y, absP2.Y);
            double y2 = Math.Max(absP1.Y, absP2.Y);

            Start = new Cord(x1, y1, absP1.Z);
            End = new Cord(x2, y2, absP2.Z);
        }

        public object Clone()
        {
            return new Side(this.Position, this.Type, (Cord)this.Start.Clone(), (Cord)this.End.Clone());
        }

        public List<Side> OccupyPart(Cord p1, Cord p2, SideType type)
        {
            return OccupyPart(new Side(this.Position, this.Type, p1, p2), type);
        }

        //TODO: 빈공간이 생기거나, 뜬금없는 곳에 문이 생기는 문제가 발생하는 경우가 많음.
        public List<Side> OccupyPart(Side target, SideType type)
        {
            List<Side> parts = new List<Side>();
            if (IsContact(target))
            {
                //접촉된 부분을 지정된 타입으로 Side를 수정한다.
                Side contact = this.GetContact(target);
                contact.Type = type;
                parts.Add(contact);

                //접촉되고 남은 부분을 처리한다.
                if (this.Start.X == contact.Start.X && this.Start.Y == contact.Start.Y && this.End.X == contact.End.X && this.End.Y == contact.End.Y)
                {
                    return parts;
                }
                else if (this.Start.X == contact.Start.X && this.Start.Y == contact.Start.Y)
                {
                    parts.Add(new Side(this.Position, this.Type, contact.End, this.End));
                }
                else if (this.End.X == contact.End.X && this.End.Y == contact.End.Y)
                {
                    parts.Add(new Side(this.Position, this.Type, this.Start, contact.Start));
                }
                else
                {
                    parts.Add(new Side(this.Position, this.Type, this.Start, contact.Start));
                    parts.Add(new Side(this.Position, this.Type, contact.End, this.End));
                }
            }
            //접촉이 없을 경우, 변화없는 상태를 반환한다.
            else
            {
                parts.Add(this);
            }

            return parts;
        }
        public Cord GetMidCord()
        {
            double x = (this.Start.X + this.End.X) / 2.0;
            double y = (this.Start.Y + this.End.Y) / 2.0;
            double z = (this.Start.Z + this.End.Z) / 2.0;

            return new Cord(x, y, z);
        }
        public bool IsContact(Side target)
        {
            if (this.IsParallelToX() && target.IsParallelToX() && this.Start.Y == target.Start.Y)
            {
                double max = Math.Max(this.Start.X, target.Start.X);
                double min = Math.Min(this.End.X, target.End.X);

                if (max < min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (this.IsParallelToY() && target.IsParallelToY() && this.Start.X == target.Start.X)
            {
                double max = Math.Max(this.Start.Y, target.Start.Y);
                double min = Math.Min(this.End.Y, target.End.Y);

                if (max < min)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public Side GetContact(Side target)
        {
            if (this.IsParallelToX() && target.IsParallelToX())
            {
                double max = Math.Max(this.Start.X, target.Start.X);
                double min = Math.Min(this.End.X, target.End.X);

                if (max < min)
                {
                    Cord p1 = new Cord(min, this.Start.Y, this.Start.Z);
                    Cord p2 = new Cord(max, this.End.Y, this.End.Z);
                    return new Side(this.Position, this.Type, p1, p2);
                }
                else
                {
                    return null;
                }
            }
            else if (this.IsParallelToY() && target.IsParallelToY())
            {
                double max = Math.Max(this.Start.Y, target.Start.Y);
                double min = Math.Min(this.End.Y, target.End.Y);

                if (max < min)
                {
                    Cord p1 = new Cord(this.Start.X, min, this.Start.Z);
                    Cord p2 = new Cord(this.End.X, max, this.End.Z);
                    return new Side(this.Position, this.Type, p1, p2);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public double GetContactLength(Side target)
        {
            if (this.IsParallelToX() && target.IsParallelToX())
            {
                double max = Math.Max(this.Start.X, target.Start.X);
                double min = Math.Min(this.End.X, target.End.X);

                if (max < min)
                {
                    return Math.Abs(max - min);
                }
                else
                {
                    return 0;
                }
            }
            else if (this.IsParallelToY() && target.IsParallelToY())
            {
                double max = Math.Max(this.Start.Y, target.Start.Y);
                double min = Math.Min(this.End.Y, target.End.Y);

                if (max < min)
                {
                    return Math.Abs(max - min);
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool IsParallelToX()
        {
            if (Start.X == End.X)
            {
                return false;
            }
            else if (Start.Y == End.Y)
            {
                return true;
            }
            else
            {
                throw new SystemException("Slide is not parallel to Axis!");
            }
        }

        public bool IsParallelToY()
        {
            return !(IsParallelToX());
        }

        public bool IsAvailable(double length)
        {
            //만약 Side가 Wall 타입이 아닐 경우, 이미 점령되어 문이나 창문 또는 공개로 사용되었다.
            if (this.Type != SideType.Wall)
            {
                return false;
            }
            else
            {
                //문과 창문을 달기 위한 최소한의 길이인지 확인
                if (GetLength() >= length)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public double GetLength()
        {
            return Math.Sqrt(Math.Pow(End.X - Start.X, 2) + Math.Pow(End.Y - Start.Y, 2));
        }


    }
}
