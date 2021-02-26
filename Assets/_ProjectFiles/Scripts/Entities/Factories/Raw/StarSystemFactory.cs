using System.Collections.Generic;
using Game;
using Game.Entities;

namespace Game.Entities.Factories
{

    public static class StarSystemFactory
    {
        public static StarSystem CreateRandomStarSystem()
        {
            var starSystem = new StarSystem(new InGalaxyRelation(0));

            return null;
        }
    }
}