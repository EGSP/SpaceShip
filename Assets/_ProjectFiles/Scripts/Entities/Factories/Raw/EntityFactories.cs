namespace Game.Entities.Factories
{
    public static class EntityFactories
    {
        public static PlanetFactory PlanetFactory { get; private set; }
        public static StarFactory StarFactory { get; private set; }

        static EntityFactories()
        {
            PlanetFactory = new PlanetFactory();
            StarFactory = new StarFactory();
        }
    }
}