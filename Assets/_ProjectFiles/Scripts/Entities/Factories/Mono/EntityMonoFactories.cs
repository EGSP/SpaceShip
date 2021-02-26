namespace Game.Entities.Factories
{
    public static class EntityMonoFactories
    {
        public static PlanetMonoFactory PlanetMonoFactory { get; private set; }

        static EntityMonoFactories()
        {
            PlanetMonoFactory = new PlanetMonoFactory();
        }
    }
}