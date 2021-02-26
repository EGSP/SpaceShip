using System.Collections.Generic;
using Game;
using Game.Entities;
using UnityEngine;

namespace Game.Entities.Factories
{

    public static class StarSystemFactory
    {
        public static StarSystem CreateRandomStarSystem(ref int id)
        {
            var planets = Random.Range(1, 6);
            
            var sun = EntityFactories.StarFactory.GenerateRandomStar(ref id);

            // Генерируем планеты и сразу вставляем их к звезде.
            for (var i = 0; i < planets; i++)
                sun.AddChild(EntityFactories.PlanetFactory.GenerateRandomPlanetSystem(ref id));

            var starSystem = new StarSystem(new InGalaxyRelation(0), sun);
            
            var starSystemObject = StarSystemObject.NewInvalid;
            StarSystemObject.CalculatePositions(starSystem.RootSystemObject, ref starSystemObject);

            return starSystem;
        }
    }
}