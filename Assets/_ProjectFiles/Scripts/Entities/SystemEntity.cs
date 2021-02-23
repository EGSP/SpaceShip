using UnityEngine;

namespace Game.Entities
{
    public readonly struct InSystemPosition
    {
        public readonly Vector3 Direction;
        public readonly float Distance;
        
        public readonly float SelfRotationSpeed;
        public readonly bool Clockwise;

        public InSystemPosition(Vector3 direction, float distance, float selfRotationSpeed, bool clockwise = false)
        {
            Direction = direction;
            Distance = distance;
            SelfRotationSpeed = selfRotationSpeed;
            Clockwise = clockwise;
        }
    }

    public readonly struct InSystemRelation
    {
        public readonly int Id;
        
        public readonly int OrbitId;
        public readonly float OrbitSpeed;

        public InSystemRelation(int id, int orbitId, float orbitSpeed)
        {
            Id = id;
            if (orbitId == id)
            {
                orbitId--;
            }

            OrbitId = orbitId;
            OrbitSpeed = orbitSpeed;
        }
    }
    
    public abstract class SystemEntity: SpaceEntity
    {
        public readonly InSystemPosition Position;
        public readonly InSystemRelation Relation;

        public readonly float Size;

        protected SystemEntity(InSystemPosition position, InSystemRelation relation, float size)
        {
            Position = position;
            Relation = relation;
            Size = size;
        }
    }
}