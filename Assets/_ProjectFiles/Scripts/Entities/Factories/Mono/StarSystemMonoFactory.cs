using System;
using System.Collections.Generic;
using System.Linq;
using Egsp.Core;
using UnityEngine;

namespace Game.Entities.Factories
{
    public static class StarSystemMonoFactory
    {
        private static Dictionary<Type, Func<SystemEntity, SystemEntityMono>> _matchedFactories;
        
        static StarSystemMonoFactory()
        {
            _matchedFactories = new Dictionary<Type,  Func<SystemEntity, SystemEntityMono>>();
            _matchedFactories.Add(typeof(Planet),
                entity => EntityMonoFactories.PlanetMonoFactory.InstancePrefabImmediately(entity as Planet));
        }

        public static StarSystemMono CreateFrom(StarSystem starSystem)
        {
            var rootObj = new GameObject($"Star system: {starSystem.GalaxyRelation.Id}");
            var systemMono = rootObj.AddComponent<StarSystemMono>();

            systemMono.Accept(starSystem);

            var entities = starSystem.GetEntities();

            var monoEntities = CreateMonos(rootObj.transform,entities);

            systemMono.MonoEntities = monoEntities.ToList();
            return systemMono;
        }

        private static IEnumerable<SystemEntityMono> CreateMonos(Transform root,IEnumerable<SystemEntity> rawEntities)
        {
            foreach (var rawEntity in rawEntities)
            {
                var mono = RawToMono(rawEntity);

                if (mono.Ok)
                {
                    var result = mono.Result;
                    result.transform.SetParent(root, true);
                    
                    yield return result;
                }
                else
                {
                    Debug.Log(mono.ErrorMessage);
                }
            }
        }

        private static Operation<SystemEntityMono> RawToMono(SystemEntity systemEntity)
        {
            var type = systemEntity.GetType();

            if (_matchedFactories.ContainsKey(type))
                return Operation<SystemEntityMono>.Correct(_matchedFactories[type].Invoke(systemEntity));

            return Operation<SystemEntityMono>.Error($"No mono factory for {type.Name}");
        }
    }

}