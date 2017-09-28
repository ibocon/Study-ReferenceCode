using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva
{
    /// <summary>
    /// Blueprint 클래스는 Room 간의 관계를 정리하고, 배치하는 핵심적인 클래스다.
    /// </summary>
    class Blueprint : ICloneable
    {
        private static Random rng = new Random();

        //관계도를 구분하여, 배치할 수 있도록 개선
        private Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>> Relations;
        private MyXmlWriter Writer;
        private Data.Cord WorldLocation;

        public Blueprint(Data.Cord worldCord)
        {
            this.WorldLocation = worldCord;
            this.Relations = new Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>>();

            foreach (Space.RelationLevel lv in Enum.GetValues(typeof(Space.RelationLevel)))
            {
                Relations.Add(lv, new List<Space.RoomRelationShip>());
            }
        }

        public Blueprint(Data.Cord worldstdloc, MyXmlWriter wirter, Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>> rels)
        {
            this.WorldLocation = worldstdloc;

            Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>> relations = new Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>>();
            foreach (KeyValuePair<Space.RelationLevel, List<Space.RoomRelationShip>> entry in rels)
            {
                List<Space.RoomRelationShip> list = new List<Space.RoomRelationShip>();
                foreach (Space.RoomRelationShip rel in entry.Value)
                {
                    list.Add(rel);
                }
                relations.Add(entry.Key, list);
            }
            
            this.Relations = relations;
            this.Writer = wirter;
        }

        public object Clone()
        {
            Data.Cord tempStdLoc = WorldLocation;
            Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>> tempRel = new Dictionary<Space.RelationLevel, List<Space.RoomRelationShip>>();
            foreach (KeyValuePair<Space.RelationLevel, List<Space.RoomRelationShip>> entry in Relations)
            {
                List<Space.RoomRelationShip> list = new List<Space.RoomRelationShip>();
                foreach (Space.RoomRelationShip target in entry.Value)
                {
                    list.Add((Space.RoomRelationShip)target.Clone());
                }
                tempRel.Add(entry.Key, list);
            }
            //TODO: 새로운 블루프린트를 만들 때, Relations 복사가 중복계산된다.
            return new Blueprint(tempStdLoc, Writer, tempRel);
        }

        //관계도 구분을 고려하며 배치할 수 있도록 개선했다.
        public static List<Blueprint> AddNewRoom(Blueprint blueprint, Space.RoomRelationShip newRelation, Variables conditions)
        {
            Blueprint PreviousBlueprint = (Blueprint)blueprint.Clone();
            List<Blueprint> PossiblBlueprint = new List<Blueprint>();

            if (newRelation.Level == Space.RelationLevel.First)
            {
                //1층에 기준 방 배치가 안되어 있으므로, 맵 기준점에 배치한다.
                if (PreviousBlueprint.Relations[newRelation.Level].Count == 0)
                {
                    if (newRelation.Origin is Space.Entrance)
                    {
                        newRelation.Origin.ChangeLocation(PreviousBlueprint.WorldLocation);
                        PreviousBlueprint.Relations[newRelation.Level].Add(newRelation);
                        PossiblBlueprint.Add(PreviousBlueprint);

                        return PossiblBlueprint;
                    }
                    else
                    {
                        throw new NotImplementedException("Error");
                    }

                }
            }
            else if(newRelation.Level == Space.RelationLevel.Second)
            {
                if (PreviousBlueprint.Relations[newRelation.Level].Count == 0)
                {
                    if (newRelation.Origin is Space.Stair)
                    {
                        //1층에 있던 계단을 찾는다.
                        foreach (Space.RoomRelationShip level in PreviousBlueprint.Relations[Space.RelationLevel.First])
                        {
                            if (level.Origin is Space.Stair)
                            {
                                newRelation.Origin.ChangeLocation(new Data.Cord(level.Origin.Location.X, level.Origin.Location.Y, level.Origin.Location.Z + Data.Config.RoomHeight));

                                foreach (Data.SideCardinalDirection sideDirection in Enum.GetValues(typeof(Data.SideCardinalDirection)))
                                {
                                    foreach (Data.Side levelOneSide in level.Origin.Sides[sideDirection])
                                    {
                                        if (levelOneSide.Type == Data.SideType.Wall)
                                        {
                                            foreach (Data.Side levelTwoSide in newRelation.Origin.Sides[sideDirection])
                                            {
                                                //단순히 타입을 Block으로 바꾸는 것보다는 괜찮은 방법이 필요함.
                                                levelTwoSide.Type = Data.SideType.Window;
                                            }
                                        }
                                    }
                                }

                                PreviousBlueprint.Relations[newRelation.Level].Add((Space.RoomRelationShip)newRelation.Clone());
                                break;
                            }
                        }

                        PossiblBlueprint.Add(PreviousBlueprint);
                        return PossiblBlueprint;
                    }
                    else
                    {
                        throw new NotImplementedException("Error");
                    }
                }
            }
            else
            {
                throw new NotImplementedException("New Relation Level does not handled.");
            }

            #region 설명
            /*
            *   배치에 제일 핵심이 되는 부분!
            *   1) 배치가능한 SidePart를 검색한다. (현재는 MinimalOverlap보다 긴 Side)
            *   2) 배치가 가능한지 판단을 한다. (Overlap이 되지 않았는지, 관계에 맞게 Cotact하게 배치되었는지)
            *   
            *   3) 배치가 가능하면, 
            *      (Room의 OccupySide)
            *          - Side를 Occupy한다.
            *          - 새로운 Side 상태를 기록한다.
            *          - 이전 Side 상태도 남긴다.
            *          
            *          - Relation에 배치된 공간을 추가한다.
            *          - 새로운 공간이 추가된 도면을 반환할 도면리스트에 추가한다.
            *          
            *   4) 배치가 불가능하다면, 다른 SidePart를 탐색하고, 다른 Side와의 가능성도 계산한다.
            *          
            *   5) 만약 배치되어 있던 모든 공간의 모든 Side가 불가능하다면, 
            *         - NULL 값을 반환한다.
            *   6) 모든 가능성을 탐구하였다면, 가능한 모든 도면리스트를 반환한다.
            * 
            */
            #endregion

            //TODO: 1층이라도 Boundary를 체크할 수 있도록 개선할 필요가 있다.
            foreach (Space.RoomRelationShip rel in PreviousBlueprint.Relations[newRelation.Level].ToArray())
            {

                //현재까지 배치된 공간 중, 배치할 공간과 관련이 있는 공간만 해당
                if (newRelation.Relations.ContainsKey(rel.Origin.id))
                {
                    foreach (Data.SideCardinalDirection sidePos in Enum.GetValues(typeof(Data.SideCardinalDirection)))
                    {
                        foreach (Data.Side side in rel.Origin.Sides[sidePos].ToArray())
                        {
                            if (side.IsAvailable(Data.Config.MinimalOverlap))
                            {
                                //sidepart 중 배치가 가능한 부분을 순차적으로 검색하며 찾는 것이 중요. 
                                //(2진 검색을 하듯이, Start와 End의 중간값 찾기와 같이 배치할 위치를 찾는다.)
                                List<Data.Cord> avalCords = PreviousBlueprint.GetPosibleCords(newRelation.Origin, side, conditions.is_simple);
                                foreach (Data.Cord cord in avalCords)
                                {
                                    //TODO: Rotate를 한 뒤에도 배치 테스트를 할 수 있도록 개선이 필요하다.

                                    //새로 배치될 수 있는 위치를 시도!
                                    Space.RoomRelationShip CloneRel = (Space.RoomRelationShip)newRelation.Clone();
                                    CloneRel.Origin.ChangeLocation((Data.Cord)cord.Clone()); // 위치를 바꾸기 전에, Side 정보를 복구 또는 저장해놓을 필요가 있다.
                                    
                                    //배치 이후에 기존의 공간과의 모순이 없는지 테스트 (Contact와 Overlap 체크)
                                    bool failed = false;

                                    #region CheckOverlap
                                    foreach (Space.RoomRelationShip test in PreviousBlueprint.Relations[newRelation.Level])
                                    {
                                        //이미 배치된 공간과 겹치는지 확인
                                        if (Space.Room.IsOverlap(test.Origin, CloneRel.Origin))
                                        {
                                            failed = true;
                                            break;
                                        }

                                    }
                                    #endregion

                                    #region CheckBoundary
                                    if (!failed)
                                    {
                                        
                                    }
                                    #endregion

                                    #region CheckConnect
                                    if (!failed)
                                    {
                                        foreach (Space.RoomRelationShip test in PreviousBlueprint.Relations[newRelation.Level])
                                        {

                                            //새로운 공간과 관계가 있는 공간이 연결되었는지 확인
                                            if (CloneRel.Relations.ContainsKey(test.Origin.id))
                                            {
                                                if (Space.Room.IsContact(CloneRel.Origin, test.Origin, Data.Config.MinimalOverlap))
                                                {
                                                    //Room의 함수로 들어가야 하는 부분
                                                    //겹치게 된 라인을 찾아 점령
                                                    Space.Room.OccupySides(test.Origin, CloneRel.Origin, CloneRel.Relations[test.Origin.id]);
                                                }
                                                else
                                                {
                                                    failed = true;
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    //모든 테스트를 통과했다면,
                                    //  1) 관계도에 포함시킨다.
                                    //  2) 새로운 도면을 출력 리스트에 추가시킨다.
                                    if (!failed)
                                    {
                                        PreviousBlueprint.Relations[newRelation.Level].Add((Space.RoomRelationShip)CloneRel.Clone());
                                        PossiblBlueprint.Add(PreviousBlueprint);
                                        PreviousBlueprint = (Blueprint)blueprint.Clone();
                                    }

                                }
                            }

                        }
                    }
                }
            }

            return PossiblBlueprint;

        }

        public void CreateWindows() //Interpreter로 부터 받을 변수가 있다면 추가
        {
            foreach (KeyValuePair<Space.RelationLevel, List<Space.RoomRelationShip>> entry in this.Relations)
            {
                foreach (Space.RoomRelationShip rel in entry.Value)
                {
                    //현재는 침실만 창문을 달고 있다.
                    if (rel.Origin is Space.Bed || rel.Origin is Space.Living)
                    {
                        int winnum = 0;
                        //Window가 가능한 Side를 찾는 과정
                        foreach (Data.SideCardinalDirection relsideDirection in Enum.GetValues(typeof(Data.SideCardinalDirection)))
                        {
                            foreach (Data.Side relSide in rel.Origin.Sides[relsideDirection])
                            {
                                if (relSide.Type == Data.SideType.Wall && relSide.GetLength() > Data.Config.WindowLength && this.IsOutterSide(rel.Origin.id, relSide, rel.Level))
                                {
                                    relSide.Type = Data.SideType.Window;
                                    //한 사이드 방향에 하나의 창문만
                                    winnum++;
                                    break;
                                }
                            }

                            if (winnum >= 2)
                            {
                                break;
                            }
                        }
                    }
                    //화장실과 주방에는 창문 하나
                    else if (rel.Origin is Space.Kitchen || rel.Origin is Space.Rest)
                    {
                        bool placed = false;

                        //어느 Side부터 탐색할지를 랜덤하게 결정하는 과정
                        List<Data.SideCardinalDirection> search = new List<Data.SideCardinalDirection>();
                        foreach (Data.SideCardinalDirection elem in Enum.GetValues(typeof(Data.SideCardinalDirection)))
                        {
                            search.Add(elem);
                        }

                        int n = search.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = rng.Next(n + 1);
                            Data.SideCardinalDirection value = search[k];
                            search[k] = search[n];
                            search[n] = value;
                        }

                        //Window가 가능한 Side를 찾는 과정
                        foreach (Data.SideCardinalDirection relsideDirection in search)
                        {
                            foreach (Data.Side relSide in rel.Origin.Sides[relsideDirection])
                            {
                                if (relSide.Type == Data.SideType.Wall && relSide.GetLength() > Data.Config.WindowLength && this.IsOutterSide(rel.Origin.id, relSide, rel.Level))
                                {
                                    relSide.Type = Data.SideType.Window;
                                    placed = true;
                                    break;
                                }
                            }

                            if (placed)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            
        }
        
        public void CreateEnterence() //Interpreter로 부터 받을 변수가 있다면 추가
        {
            //1층에서만 현관을 찾는다.
            foreach(Space.RoomRelationShip rel in this.Relations[Space.RelationLevel.First])
            {
                if (rel.Origin is Space.Entrance)
                {
                    Space.RoomRelationShip Ent = rel;
                    bool placed = false;

                    //어느 Side부터 탐색할지를 랜덤하게 결정하는 과정
                    List<Data.SideCardinalDirection> search = new List<Data.SideCardinalDirection>();
                    foreach (Data.SideCardinalDirection elem in Enum.GetValues(typeof(Data.SideCardinalDirection)))
                    {
                        search.Add(elem);
                    }

                    int n = search.Count;
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        Data.SideCardinalDirection value = search[k];
                        search[k] = search[n];
                        search[n] = value;
                    }

                    foreach (Data.SideCardinalDirection relsideDirection in search)
                    {
                        foreach (Data.Side relSide in rel.Origin.Sides[relsideDirection])
                        {
                            if (relSide.Type == Data.SideType.Wall && relSide.GetLength() > Data.Config.DoorLength && this.IsOutterSide(rel.Origin.id, relSide, rel.Level))
                            {
                                relSide.Type = Data.SideType.Door;
                                placed = true;
                                break;
                            }
                        }

                        if (placed)
                        {
                            break;
                        }
                    }

                    break;
                }
            }
        }

        //id는 입력된 Side가 소속된 공간
        private bool IsOutterSide(Guid id, Data.Side side, Space.RelationLevel level)
        {
            foreach (Space.RoomRelationShip other in this.Relations[level])
            {
                if (id != other.Origin.id)
                {
                    foreach (Data.SideCardinalDirection othersideDirection in Enum.GetValues(typeof(Data.SideCardinalDirection)))
                    {
                        foreach (Data.Side otherSide in other.Origin.Sides[othersideDirection])
                        {
                            if (side.IsContact(otherSide))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
            
        }

        public void CreateXML(string path)
        {

            this.Writer = new MyXmlWriter(path);
            Writer.StartToWrite();
            int cameraNumber = 1;
            foreach (List<Space.RoomRelationShip> relations in this.Relations.Values)
            {
                foreach (Space.RoomRelationShip rel in relations)
                {
                    //Side가 모두 계산된 후에, 벽을 생성한다.
                    bool isFloor = true;
                    if(rel.Level == Space.RelationLevel.Second && rel.Origin is Space.Stair)
                    {
                        isFloor = false;
                    }

                    //지붕을 판단할 알고리즘이 필요하다.
                    bool isRoof = true;
                    if (rel.Level == Space.RelationLevel.First && rel.Origin is Space.Stair)
                    {
                        isRoof = false;
                    }

                    bool isStair = false;
                    if (rel.Level == Space.RelationLevel.First && rel.Origin is Space.Stair)
                    {
                        isStair = true;
                    }
                    
                    rel.Origin.CreateComponents(isFloor, isRoof, cameraNumber++, isStair, rel.Level);
                    rel.Origin.WriteModel(Writer);
                }
            }
            Writer.EndToWrite();

        }
        
        private List<Data.Cord> GetPosibleCords(Space.Room room, Data.Side target, bool is_simple)
        {
            List<Data.Cord> avalCords = new List<Data.Cord>();
            Data.Cord start = new Data.Cord();
            Data.Cord end = new Data.Cord();
            switch (target.Position)
            {
                case Data.SideCardinalDirection.West:
                    start = new Data.Cord(target.Start, -room.Size.Length, 0, 0);
                    end = new Data.Cord(target.End, -room.Size.Length, -room.Size.Width, 0);
                    break;
                case Data.SideCardinalDirection.North:
                    start = new Data.Cord(target.Start, 0, 0, 0);
                    end = new Data.Cord(target.End, -room.Size.Length, 0, 0);
                    
                    break;
                case Data.SideCardinalDirection.East:
                    start = new Data.Cord(target.Start, 0, 0, 0);
                    end = new Data.Cord(target.End, 0, -room.Size.Width, 0);
                    break;
                case Data.SideCardinalDirection.South:
                    start = new Data.Cord(target.Start, 0, -room.Size.Width, 0);
                    end = new Data.Cord(target.End, -room.Size.Length, -room.Size.Width, 0);
                    break;
            }
            avalCords.Add(start);
            avalCords.Add(end);
            if (!is_simple)
            {
                Data.Cord last = this.GetPosibleCord(avalCords, new Data.Side(target.Position, target.Type, start, end), Data.Config.MinimalOverlap);
                if (last != null)
                {
                    avalCords.Add(last);
                }
            }
            return avalCords;
        }

        private Data.Cord GetPosibleCord(List<Data.Cord> collect, Data.Side side, double length)
        {
            if(side.GetLength() < length) return null;

            Data.Cord mid = side.GetMidCord();

            Data.Cord before = GetPosibleCord(collect, new Data.Side(side.Position, side.Type, side.Start, mid), length);
            if(before != null)
            {
                collect.Add(before);
            }
            Data.Cord after = GetPosibleCord(collect, new Data.Side(side.Position, side.Type, mid, side.End), length);
            if(after != null)
            {
                collect.Add(after);
            }

            return mid;
        }
    }
}
