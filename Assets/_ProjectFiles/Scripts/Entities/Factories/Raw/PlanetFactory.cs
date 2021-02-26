using System;
using System.Collections.Generic;
using Egsp.Core;
using Egsp.Extensions.Linq;
using Game.Entities.Planets;

namespace Game.Entities.Factories
{
    public class PlanetFactory : SystemEntityFactory<Planet>
    {
        private static List<PlanetType> _planetTypes;
        private static readonly TreeGenerationProperties defaultPlanetProperties;
        public static ref readonly TreeGenerationProperties DefaultPlanetProperties => ref defaultPlanetProperties;
        
        private static readonly TreeGenerationProperties defaultMoonProperties;
        public static ref readonly TreeGenerationProperties DefaultMoonProperties => ref defaultMoonProperties;
        

        static PlanetFactory()
        {
            _planetTypes = new List<PlanetType>()
            {
                new SolidType.DesertType(),
                new LiquidType.LavaType(), new LiquidType.OceanType(),
                new GasType.GasDwarfType(), new GasType.GasGigantPlanetType()
            };

            defaultPlanetProperties = new TreeGenerationProperties(
                new MinMaxInt(1, 1), new MinMaxFloat(5f, 6f), new MinMaxFloat(2f, 5f)
                , 0);
            
            defaultMoonProperties = new TreeGenerationProperties(
                new MinMaxInt(1, 3), new MinMaxFloat(1.5f, 3f), new MinMaxFloat(0.6f, 1.3f)
                , 2);
        }

        public StarSystemObject GenerateRandomPlanetSystem(ref int id)
        {
            var mainPlanet = StarSystemObject.GenerateOne(in DefaultPlanetProperties, this, ref id);

            StarSystemObject.GenerateTree(mainPlanet, DefaultMoonProperties, this, ref id, 0);
            return mainPlanet;
        }

        public override Planet CreateEntity(int treeLevel, in TreeGenerationProperties properties)
        {
            if (treeLevel == 0)
                treeLevel = 1;
            
            return CreatePlanet(properties.Size.Random * 1f/(float)treeLevel);
        }
        
        // Создает планету без какой-либо информации.
        private Planet CreatePlanet(float size)
        {
            var type = _planetTypes.Random();

            if (type == null)
                throw new NullReferenceException();

            var planet = new Planet(type, size, 1);
            return planet;
        }
    }
}