using Egsp.Core;

namespace Game.Entities.Factories
{
    public abstract class SystemEntityFactory<TEntity> where TEntity: SystemEntity
    {
        public abstract TEntity CreateEntity(int treeLevel, in TreeGenerationProperties properties);
    }
    
    public class StarFactory : SystemEntityFactory<Star>
    {
        private static readonly TreeGenerationProperties defaultStarProperties;
        public static ref readonly TreeGenerationProperties DefaultPlanetProperties => ref defaultStarProperties;

        static StarFactory()
        {
            defaultStarProperties = new TreeGenerationProperties(
                new MinMaxInt(1, 1), new MinMaxFloat(5f, 6f), new MinMaxFloat(7f, 9f)
                , 0);
        }
        
        
        public StarSystemObject GenerateRandomStar(ref int id)
        {
            var star = StarSystemObject.GenerateOne(in DefaultPlanetProperties, this, ref id);
            return star;
        }
        
        public override Star CreateEntity(int treeLevel, in TreeGenerationProperties properties)
        {
            return CreateStar(properties.Size.Random * 1f / (float) treeLevel);
        }

        private Star CreateStar(float size)
        {
            var star = new Star(size, 1);

            return star;
        }
    }
}