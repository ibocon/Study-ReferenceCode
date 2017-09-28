using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva
{
    //TODO: 하드코딩되어 있는 정보를 외부 파일을 활용하여 관리할 수 있도록 개선할 필요가 있다.
    class Variables
    {
        //사용할 수 있는 최대한의 면적
        public double MaxSpace;
        //모든 배치의 기준이 될 좌표
        public Data.Cord WorldStdLocation;

        public int how_many_bed;
        public int how_many_rest;
        public bool is_simple;

        //방 크기 변수 목록
        public RoomSize RoomSize;
        public RelationVar RelationShipVariables;

        //Material 정보를 저장할 변수
        //TODO: 하드코딩된 부분이 많아, 효율적으로 처리할 수 있도록 개선할 필요가 많다!
        public Dictionary<Space.RoomType, Dictionary<string, string>> FloorMaterials;
        public Dictionary<Space.RoomType, Dictionary<string, string>> WallMaterials;
        public Dictionary<Space.RoomType, Dictionary<string, string>> FixedwindowMaterials;
        public Dictionary<Space.RoomType, Dictionary<string, string>> FlatroofMaterials;
        public Dictionary<Space.RoomType, Dictionary<string, string>> FullstairMaterials;

        public Dictionary<StaticCompType, string> StaticComponents;

        public string RoofType;

        public Variables(double maxspace, Data.Cord stdloc, int beds, int rests, bool simple, string design, string roof)
        {
            MaxSpace = maxspace;
            WorldStdLocation = stdloc;
            how_many_bed = beds;
            how_many_rest = rests;
            is_simple = simple;

            RoomSize = new RoomSize(MaxSpace, how_many_bed, how_many_rest);
            RelationShipVariables = new RelationVar();

            FloorMaterials = new Dictionary<Space.RoomType, Dictionary<string, string>>();
            WallMaterials = new Dictionary<Space.RoomType, Dictionary<string, string>>();
            FixedwindowMaterials = new Dictionary<Space.RoomType, Dictionary<string, string>>();
            FlatroofMaterials = new Dictionary<Space.RoomType, Dictionary<string, string>>();
            FullstairMaterials = new Dictionary<Space.RoomType, Dictionary<string, string>>();

            StaticComponents = new Dictionary<StaticCompType, string>();

            //디자인 템플렛에 맞추어, Material을 결정
            DecideMaterialSet(design);

            //디자인 템플렛에 맞추어, Door이나 Window 타입을 결정
            DecideStaticSet(design);

            RoofType = roof;
        }

        //소파를 기준으로 거실크기 계산
        public void CalcLivingSize(double sofaLen, double margin)
        {
            double livingLength = RoomSize.LivingSize.Length;

            if (RoomSize.LivingSize.Length < (sofaLen + margin))
            {
                livingLength = sofaLen + margin;
            }

            RoomSize.LivingSize = new Data.Size(livingLength, RoomSize.LivingSize.Width, RoomSize.LivingSize.Height);
        }

        //주방에 들어갈 주방시스템을 기준으로 주방크기 계산
        public void CalcKitchenSize(double sysLength, double sysWidth, double lenMargin, double widMargin)
        {
            double kitchenLength = RoomSize.KitchenSize.Length;
            double kitchenWidth = RoomSize.KitchenSize.Width;

            if (RoomSize.KitchenSize.Length < (sysLength + lenMargin))
            {
                kitchenLength = sysLength + lenMargin;
            }

            if (RoomSize.KitchenSize.Width < (sysWidth + widMargin))
            {
                kitchenWidth = sysWidth + widMargin;
            }

            RoomSize.KitchenSize = new Data.Size(kitchenLength, kitchenWidth, RoomSize.KitchenSize.Height);
        }

        //Unreal Engine에서 Material Path를 찾아서, 입력하자.
        //Material을 결정하는 변수를 추가하려면 여기!
        //TODO: 각 디자인 타입마다, 엑셀 시트가 있고 거기서 정보를 가져오는 기능을 구현할 필요가 있다.
        public void DecideMaterialSet(string design)
        {
            #region Design_Basic
            if (design.Equals("Basic"))
            {
                //TODO: 디자인 추가할 때, 참고하라고 하드코딩 함. 하지만, 엑셀에서 자료를 가져오는 기능을 짜는게 좋을 것임.

                //현관 디자인
                this.WallMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "top", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "bottom", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "side", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Black_Inst.M_Black_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };

                //거실 디자인
                this.WallMaterials[Space.RoomType.Living] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "top", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "bottom", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "side", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Black_Inst.M_Black_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };

                //주방 디자인
                this.WallMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "top", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "bottom", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "side", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Black_Inst.M_Black_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };

                //침실 디자인
                this.WallMaterials[Space.RoomType.Bed] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "top", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "bottom", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "side", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Black_Inst.M_Black_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };

                //화장실 디자인
                this.WallMaterials[Space.RoomType.Rest] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "top", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "bottom", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "side", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Black_Inst.M_Black_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };

                //계단 디자인
                this.WallMaterials[Space.RoomType.Stair] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "top", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "bottom", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" },
                    { "side", "Material'/Game/Organized/Environment/Materials/M_Basic_Floor.M_Basic_Floor'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Black_Inst.M_Black_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
            }
            #endregion
            #region Design_Wood
            else if (design.Equals("Wood"))
            {
                //현관 디자인
                this.WallMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_06.Wall_06'" },
                    { "out", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Wall_1_Inst.M_Wall_1_Inst'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "top", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_05.Floor_05'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_05.Floor_05'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                
                this.FixedwindowMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Door_Wood_Inst.M_Door_Wood_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                
                this.FlatroofMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Marble_Tiled_02_Inst.M_Marble_Tiled_02_Inst'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                
                this.FullstairMaterials[Space.RoomType.Entrance] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                

                //거실 디자인
                this.WallMaterials[Space.RoomType.Living] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FABRIC/Fabric_02.Fabric_02'" },
                    { "out", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Wall_1_Inst.M_Wall_1_Inst'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "top", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_01.Floor_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WOOD/Wood_01.Wood_01'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Marble_Tiled_02_Inst.M_Marble_Tiled_02_Inst'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                
                this.FullstairMaterials[Space.RoomType.Living] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                

                //주방 디자인
                this.WallMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FABRIC/Fabric_02.Fabric_02'" },
                    { "out", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Wall_1_Inst.M_Wall_1_Inst'" },
                    { "side", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Door_Wood3_Inst.M_Door_Wood3_Inst'" }
                };
                this.FloorMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "top", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_01.Floor_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_03.Wall_03'" }
                };
                this.FixedwindowMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Door_Wood_Inst.M_Door_Wood_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                this.FlatroofMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Marble_Tiled_02_Inst.M_Marble_Tiled_02_Inst'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                
                this.FullstairMaterials[Space.RoomType.Kitchen] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                

                //침실 디자인
                this.WallMaterials[Space.RoomType.Bed] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FABRIC/Fabric_02.Fabric_02'" },
                    { "out", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Wall_1_Inst.M_Wall_1_Inst'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "top", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_01.Floor_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_03.Wall_03'" }
                };
                
                this.FixedwindowMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Door_Wood_Inst.M_Door_Wood_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                
                this.FlatroofMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Marble_Tiled_02_Inst.M_Marble_Tiled_02_Inst'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                
                this.FullstairMaterials[Space.RoomType.Bed] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                

                //화장실 디자인
                this.WallMaterials[Space.RoomType.Rest] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/CERAMIC/Ceramic_04.Ceramic_04'" },
                    { "out", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Wall_1_Inst.M_Wall_1_Inst'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FloorMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "top", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/CERAMIC/Ceramic_01.Ceramic_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                
                this.FixedwindowMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Door_Wood_Inst.M_Door_Wood_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                
                this.FlatroofMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Marble_Tiled_02_Inst.M_Marble_Tiled_02_Inst'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/MARBLE/Marble_03.Marble_03'" }
                };
                
                this.FullstairMaterials[Space.RoomType.Rest] = new Dictionary<string, string>()
                {
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "step", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                

                //계단 디자인
                this.WallMaterials[Space.RoomType.Stair] = new Dictionary<string, string>() {
                    { "in", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FABRIC/Fabric_02.Fabric_02'" },
                    { "out", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_05.Floor_05'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_03.Wall_03'" }
                };
                this.FloorMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "top", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/FLOOR/Floor_01.Floor_01'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" },
                    { "side", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_03.Wall_03'" }
                };
                
                this.FixedwindowMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "frame", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Door_Wood_Inst.M_Door_Wood_Inst'" },
                    { "glass", "Material'/Game/Organized/Environment/Materials/Glass/M_Glass.M_Glass'" }
                };
                
                this.FlatroofMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "roof", "MaterialInstanceConstant'/Game/Organized/Environment/Materials/M_Marble_Tiled_02_Inst.M_Marble_Tiled_02_Inst'" },
                    { "bottom", "MaterialInstanceConstant'/Game/DT_Mat_Vol1/Materials/WALL/Wall_01.Wall_01'" }
                };
                this.FullstairMaterials[Space.RoomType.Stair] = new Dictionary<string, string>()
                {
                    { "side", "Material'/Game/ImpromptuStairs/Materials/M_Wood_Floor_Walnut_Polished_WorldAligned.M_Wood_Floor_Walnut_Polished_WorldAligned'" },
                    { "step", "Material'/Game/ImpromptuStairs/Materials/M_WoodStep_001.M_WoodStep_001'" }
                };
            }
            #endregion
        }
        //이번엔 Static Mesh 타입을 결정하는 함수다!
        private void DecideStaticSet(string design)
        {
            #region Design_Basic
            if (design.Equals("Basic"))
            {
                this.StaticComponents[StaticCompType.Door] = "StaticMesh'/Game/Organized/Environment/StaticMeshes/Door/SM_Door_Door6.SM_Door_Door6'";
                this.StaticComponents[StaticCompType.Window] = "StaticMesh'/Game/Organized/Environment/StaticMeshes/Window/SM_2000_1650_Window3.SM_2000_1650_Window3'";
                this.StaticComponents[StaticCompType.SlideWindow] = "StaticMesh'/Game/Organized/Environment/StaticMeshes/Furniture/SM_SlidingDoors.SM_SlidingDoors'";
            }
            #endregion
            #region Design_Wood
            else if (design.Equals("Wood"))
            {
                this.StaticComponents[StaticCompType.Door] = "StaticMesh'/Game/Organized/Environment/StaticMeshes/Door/SM_Door_Door2.SM_Door_Door2'";
                this.StaticComponents[StaticCompType.Window] = "StaticMesh'/Game/Organized/Environment/StaticMeshes/Window/SM_2000_1650_Window4.SM_2000_1650_Window4'";
                this.StaticComponents[StaticCompType.SlideWindow] = "StaticMesh'/Game/Organized/Environment/StaticMeshes/Furniture/SM_SlidingDoors.SM_SlidingDoors'";
            }
            #endregion
        }
    }

    enum StaticCompType
    {
        Door, Window, SlideWindow
    }

    //각 공간 크기에 관계된 변수 목록
    class RoomSize
    {
        static Random rnd = new Random();

        public Data.Size EnterenceSize;
        public Data.Size LivingSize;
        public Data.Size KitchenSize;
        public List<Data.Size> BedSizes;
        public List<Data.Size> RestSizes;

        static private Tuple<int, int>[] Ratio = {
            new Tuple<int, int>(8, 7),
            //new Tuple<int, int>(11, 12),
            //new Tuple<int, int>(1, 1),
            //new Tuple<int, int>(3, 2),
            //new Tuple<int, int>(9, 8)
        };
        static private Tuple<double, double>[] Roomspace = {
            //new Tuple<double, double>(330, 350),
            //new Tuple<double, double>(340, 450),
            new Tuple<double, double>(480, 500)
            //new Tuple<double, double>(300, 320)
        };

        public RoomSize(double maxSpace, int bedNum, int resNum)
        {
            //현관
            EnterenceSize = new Data.Size(250, 240, Data.Config.RoomHeight);

            //거실
            double ratioLiving = 0.3;

            int select = rnd.Next(Ratio.Length);

            double multiple = Math.Sqrt((maxSpace * ratioLiving) / (Ratio[select].Item1 * Ratio[select].Item2));

            double livingLength = multiple * Ratio[select].Item1;
            double livingWidth = multiple * Ratio[select].Item2;

            //테스트를 위해, 고정값을 입력
            //LivingSize = new Data.Size(livingLength, livingWidth, Data.Config.RoomHeight);
            LivingSize = new Data.Size(700, 800, Data.Config.RoomHeight);

            //주방
            double ratioKitchen = 0.2;
            select = rnd.Next(Ratio.Length);

            multiple = Math.Sqrt((maxSpace * ratioKitchen) / (Ratio[select].Item1 * Ratio[select].Item2));

            double KitchenLength = multiple * Ratio[select].Item1;
            double KitchenWidth = multiple * Ratio[select].Item2;

            //테스트를 위해, 고정값을 입력
            //KitchenSize = new Data.Size(KitchenLength, KitchenWidth, Data.Config.RoomHeight);
            KitchenSize = new Data.Size(400, 500, Data.Config.RoomHeight);

            //침실
            BedSizes = new List<Data.Size>();
            for (int i = 1; i <= bedNum; i++)
            {
                select = rnd.Next(Roomspace.Length);
                BedSizes.Add(new Data.Size(Roomspace[select].Item1, Roomspace[select].Item2, Data.Config.RoomHeight));
            }

            //화장실
            RestSizes = new List<Data.Size>();
            for (int i = 1; i <= resNum; i++)
            {
                RestSizes.Add(new Data.Size(260, 300, Data.Config.RoomHeight));
            }
        }
    }

    //관계를 변화시킬 변수 목록
    class RelationVar
    {
        public bool is_allow_place_bed_next_kitchen; //이걸 해결하기 위해선, Side를 Occupy할 필요가 있다.
        public bool is_allow_place_bed_front_living;
        
        public RelationVar()
        {
            is_allow_place_bed_next_kitchen = false;
            is_allow_place_bed_front_living = false;
        }

    }
}
