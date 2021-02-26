using Egsp.Core;

namespace Game.Entities.Factories
{
    public readonly struct TreeGenerationProperties
    {
        public readonly int MaxTreeLevel;
            
        public readonly MinMaxInt EntitiesCount;
        
        public readonly MinMaxFloat Distance;
        
        public readonly MinMaxFloat Size;

        public TreeGenerationProperties(MinMaxInt entitiesCount, MinMaxFloat distance, MinMaxFloat size,
            int maxTreeLevel)
        {
            if (maxTreeLevel == 0)
                maxTreeLevel++;
            
            EntitiesCount = entitiesCount;
            Distance = distance;
            MaxTreeLevel = maxTreeLevel;
            Size = size;
        }

        public float Factor(int level)
        {
            return 1f - ((float) level / (float) MaxTreeLevel);
        }
    }
}