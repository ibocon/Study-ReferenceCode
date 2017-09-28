using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Data
{
    /// <summary>
    /// 정적인 정보를 저장하는 클래스로, Archiva namespace 어디서든지 접근이 가능하다.
    /// </summary>
    class Config
    {
        static public double FloorTickness = 30;
        static public double WallTickness = 10;
        static public double PillarTickness = 10; //TODO: 언리얼엔진에 기둥 모델을 추가하자!
        static public Cord WorldBase = new Cord(0, 0, 0);
        static public double MinimalOverlap = 100;
        static public double RoomHeight = 380;
        static public double DoorLength = 73;
        static public double DoorHeight = 230;
        static public double WindowY = 120;
        static public double WindowLength = 200;
        static public double WindowHeight = 165;
        static public double WindowFrame = 10;

        //Temp Standard Width Distance Window
        static public double TempStdWidDstWin = 6;
        //Temp Standard Width Distance Door
        static public double TempStdWidDstDr = (8 / 2);

        public Config(Cord worldCord, double wallThick)
        {
            WallTickness = wallThick;
            WorldBase = worldCord;
        }
    }
}
