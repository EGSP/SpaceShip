using System.Collections.Generic;
using System.Linq;

namespace Game.Entities.Factories
{

    public class StarSystemObject
    {
        public readonly StarSystemObject Parent;

        public readonly float Size;
        public float Distance;

        public LinkedList<StarSystemObject> Line { get; private set; }
            = new LinkedList<StarSystemObject>();

        // Размер занимаемой области - это собственный размер + длина основной линии спутников.
        public float AreaSize { get; private set; }

        public float DistanceWithSize => Distance + Size;
        public float DistanceWithHalfSize => Distance + (Size / 2f);
        public float DistanceWithAreaSize => Distance + AreaSize;
        public float DistanceWithHalfAreaSize => Distance + (AreaSize / 2f);

        public StarSystemObject(float size, StarSystemObject parent)
        {
            Size = size;
            Parent = parent;
        }

        public void SetLine(LinkedList<StarSystemObject> line, bool calculateAreaSize = true)
        {
            Line = line;
            if (calculateAreaSize)
                AreaSize = Size + Line.Sum(x => x.DistanceWithHalfSize);
        }

        public float GetPosition()
        {
            if (Parent == null)
                return 0;

            return Parent.GetPosition() + Parent.Size / 2f + DistanceWithHalfAreaSize;
        }
    }
}