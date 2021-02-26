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

        public static StarSystemMono CreateFromRaw(StarSystem starSystem)
        {
            var rootObj = new GameObject($"Star system: {starSystem.GalaxyRelation.Id}");
            var systemMono = rootObj.AddComponent<StarSystemMono>();

            systemMono.Accept(starSystem);

            var entities = starSystem.GetObjects();
            var starSystemObjects = entities.ToArray();
            
            var monoEntities = CreateMonos(rootObj.transform, starSystemObjects);
            var monoEntitiesList = monoEntities.ToList();
            
            for (var i = 0; i < starSystemObjects.Length; i++)
            {
                var op = starSystemObjects[i].NotifyWaitingMonos();
                if (op.NotOk)
                    Debug.Log($"{op.ErrorMessage}");
            }

            systemMono.MonoEntities = monoEntitiesList;
            return systemMono;
        }

        private static IEnumerable<SystemEntityMono> CreateMonos(Transform root, StarSystemObject[] objects)
        {
            for (var index = 0; index < objects.Length; index++)
            {
                var obj = objects[index];
                var mono = RawToMono(obj.Entity);

                if (mono.Ok)
                {
                    var result = mono.Result;
                    obj.OwnerMono = result;
                    obj.Parent?.WaitingMonos.AddLast(result);

                    yield return result;
                }
                else
                {
                    Debug.Log(mono.ErrorMessage);
                }
            }
        }

        private static Op<SystemEntityMono> RawToMono(SystemEntity systemEntity)
        {
            var type = systemEntity.GetType();

            if (_matchedFactories.ContainsKey(type))
                return Op<SystemEntityMono>.Correct(_matchedFactories[type].Invoke(systemEntity));

            return Op<SystemEntityMono>.Error($"No mono factory for {type.Name}");
        }
    }

}