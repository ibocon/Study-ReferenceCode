using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva
{
    //변수로 공간의 크기와 관계를 설정하고, 공간 관계의 정의로 도출된 도면을 반환한다.
    class Interpreter
    {
        public Dictionary<Guid, Space.Room> Rooms;
        public Dictionary<Guid, Space.RoomRelationShip> Relationships;
        private Variables Conditions;

        public Interpreter(Variables conditions)
        {
            Rooms = new Dictionary<Guid, Space.Room>();
            Relationships = new Dictionary<Guid, Space.RoomRelationShip>();
            Conditions = conditions;
        }

        public List<List<Blueprint>> GenerateBlueprints() //배치순서를 결정할 수 있는 변수들을 입력
        {
            //리턴할 준비
            List<List<Blueprint>> Results = new List<List<Blueprint>>();

            //Coditions을 활용하여 Rooms과 관계도를 생성하여, 객체에 저장
            GenerateRoomsAndRel();

            //배치과정을 기록하는 과정
            Dictionary<Space.RelationLevel, Space.RoomRelationShip> StartPoint = new Dictionary<Space.RelationLevel, Space.RoomRelationShip>();
            List<Guid> PlacedRelList = new List<Guid>();
            Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>> NotPlacedRelList = new Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>>();
            
            //초기화
            foreach (Space.RelationLevel lv in Enum.GetValues(typeof(Space.RelationLevel)))
            {
                NotPlacedRelList.Add(lv, new List<Space.RoomRelationShip>());

                //찾는 타입의 방으로 시작할 지점을 찾아, List를 만든다.
                if (lv == Space.RelationLevel.First)
                {
                    StartPoint.Add(lv, CreateWaitingList<Space.Entrance>(PlacedRelList, NotPlacedRelList, lv));
                }
                else if (lv == Space.RelationLevel.Second)
                {
                    StartPoint.Add(lv, CreateWaitingList<Space.Stair>(PlacedRelList, NotPlacedRelList, lv));
                }
            }

            //실제로 배치가 진행되는 과정. (모든 룸이 배치될 때까지 배치)
            foreach (Space.RelationLevel lv in Enum.GetValues(typeof(Space.RelationLevel)))
            {
                //StartPoint를 배치한다.
                HelpToGeneareteBlueprint(Results, StartPoint[lv]);

                while (NotPlacedRelList[lv].Count != 0)
                {
                    //새로 배치할 대상
                    Space.RoomRelationShip target = NotPlacedRelList[lv][0];

                    //실제로 배치가 진행되는 함수
                    HelpToGeneareteBlueprint(Results, target); //Results에 포함된 가능성을 새로운 가능성으로 전환

                    //배치완료를 기록
                    PlacedRelList.Add(target.Origin.id);
                    //Wating 리스트에서 제거
                    NotPlacedRelList[lv].Remove(target);

                    foreach (Guid id in target.Relations.Keys)
                    {
                        if (!(PlacedRelList.Contains(id) || NotPlacedRelList[lv].Contains(Relationships[id])) && target.Level == Relationships[id].Level)
                        {
                            NotPlacedRelList[lv].Add(Relationships[id]);
                        }
                    }
                    //관계 수에 따라 다시 정렬!
                    NotPlacedRelList[lv].Sort();
                }
            }

            //모든 배치가 완료된 상황에서, 창문과 현관문을 배치하기 위해서 외곽선을 파악하고 각 공간마다 창문 타입의 Side를 지정할 수 있도록 한다.
            foreach (List<Blueprint> bps in Results)
            {
                foreach (Blueprint bp in bps)
                {
                    bp.CreateEnterence();
                    bp.CreateWindows();
                    //TODO: 현재 계단작업은 잠시 중단되었다.
                    //bp.CreateStair();
                    
                }
            }
            return Results;
        }

        private Space.RoomRelationShip CreateWaitingList<Initial>(List<Guid> PlacedRelList, Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>> NotPlacedRelList, Space.RelationLevel lv)
        {
            Space.RoomRelationShip init = new Space.RoomRelationShip(new Space.Room(), lv);
            foreach (Space.RoomRelationShip rel in Relationships.Values)
            {
                if (rel.Level == lv && rel.Origin is Initial)
                {
                    init = rel;
                }
            }

            if (init.Relations.Count == 0)
            {
                throw new System.Exception("Not Found initial start point at func CreateWaitingList");
            }

            PlacedRelList.Add(init.Origin.id);

            foreach (Guid id in init.Relations.Keys)
            {
                if (Relationships[id].Level == lv)
                {
                    NotPlacedRelList[lv].Add(Relationships[id]);
                }
            }
            NotPlacedRelList[lv].Sort();

            return init;
        }

        private void HelpToGeneareteBlueprint(List<List<Blueprint>> results, Space.RoomRelationShip target)
        {
            if (results.Count == 0)
            {
                Blueprint Origin = new Blueprint(Conditions.WorldStdLocation);
                Space.RoomRelationShip Enterence = new Space.RoomRelationShip(new Space.Room(), Space.RelationLevel.First);
                //현관을 찾아, Origin에 배치하여 RootCase를 만듬.
                foreach (Space.RoomRelationShip rel in Relationships.Values)
                {
                    if (rel.Origin is Space.Entrance)
                    {
                        Enterence = rel;
                        //배치를 시작하는 지점을 찾았고, Results에 저장
                        results.Add(Blueprint.AddNewRoom(Origin, rel, Conditions));
                        break;
                    }
                }
            }
            else
            {
                List<Blueprint>[] rootCase = results.ToArray();
                results.Clear();
                foreach (List<Blueprint> cases in rootCase)
                {
                    foreach (Blueprint blue in cases)
                    {
                        results.Add(Blueprint.AddNewRoom(blue, target, Conditions));
                    }
                }
            }
        }

        //배치의 기준을 잡는 핵심적인 논리파트
        private void GenerateRoomsAndRel() //관계를 정의하기 위한 변수를 추가하려면 여기에 추가할 것!
        {
            Space.RoomRelationShip connection = PlanFirstLevel(); //1층을 설계하는데 필요한 변수는 여기에 추가할 것!

            connection = PlanSecondLevel(connection); //2층을 설계하는데 필요한 변수는 여기에 추가할 것!
        }

        private Space.RoomRelationShip PlanFirstLevel()
        {
            //현관 정의
            Space.Room enterence = new Space.Entrance(Conditions.RoomSize.EnterenceSize, GetMaterials(Space.RoomType.Entrance), Conditions.StaticComponents);
            Space.RoomRelationShip enterenceRel = new Space.RoomRelationShip(enterence, Space.RelationLevel.First);
            this.Register(enterenceRel);

            //거실 정의
            Space.Room living = new Space.Living(Conditions.RoomSize.LivingSize, GetMaterials(Space.RoomType.Living), Conditions.StaticComponents);
            Space.RoomRelationShip livingRel = new Space.RoomRelationShip(living, Space.RelationLevel.First);

                //거실 남향을 통창으로 바꿈
            living.Sides[Data.SideCardinalDirection.South][0].Type = Data.SideType.WindowWall;

            this.ConnectRelation(livingRel, enterenceRel, Space.ConnectionType.Door);
            this.Register(livingRel);

            //주방 정의
            Space.Room kitchen = new Space.Kitchen(Conditions.RoomSize.KitchenSize, GetMaterials(Space.RoomType.Kitchen), Conditions.StaticComponents);
            Space.RoomRelationShip kitchenRel = new Space.RoomRelationShip(kitchen, Space.RelationLevel.First);

            this.ConnectRelation(livingRel, kitchenRel, Space.ConnectionType.Open);
            this.Register(kitchenRel);

            //침실 정의
            List<Space.Room> beds = new List<Space.Room>();
           
            foreach (Data.Size size in Conditions.RoomSize.BedSizes)
            {

                Space.Room bed = new Space.Bed(size, GetMaterials(Space.RoomType.Bed), Conditions.StaticComponents);
                Space.RoomRelationShip bedRel = new Space.RoomRelationShip(bed, Space.RelationLevel.First);

                this.ConnectRelation(livingRel, bedRel, Space.ConnectionType.Door);
                this.Register(bedRel);

                beds.Add(bed);
            }

            beds.Sort(); //침실을 크기에 따라 정렬

            //화장실 정의

            for (int i = 0; i < Conditions.RoomSize.RestSizes.Count; i++)
            {
                Space.Room rest = new Space.Rest(Conditions.RoomSize.RestSizes[i], GetMaterials(Space.RoomType.Rest), Conditions.StaticComponents);
                Space.RoomRelationShip restRel = new Space.RoomRelationShip(rest, Space.RelationLevel.First);

                if (i == 0)
                {
                    this.ConnectRelation(livingRel, restRel, Space.ConnectionType.Door);
                }
                else
                {
                    if (beds.Count != 0)
                    {
                        Space.Room target = beds.First();
                        this.ConnectRelation(this.Relationships[target.id], restRel, Space.ConnectionType.Door);
                        beds.Remove(target);
                    }
                }

                this.Register(restRel);
            }

            //계단 정의
            Space.Room stair = new Space.Stair(new Data.Size(260, 400, Data.Config.RoomHeight), GetMaterials(Space.RoomType.Stair), Conditions.StaticComponents );
            Space.RoomRelationShip stairRel = new Space.RoomRelationShip(stair, Space.RelationLevel.First);

            this.ConnectRelation(livingRel, stairRel, Space.ConnectionType.Open);

            this.Register(stairRel);

            return stairRel;
        }

        private Space.RoomRelationShip PlanSecondLevel(Space.RoomRelationShip connRel)
        {
            //계단 정의
            Space.Room stair = new Space.Stair(connRel.Origin.Size, connRel.Origin.Materials, Conditions.StaticComponents);
            Space.RoomRelationShip stairRel = new Space.RoomRelationShip(stair, Space.RelationLevel.Second);

            this.ConnectRelation(stairRel, connRel, Space.ConnectionType.Level);

            this.Register(stairRel);
            
            //가족실(거실) 정의
            Space.Room family = new Space.Living(new Data.Size(360, 400, Data.Config.RoomHeight), GetMaterials(Space.RoomType.Living), Conditions.StaticComponents);
            Space.RoomRelationShip familyRel = new Space.RoomRelationShip(family, Space.RelationLevel.Second);

            this.ConnectRelation(stairRel, familyRel, Space.ConnectionType.Open);

            this.Register(familyRel);

            //침실 정의
            Space.Room bed = new Space.Bed(new Data.Size(350, 380, Data.Config.RoomHeight), GetMaterials(Space.RoomType.Bed), Conditions.StaticComponents);
            Space.RoomRelationShip bedRel = new Space.RoomRelationShip(bed, Space.RelationLevel.Second);

            this.ConnectRelation(bedRel, familyRel, Space.ConnectionType.Door);
            this.Register(bedRel);

            //화장실 정의
            Space.Room rest = new Space.Rest(new Data.Size(260, 300, Data.Config.RoomHeight), GetMaterials(Space.RoomType.Rest), Conditions.StaticComponents);
            Space.RoomRelationShip restRel = new Space.RoomRelationShip(rest, Space.RelationLevel.Second);
             
            this.ConnectRelation(restRel, familyRel, Space.ConnectionType.Door);
            this.Register(restRel);

            return stairRel;
        }

        //TODO: 새로운 Material 종류를 추가할 때마다 수정해야 하는 불편함이 있으므로, 개선할 필요가 있다. Coditions에도 ModelType을 활용하면 해결가능할 것이다.
        private Dictionary<Model.ModelType, Dictionary<string, string>> GetMaterials(Space.RoomType type)
        {
            Dictionary<Model.ModelType, Dictionary<string, string>> Material = new Dictionary<Model.ModelType, Dictionary<string, string>>()
            {
                { Model.ModelType.Floor, Conditions.FloorMaterials[type] },
                { Model.ModelType.Wall, Conditions.WallMaterials[type] },
                { Model.ModelType.Fixedwindow, Conditions.FixedwindowMaterials[type] },
                { Model.ModelType.Flatroof, Conditions.FlatroofMaterials[type] },
                { Model.ModelType.Fullstair , Conditions.FullstairMaterials[type] }
            };

            return Material;
        }

        private void Register(Space.RoomRelationShip rel)
        {
            Rooms.Add(rel.Origin.id, rel.Origin);
            Relationships.Add(rel.Origin.id, rel);
        }

        private void ConnectRelation(Space.RoomRelationShip a, Space.RoomRelationShip b, Space.ConnectionType type)
        {
            a.AddRelation(b.Origin, type);
            b.AddRelation(a.Origin, type);
        }

    }

    
}
