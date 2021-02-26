using System;
using System.Collections.Generic;
using Egsp.Utils.MeshUtilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Game.Entities.Factories
{
    public sealed class PlanetMonoFactory : SystemEntityMonoFactory<Planet, PlanetMono>
    {
        protected override string PathToPrefab => "Assets/Prefabs/Planet.prefab";
        
        public override void AcceptRawObject(PlanetMono systemEntityMono, Planet systemEntity)
        {
            systemEntityMono.Accept(systemEntity);
        }
    }
}