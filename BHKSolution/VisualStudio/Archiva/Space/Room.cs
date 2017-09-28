using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Archiva.Space
{
    enum RoomType
    {
        Entrance,   //0
        Hallway,    //1
        Living,     //2
        Kitchen,    //3
        Bed,        //4
        Rest,       //5
        Stair       //6
    }

    class Room : ICloneable, IEquatable<Room>, IComparable<Room>, Model.Modelable
    {
        public Guid id;
        public Data.Cord Location;
        public Data.Size Size;

        //public RoomType Type;
        public Dictionary<Data.SideCardinalDirection, List<Data.Side>> Sides;

        //public List<Model.Wall> Walls;
        //public Dictionary<string, string> WallMat;

        //public Model.Floor Floor;
        //public Dictionary<string, string> FloorMat;

        public List<Model.Modelable> Components;
        public Dictionary<Model.ModelType, Dictionary<string, string>> Materials;

        public Dictionary<StaticCompType, string> StaticComponents;

        public Room()
        {
            id = Guid.NewGuid();
            Location = new Data.Cord();
            //this.Type = RoomType.Bed;
            this.Size = new Data.Size(100, 100, 100);
            this.Sides = new Dictionary<Data.SideCardinalDirection, List<Data.Side>>();
            this.Components = new List<Model.Modelable>();
            this.Materials = new Dictionary<Model.ModelType, Dictionary<string, string>>();
            this.StaticComponents = new Dictionary<StaticCompType, string>();

            foreach (Data.SideCardinalDirection sidePosition in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                Sides[sidePosition] = new List<Data.Side>();
            }

            InitializeSides();
        }

        public Room(Data.Size size, Dictionary<Model.ModelType, Dictionary<string, string>> materials, Dictionary<StaticCompType, string> staticComp)
        {
            id = Guid.NewGuid();
            this.Location = new Data.Cord();
            //this.Type = type;
            this.Size = size;
            this.Components = new List<Model.Modelable>();
            this.Sides = new Dictionary<Data.SideCardinalDirection, List<Data.Side>>();
            this.Materials = materials;
            this.StaticComponents = staticComp;

            foreach (Data.SideCardinalDirection sidePosition in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                Sides[sidePosition] = new List<Data.Side>();
            }

            InitializeSides();
        }

        /*
        public Room(Data.Cord location, Data.Size size, Dictionary<Model.ModelType, Dictionary<string, string>> materials, Dictionary<StaticCompType, string> staticComp)
        {
            id = Guid.NewGuid();
            this.Location = (Data.Cord)location.Clone();
            //this.Type = type;
            this.Size = size;
            this.Components = new List<Model.Modelable>();
            this.Sides = new Dictionary<Data.SideCardinalDirection, List<Data.Side>>();
            this.Materials = materials;
            this.StaticComponents = staticComp;

            foreach (Data.SideCardinalDirection sidePosition in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                Sides[sidePosition] = new List<Data.Side>();
            }

            InitializeSides();
        }
        */
        
        public virtual object Clone()
        {
            Room clone = new Room(Size, Materials, StaticComponents);
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

        //바닥의 좌표를 입력하는 함수. 다만, 언리얼 코드에서 Floor은 반드시 사각형만 가능하고 심지어 방향이 정해져 있다.
        private List<Data.Cord> CalcFloorVerts(List<Data.Cord> vertices)
        {
            List<Data.Cord> verts = new List<Data.Cord>();
            verts.Add(new Data.Cord(0.01, 0.01, 0));
            verts.Add(new Data.Cord(0.01, Size.Width - 0.01, 0));
            verts.Add(new Data.Cord(Size.Length - 0.01, Size.Width - 0.01, 0));
            verts.Add(new Data.Cord(Size.Length - 0.01, 0.01, 0));

            return verts;
        }

        private void InitializeSides()
        {
            //초기화
            foreach (Data.SideCardinalDirection sidePosition in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                Sides[sidePosition].Clear();
            }

            //좌표계산
            Data.Cord p0 = new Data.Cord(this.Location, 0, 0, 0);
            Data.Cord p1 = new Data.Cord(this.Location, 0, Size.Width, 0);
            Data.Cord p2 = new Data.Cord(this.Location, Size.Length, Size.Width, 0);
            Data.Cord p3 = new Data.Cord(this.Location, Size.Length, 0, 0);

            //좌표입력
            this.Sides[Data.SideCardinalDirection.West].Add(new Data.Side(Data.SideCardinalDirection.West, Data.SideType.Wall, p0, p1));
            this.Sides[Data.SideCardinalDirection.North].Add(new Data.Side(Data.SideCardinalDirection.North, Data.SideType.Wall, p1, p2));
            this.Sides[Data.SideCardinalDirection.East].Add(new Data.Side(Data.SideCardinalDirection.East, Data.SideType.Wall, p2, p3));
            this.Sides[Data.SideCardinalDirection.South].Add(new Data.Side(Data.SideCardinalDirection.South, Data.SideType.Wall, p3, p0));

        }

        private void UpdateSides(double changeX, double changeY, double changeZ)
        {
            foreach (Data.SideCardinalDirection sidePos in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                foreach (Data.Side side in this.Sides[sidePos])
                {
                    side.Start.X = side.Start.X + changeX;
                    side.Start.Y = side.Start.Y + changeY;
                    side.Start.Z = side.Start.Z + changeZ;

                    side.End.X = side.End.X + changeX;
                    side.End.Y = side.End.Y + changeY;
                    side.End.Z = side.End.Z + changeZ;
                }
            }
        }

        //배치를 바꿔다시 계산하는 의외로 중요한 함수
        public void ChangeLocation(Data.Cord newLocation)
        {
            double x = newLocation.X - Location.X;
            double y = newLocation.Y - Location.Y;
            double z = newLocation.Z - Location.Z;
            this.Location = (Data.Cord)newLocation.Clone();

            UpdateSides(x, y, z);

        }

        //아직 사용되지 못하고 있는 함수다. 모델의 가능성을 더욱 확장해서 추가할 수 있는 함수.
        public void Rotate()
        {
            ChangeSize(Size.Width, Size.Length, Size.Height);    
        }

        //기존의 Side 정보가 초기화됨
        public void ChangeSize(double roomLength, double roomWidth, double roohHeight)
        {
            this.Size.Length = roomLength;
            this.Size.Width = roomWidth;
            this.Size.Height = roohHeight;

            InitializeSides();
        }

        public void OccupySide(Data.SideCardinalDirection part, double start, double end)
        {
            //특정한 Side를 Occupy할 때,
            /*
             * 1) 새로운 Side 상태의 기록을 남긴다.
             * 2) 이전 Side의 상태도 기록한다.
             * 
             * 일단, 변화를 순차적으로 추적하기 위해, 
             * Stack 자료구조를 활용해서 이전 자료를 저장하는 과정이 필요하다.
             * Stack에는 List<Side>와 part를 같이 저장해야 복구할 Side가 무엇인지 알 수 있다.
             */

            List<Data.Side> target = this.Sides[part];

        }

        public static bool IsOverlap(Room a, Room b)
        {
            double x1 = Math.Max(a.Location.X, b.Location.X);
            double x2 = Math.Min(a.Location.X + a.Size.Length, b.Location.X + b.Size.Length);
            double y1 = Math.Max(a.Location.Y, b.Location.Y);
            double y2 = Math.Min(a.Location.Y + a.Size.Width, b.Location.Y + b.Size.Width);

            if (x2 > x1 && y2 > y1)
            {
                return true;
            }

            return false;
        }

        public static bool IsContact(Room r1, Room r2, double minOverlapLen)
        {

            foreach (Data.SideCardinalDirection i in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                Data.SideCardinalDirection j = Data.SideCardinalDirection.West;
                switch (i)
                {
                    case (Data.SideCardinalDirection.West):
                        j = Data.SideCardinalDirection.East;
                        break;
                    case (Data.SideCardinalDirection.North):
                        j = Data.SideCardinalDirection.South;
                        break;
                    case (Data.SideCardinalDirection.East):
                        j = Data.SideCardinalDirection.West;
                        break;
                    case (Data.SideCardinalDirection.South):
                        j = Data.SideCardinalDirection.North;
                        break;
                }

                foreach (Data.Side a in r1.Sides[i])
                {
                    foreach (Data.Side b in r2.Sides[j])
                    {
                        if (a.IsContact(b))
                        {
                            Data.Side contacted = a.GetContact(b);
                            if(contacted.GetLength() >= minOverlapLen)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public static void OccupySides(Room r1, Room r2, ConnectionType type)
        {
            foreach (Data.SideCardinalDirection i in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {

                Data.SideCardinalDirection j = Data.SideCardinalDirection.West;
                switch (i)
                {
                    case (Data.SideCardinalDirection.West):
                        j = Data.SideCardinalDirection.East;
                        break;
                    case (Data.SideCardinalDirection.North):
                        j = Data.SideCardinalDirection.South;
                        break;
                    case (Data.SideCardinalDirection.East):
                        j = Data.SideCardinalDirection.West;
                        break;
                    case (Data.SideCardinalDirection.South):
                        j = Data.SideCardinalDirection.North;
                        break;
                }

                Data.Side[] r1Sides = r1.Sides[i].ToArray();
                Data.Side[] r2Sides = r2.Sides[j].ToArray();

                foreach (Data.Side a in r1Sides)
                {
                    foreach (Data.Side b in r2Sides)
                    {
                       
                        if (a.IsContact(b))
                        {
                            
                            r1.Sides[i].Remove(a);
                            r2.Sides[j].Remove(b);

                            //접촉하는 부분을 추출
                            Data.Side contacted = a.GetContact(b);

                            //두 공간의 관계에 따라, 점령될 SideType을 결정
                            Data.SideType sType = Data.SideType.Wall;
                            if (type == ConnectionType.Open)
                            {
                                sType = Data.SideType.Open;
                            }
                            else if (type == ConnectionType.Door)
                            {
                                sType = Data.SideType.Door;
                            }

                            //Side 점령
                            foreach (Data.Side newA in a.OccupyPart(contacted, sType))
                            {
                                r1.Sides[i].Add(newA);
                            }
                            //r1.Sides[(int)i] = a.OccupyPart(contacted, sType);
                            foreach (Data.Side newB in b.OccupyPart(contacted, sType))
                            {
                                r2.Sides[j].Add(newB);
                            }
                            //r2.Sides[(int)j] = b.OccupyPart(contacted, sType);
                        }
                        
                    }
                }
            }
        }

        //방에 추가적인 물체를 둘 때는 이 함수를 활용할 것! (가구같은 것들)
        public virtual void CreateComponents(bool isFloor, bool isRoof, int cameraNumber, bool isStair, RelationLevel level)
        {
            this.Components.Clear();
            if (isFloor)
            {
                this.CreateFloor();
            }

            this.CreateWalls();

            if (isRoof)
            {
                this.CreateRoof();
                this.CreateLight();
            }


            this.CreateCamera(cameraNumber);

            if (isStair)
            {
                this.CreateFullstair(level);
            }

            this.CreateFurniture();
        }

        protected virtual void CreateFurniture()
        {
            //Room은 Generic한 클래스로 기본적으로 배치되야 할 가구는 없으므로, 방이 구체적으로 정의된 곳에서 추가하길 바란다.
            //또한, 가구를 자동적으로 생성해서 원하는 퀼리티는 뽑기 어려우므로, Model은 Static을 활용하길 추천한다.
        }

        //TODO: Side를 체크해서 연결된 부분의 방향에 따라, 계단의 배치가 변경되도록 코딩할 필요가 있다.
        protected virtual void CreateFullstair(RelationLevel level)
        {
            //계단 생성을 위한 변수
            Data.Cord location = new Data.Cord();
            Data.Rot rotation = new Data.Rot();
            
            int number = 7;
            double width = this.Size.Length / 2 - Data.Config.WallTickness;
            double length = (this.Size.Width - (width * 2)) / number;
            double height = (Data.Config.RoomHeight / 2) / 8;
            double foundation = 0;

            //처음 진입하는 계단
            rotation = new Data.Rot(0, 0, 270);
            location = new Data.Cord(
                this.Location.X + Data.Config.WallTickness,
                this.Location.Y + this.Size.Width - width - 10, // -10은 Stair 모델에서 튀어나온 부분 때문에 겹치지 않도록 하느라...
                this.Location.Z + Data.Config.FloorTickness);
            this.Components.Add(new Model.Fullstair(location, rotation, number, length, width, height, foundation, Materials[Model.ModelType.Fullstair]));

            //2층 계단으로 진입하기 전, 평지

            //오른쪽
            foundation = (Data.Config.RoomHeight / 2) - height;
            location = new Data.Cord(
                this.Location.X + Data.Config.WallTickness,
                this.Location.Y + this.Size.Width - Data.Config.WallTickness,
                this.Location.Z + Data.Config.FloorTickness + foundation);
            this.Components.Add(new Model.Fullstair(location, rotation, 1, width, width, height, foundation, Materials[Model.ModelType.Fullstair]));
            //왼쪽
            rotation = new Data.Rot(0, 0, 90);
            location = new Data.Cord(this.Location.X - Data.Config.WallTickness + this.Size.Length,
                this.Location.Y + this.Size.Width - Data.Config.WallTickness - width,
                this.Location.Z + Data.Config.FloorTickness + foundation);
            this.Components.Add(new Model.Fullstair(location, rotation, 1, width, width, height, foundation, Materials[Model.ModelType.Fullstair]));


            //2층 진입 계단
            foundation = (Data.Config.RoomHeight / 2);
            location = new Data.Cord(
                this.Location.X + this.Size.Length - Data.Config.WallTickness,
                this.Location.Y + width - 10,
                this.Location.Z + Data.Config.FloorTickness + (Data.Config.RoomHeight / 2));
            this.Components.Add(new Model.Fullstair(location, rotation, number, length, width, height, foundation, Materials[Model.ModelType.Fullstair]));

            //2층 계단과 연결되는 평지
            foundation = 0;
            rotation = new Data.Rot(0, 0, 90);
            location = new Data.Cord(this.Location.X + this.Size.Length,
                this.Location.Y + Data.Config.WallTickness,
                this.Location.Z + Data.Config.FloorTickness + Data.Config.RoomHeight - Data.Config.FloorTickness);
            this.Components.Add(new Model.Fullstair(location, rotation, 1, width, width * 2, height, foundation, Materials[Model.ModelType.Fullstair]));

        }

        //카메라가 바라보는 방향을 방의 타입에 따라, 현관은 입구를, 거실은 창을, 방은 창문을 바라본다.
        protected virtual void CreateCamera(int cameraNumber)
        {
            double rotation = 0;
            Data.Cord a = new Data.Cord(Location.X + (0.5 * Size.Length), Location.Y + (0.5 * Size.Width), Location.Z + 170);

            Data.SideType current = Data.SideType.Wall;
            foreach (Data.SideCardinalDirection sideDirection in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                foreach (Data.Side side in this.Sides[sideDirection])
                {
                    if ( current < side.Type )
                    {
                        Data.Cord b = side.GetMidCord();

                        double y = b.Y - a.Y;
                        double x = b.X - a.X;

                        if(x == 0)
                        {
                            if ( y > 0)
                            {
                                rotation = 90;
                            }
                            else
                            {
                                rotation = 270;
                            }
                        }
                        else
                        {
                            rotation = (Math.Acos(y / x) * (180 / Math.PI));

                            if (rotation < 0)
                            {
                                rotation += 360;
                            }
                        }
                        

                        current = side.Type;
                    }
                }
            }

            
            this.Components.Add(new Model.Camera(new Data.Cord(Location.X + (0.5 * Size.Length), Location.Y + (0.5 * Size.Width), Location.Z + 170), new Data.Rot(0, 0, rotation), cameraNumber));
        }

        protected virtual void CreateLight()
        {
            int red = 255;
            int green = 255;
            int blue = 255;

            double lightIntensity = (this.Size.Length * this.Size.Width) / 16; 

            Model.Pointlight pointLight = new Model.Pointlight(new Data.Cord(Location.X + (0.5 * Size.Length), Location.Y + (0.5 * Size.Width), Location.Z + Size.Height), lightIntensity, red, green, blue);
            this.Components.Add(pointLight);
        }

        //TODO: 당장은 평지붕이 올라가도록 디자인했다.
        private void CreateRoof()
        {
            Data.Cord location = this.Location.Clone() as Data.Cord;
            this.Components.Add(new Model.Flatroof(new Data.Cord(location.X - 0.01, location.Y - 0.01, location.Z + Data.Config.RoomHeight), new Data.Rot(), Data.Config.FloorTickness - 0.01, Size.Width + 0.02, Size.Length + 0.02, Materials[Model.ModelType.Flatroof]));
        }

        private void CreateFloor()
        {
            this.Components.Add(new Model.Floor(this.Location, Data.Config.FloorTickness, CalcFloorVerts(null), Materials[Model.ModelType.Floor]));
        }
        //TODO: 현재 삽입되는 부가적인 모델(창문이나 문 또는 가구)의 피벗(기준점) 위치가 정해지지 않아, 임시적으로 현재 언리얼에 있는 모델을 기준으로 계산되어 있다.
        private void CreateWalls()
        {
            foreach (Data.SideCardinalDirection sidePos in Enum.GetValues(typeof(Data.SideCardinalDirection)))
            {
                foreach (Data.Side side in Sides[sidePos])
                {
                    List<Data.Hole> holes = new List<Data.Hole>();

                    if(side.Type == Data.SideType.Wall)
                    {
                        //그냥 벽일 경우, 특정한 작업을 딱히 할 필요는 없다.
                    }
                    else if (side.Type == Data.SideType.Door)
                    {
                        double doorPos = 0;
                        if(side.Position == Data.SideCardinalDirection.East)
                        {
                            doorPos = (side.End.Y - side.Start.Y) / 2.0;
                            Model.Static door = new Model.Static(
                                    new Data.Cord(side.Start.X - Data.Config.TempStdWidDstDr, side.Start.Y + doorPos + Data.Config.DoorLength / 2, side.Start.Z + Data.Config.FloorTickness),
                                    new Data.Rot(0, 0, 90),
                                    this.StaticComponents[StaticCompType.Door]
                                );
                            this.Components.Add(door);
                        }
                        else if (side.Position == Data.SideCardinalDirection.West)
                        {
                            doorPos = (side.End.Y - side.Start.Y) / 2.0;
                            Model.Static door = new Model.Static(
                                    new Data.Cord(side.Start.X + Data.Config.TempStdWidDstDr, side.Start.Y + doorPos - Data.Config.DoorLength / 2, side.Start.Z + Data.Config.FloorTickness),
                                    new Data.Rot(0, 0, 270),
                                    this.StaticComponents[StaticCompType.Door]
                                );
                            this.Components.Add(door);
                        }
                        else if (side.Position == Data.SideCardinalDirection.North)
                        {
                            doorPos = (side.End.X - side.Start.X) / 2.0;
                            Model.Static door = new Model.Static(
                                new Data.Cord(side.Start.X + doorPos - Data.Config.DoorLength / 2, side.Start.Y + Data.Config.TempStdWidDstDr, side.Start.Z + Data.Config.FloorTickness),
                                new Data.Rot(0, 0, 180),
                                this.StaticComponents[StaticCompType.Door]
                            );
                            this.Components.Add(door);
                        }
                        else if (side.Position == Data.SideCardinalDirection.South)
                        {
                            doorPos = (side.End.X - side.Start.X) / 2.0;
                            Model.Static door = new Model.Static(
                                new Data.Cord(side.Start.X + doorPos + Data.Config.DoorLength / 2, side.Start.Y + Data.Config.TempStdWidDstDr, side.Start.Z + Data.Config.FloorTickness),
                                new Data.Rot(0, 0, 0),
                                this.StaticComponents[StaticCompType.Door]
                            );
                            this.Components.Add(door);
                        }
                        Data.Hole doorHole = new Data.Hole(doorPos, Data.Config.FloorTickness + 1, Data.Config.DoorLength, Data.Config.DoorHeight);
                        holes.Add(doorHole);
                    }
                    else if (side.Type == Data.SideType.Open)
                    {
                        //DO NOT CREATE Wall
                    }
                    else if (side.Type == Data.SideType.Window)
                    {
                        double winX = 0;
                        double winY = Data.Config.WindowY;
                        if (side.Position == Data.SideCardinalDirection.East)
                        {
                            winX = (side.End.Y - side.Start.Y) / 2.0;
                            Model.Static window = new Model.Static(
                                new Data.Cord(side.Start.X - Data.Config.TempStdWidDstWin, side.Start.Y + winX, side.Start.Z + winY + Data.Config.WindowHeight / 2),
                                new Data.Rot(0, 0, 90),
                                this.StaticComponents[StaticCompType.Window]
                            );
                            this.Components.Add(window);
                        }
                        else if (side.Position == Data.SideCardinalDirection.West)
                        {
                            winX = (side.End.Y - side.Start.Y) / 2.0;
                            Model.Static window = new Model.Static(
                                new Data.Cord(side.Start.X + Data.Config.TempStdWidDstWin, side.Start.Y + winX, side.Start.Z + winY + Data.Config.WindowHeight / 2),
                                new Data.Rot(0, 0, 90),
                                this.StaticComponents[StaticCompType.Window]
                            );
                            this.Components.Add(window);
                        }
                        else if (side.Position == Data.SideCardinalDirection.North)
                        {
                            winX = (side.End.X - side.Start.X) / 2.0;
                            Model.Static window = new Model.Static(
                                new Data.Cord(side.Start.X + winX, side.Start.Y - Data.Config.TempStdWidDstWin, side.Start.Z + winY + Data.Config.WindowHeight / 2),
                                new Data.Rot(0, 0, 0),
                                this.StaticComponents[StaticCompType.Window]
                            );
                            this.Components.Add(window);
                        }
                        else if (side.Position == Data.SideCardinalDirection.South)
                        {
                            winX = (side.End.X - side.Start.X) / 2.0;
                            Model.Static window = new Model.Static(
                                new Data.Cord(side.Start.X + winX, side.Start.Y + Data.Config.TempStdWidDstWin, side.Start.Z + winY + Data.Config.WindowHeight / 2),
                                new Data.Rot(0, 0, 0),
                                this.StaticComponents[StaticCompType.Window]
                            );
                            this.Components.Add(window);
                        }
                        Data.Hole windowHole = new Data.Hole(winX, winY, Data.Config.WindowLength, Data.Config.WindowHeight);
                        holes.Add(windowHole);

                    }
                    else if (side.Type == Data.SideType.WindowWall)
                    {

                        double tempL = 270;
                        double tempH = 250;

                        double winX = 0;
                        double winY = Data.Config.FloorTickness;
                        if (side.Position == Data.SideCardinalDirection.East || side.Position == Data.SideCardinalDirection.West)
                        {
                            winX = (side.End.Y - side.Start.Y) / 2.0;
                            Model.Static window1 = new Model.Static(
                                new Data.Cord(side.Start.X, side.Start.Y + winX - tempL / 2, side.Start.Z + winY),
                                new Data.Rot(0, 0, 90),
                                this.StaticComponents[StaticCompType.SlideWindow]
                            );
                            Model.Static window2 = new Model.Static(
                                new Data.Cord(side.Start.X, side.Start.Y + winX + tempL / 2, side.Start.Z + winY),
                                new Data.Rot(0, 0, 90),
                                this.StaticComponents[StaticCompType.SlideWindow]
                            );
                            this.Components.Add(window1);
                            this.Components.Add(window2);
                        }
                        else if (side.Position == Data.SideCardinalDirection.North || side.Position == Data.SideCardinalDirection.South)
                        {
                            winX = (side.End.X - side.Start.X) / 2.0;
                            Model.Static window1 = new Model.Static(
                                new Data.Cord(side.Start.X + winX - tempL / 2, side.Start.Y, side.Start.Z + winY),
                                new Data.Rot(0, 0, 0),
                                this.StaticComponents[StaticCompType.SlideWindow]
                            );
                            Model.Static window2 = new Model.Static(
                                new Data.Cord(side.Start.X + winX + tempL / 2, side.Start.Y, side.Start.Z + winY),
                                new Data.Rot(0, 0, 0),
                                this.StaticComponents[StaticCompType.SlideWindow]
                            );
                            this.Components.Add(window1);
                            this.Components.Add(window2);

                        }

                        Data.Hole windowwallHole = new Data.Hole(winX, winY, tempL * 2, tempH); //TODO: 하드코딩되어 있는 미닫이창 크기
                        holes.Add(windowwallHole);

                    }
                

                    /* 다이나믹하게 생성가능한 슬라이드 윈도우를 활용하는 방법
                    else if (side.Type == Data.SideType.WindowWall)
                    {
                        double winX = 0;
                        double winY = Data.Config.FloorTickness + Data.Config.WallTickness;
                        if (side.Position == Data.SideCardinalDirection.East || side.Position == Data.SideCardinalDirection.West)
                        {
                            winX = (side.End.Y - side.Start.Y) / 2.0;

                            Model.Fixedwindow win = new Model.Fixedwindow(
                                new Data.Cord(side.Start.X - Data.Config.WallTickness / 2, side.Start.Y + winX, side.Start.Z + winY), 
                                new Data.Rot(0, 0, 270), 
                                new Data.Size(((winX - (Data.Config.WallTickness * 2)) * 2), Data.Config.WallTickness + 5, Data.Config.RoomHeight - (Data.Config.WallTickness * 2) - Data.Config.FloorTickness), 
                                Data.Config.WindowFrame,
                                Materials[Model.ModelType.Fixedwindow]
                            );
                            this.Components.Add(win);
                        } 
                        else if (side.Position == Data.SideCardinalDirection.North || side.Position == Data.SideCardinalDirection.South)
                        {
                            winX = (side.End.X - side.Start.X) / 2.0;

                            Model.Fixedwindow win = new Model.Fixedwindow(
                                new Data.Cord(side.Start.X + winX, side.Start.Y + Data.Config.WallTickness / 2, side.Start.Z + winY),
                                new Data.Rot(0, 0, 0),
                                new Data.Size(((winX - (Data.Config.WallTickness * 2)) * 2), Data.Config.WallTickness + 5, Data.Config.RoomHeight - (Data.Config.WallTickness * 2) - Data.Config.FloorTickness),
                                Data.Config.WindowFrame,
                                Materials[Model.ModelType.Fixedwindow]
                            );
                            this.Components.Add(win);
                        }

                        Data.Hole windowwallHole = new Data.Hole(winX, winY, ((winX - (Data.Config.WallTickness * 2)) * 2), Data.Config.RoomHeight - (Data.Config.WallTickness * 2) - Data.Config.FloorTickness);
                        holes.Add(windowwallHole);

                    }
                    */

                    if (side.Type != Data.SideType.Open)
                    {
                        //미세하게 어긋나는 계산 조정 및 Materal이 정확하게 반연되기 위한 논리
                        Data.Cord start = new Data.Cord(side.Start.X, side.Start.Y, side.Start.Z);
                        Data.Cord end = new Data.Cord(side.End.X, side.End.Y, side.End.Z);

                        //start.Z = start.Z + Data.Config.FloorTickness;
                        //end.Z = end.Z + Data.Config.FloorTickness;

                        if (side.Position == Data.SideCardinalDirection.East)
                        {
                            start.X = start.X - Data.Config.WallTickness / 2;
                            end.X = end.X - Data.Config.WallTickness / 2;

                            this.Components.Add(CreateWall(end, start, holes, Materials[Model.ModelType.Wall]));
                        }
                        else if (side.Position == Data.SideCardinalDirection.North)
                        {
                            start.Y = start.Y - Data.Config.WallTickness / 2;
                            end.Y = end.Y - Data.Config.WallTickness / 2;

                            this.Components.Add(CreateWall(start, end, holes, Materials[Model.ModelType.Wall]));
                        }
                        else if (side.Position == Data.SideCardinalDirection.West)
                        {
                            start.X = start.X + Data.Config.WallTickness / 2;
                            end.X = end.X + Data.Config.WallTickness / 2;

                            this.Components.Add(CreateWall(start, end,  holes, Materials[Model.ModelType.Wall]));
                        }
                        else if (side.Position == Data.SideCardinalDirection.South)
                        {
                            start.Y = start.Y + Data.Config.WallTickness / 2;
                            end.Y = end.Y + Data.Config.WallTickness / 2;

                            this.Components.Add(CreateWall(end, start,  holes, Materials[Model.ModelType.Wall]));
                        }
                    }

                }
            }
        }

        private Model.Wall CreateWall(Data.Cord startPoint, Data.Cord endPoint, List<Data.Hole> holes, Dictionary<string, string> wallMat)
        {
            Model.Wall wall = new Model.Wall(startPoint, endPoint, Data.Config.WallTickness, Size.Height, holes, wallMat);
            return wall;
        }

        public bool Equals(Room other)
        {
            if(this.id == other.id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Room other)
        {
            if (this.Size.getSpace() > other.Size.getSpace())
            {
                return -1;
            }
            else if (this.Size.getSpace() < other.Size.getSpace())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void WriteModel(MyXmlWriter xml)
        {
            xml.Writer.WriteStartElement("Room");

            foreach (Model.Modelable model in Components)
            {
                model.WriteModel(xml);
            }

            xml.Writer.WriteEndElement();
        }
    }
}
