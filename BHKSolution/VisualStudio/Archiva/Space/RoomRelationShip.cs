using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archiva.Space
{
    enum ConnectionType
    {
        Open,
        Door,
        Level
    }

    //부여된 순서대로 배치가 진행된다.
    enum RelationLevel
    {
        First = 0,
        Second = 1
    }

    class RoomRelationShip : ICloneable, IEquatable<RoomRelationShip>, IComparable<RoomRelationShip>
    {
        public Space.Room Origin;
        public RelationLevel Level;
        public Dictionary<Guid, ConnectionType> Relations;

        public RoomRelationShip(Space.Room origin, RelationLevel level)
        {
            this.Origin = origin;
            this.Level = level;
            this.Relations = new Dictionary<Guid, ConnectionType>();
        }

        public object Clone()
        {
            RoomRelationShip clone = new RoomRelationShip((Space.Room)this.Origin.Clone(), this.Level);
            Dictionary<Guid, ConnectionType> tempRel = new Dictionary<Guid, ConnectionType>();
            foreach (KeyValuePair<Guid, ConnectionType> dic in Relations)
            {
                tempRel.Add(dic.Key, dic.Value);
            }
            clone.Relations = tempRel;
            return clone;
        }

        public void AddRelation(Space.Room room, ConnectionType type)
        {
            this.Relations.Add(room.id, type);
        }

        public void ChangeRelation(Space.Room room, ConnectionType newType)
        {
            this.Relations[room.id] = newType;
        }

        public void RemoveRelation(Space.Room room)
        {
            this.Relations.Remove(room.id);
        }

        public int CompareTo(RoomRelationShip other)
        {
            if (this.Relations.Count > other.Relations.Count)
            {

                return -1;
            }
            else if (this.Relations.Count < other.Relations.Count)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public bool Equals(RoomRelationShip other)
        {
            if (this.Origin.id == other.Origin.id)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
