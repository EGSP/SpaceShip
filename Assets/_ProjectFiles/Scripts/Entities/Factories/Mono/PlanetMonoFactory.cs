using System;
using System.Collections.Generic;
using Egsp.Utils.MeshUtilities;
using Game.Entities.Planets;
using UnityEngine;

namespace Game.Entities.Factories
{
    public sealed class PlanetMonoFactory : SystemEntityMonoFactory<Planet, PlanetMono>
    {
        protected override string PathToPrefab => "Assets/Prefabs/Planet.prefab";

        protected override IEnumerable<ColorFactory> AddColorFactories()
        {
            yield return new ColorFactory<PlanetType.SolidType>(
                new Color[]
                {
                    new Color(0.372f, 0.2f, 0.066f), new Color(0.486f, 0.329f, 0.192f),
                    new Color(0.447f, 0.549f, 0.321f), new Color(0.729f, 0.737f, 0.443f)
                });
            yield return new ColorFactory<PlanetType.LiquidType>(
                new Color[]
                {
                    new Color(0.094f, 0.274f, 0.509f), new Color(0.505f, 0.698f, 0.898f)
                });
            yield return new ColorFactory<PlanetType.GasType>(
                new Color[]
                {
                    new Color(0.537f, 0.552f, 0.952f), new Color(0.098f, 0.196f, 0.239f),
                    new Color(0.172f, 0.266f, 0.239f), new Color(0.42f, 0.027f, 0.2f)
                });
        }

        public override Color GetColorFor(Entity entity)
        {
            return base.GetColorFor(entity);
        }

        public override void AcceptRawObject(PlanetMono systemEntityMono, Planet systemEntity)
        {
            systemEntityMono.Accept(systemEntity);
        }
    }
}