using Egsp.Core;

namespace Game.Entities.Factories
{
    public class StarFactory : SystemEntityFactory<Star>
    {
        private static readonly TreeGenerationProperties defaultStarProperties;
        public static ref readonly TreeGenerationProperties DefaultStarProperties => ref defaultStarProperties;

        static StarFactory()
        {
            defaultStarProperties = new TreeGenerationProperties(
                new MinMaxInt(1, 1), new MinMaxFloat(0f, 0f), new MinMaxFloat(7f, 9f)
                , 0);
        }
        
        
        public StarSystemObject GenerateRandomStar(ref int id)
        {
            var star = StarSystemObject.GenerateOne(in DefaultStarProperties, this, ref id);
            return star;
        }
        
        public override Star CreateEntity(int treeLevel, in TreeGenerationProperties properties)
        {
            if (treeLevel == 0)
                treeLevel = 1;
            
            return CreateStar(properties.Size.Random * 1f / (float) treeLevel);
        }

        private Star CreateStar(float size)
        {
            var star = new Star(size, 1);

            return star;
        }
    }
}