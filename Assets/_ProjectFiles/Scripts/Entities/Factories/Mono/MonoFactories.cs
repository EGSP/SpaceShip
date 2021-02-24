namespace Game.Entities.Factories
{
    public static class MonoFactories
    {
        public static PlanetMonoFactory PlanetMonoFactory { get; private set; }

        static MonoFactories()
        {
            PlanetMonoFactory = new PlanetMonoFactory();
        }
    }
}