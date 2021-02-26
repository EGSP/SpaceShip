using UnityEngine;

namespace Game.Entities
{
    public readonly struct InSystemPosition
    {
        public readonly float Distance;

        public Vector3 MultipliedDirection => Vector3.right * Distance;

        public InSystemPosition(float distance)
        {
            Distance = distance;
        }
    }

    public readonly struct InSystemRelation
    {
        public readonly int Id;
        public readonly int OrbitId;

        public InSystemRelation(int id, int orbitId)
        {
            Id = id;
            if (orbitId == id)
            {
                orbitId--;
            }

            OrbitId = orbitId;
        }
    }
    
    public abstract class SystemEntity: SpaceEntity
    {
        public InSystemPosition Position;
        public InSystemRelation Relation;

        public readonly float Size;
        public readonly float Rotation;
        public readonly float OrbitRotation;
        
        public readonly bool Clockwise;

        protected SystemEntity(float size, float rotation,
            float orbitRotation=0, bool clockwise = false)
        {
            Size = size;
            Rotation = rotation;
            OrbitRotation = orbitRotation;
            Clockwise = clockwise;
        }
    }
}