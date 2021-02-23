using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Game.Entities
{
    public struct InGalaxyRelation
    {
        public readonly int Id;

        public InGalaxyRelation(int id)
        {
            Id = id;
        }
    }

    public class StarSystem : SpaceEntity
    {
        public readonly InGalaxyRelation Info;

        private readonly List<Star> _stars;
        private readonly List<Planet> _planets;

        public StarSystemEntity[] AllEntities => _stars.WithArray<Star, Planet, StarSystemEntity>(_planets);

        public StarSystem(InGalaxyRelation info)
        {
            Info = info;
            _stars = new List<Star>();
            _planets = new List<Planet>();
        }

        public void AddStar([NotNull] Star star)
        {
            _stars.Add(star ?? throw new ArgumentNullException());
        }

        public void AddPlanet([NotNull] Planet planet)
        {
            _planets.Add(planet ?? throw new ArgumentNullException());
        }

        public StarSystemImage TakeImage()
        {
            var entities = AllEntities;

            var systemAdapter = new StarSystemToStarSystemEntityAdapter(new InSystemPosition(),
                new InSystemRelation(0, 0, 0), 0, this);
            
            var root = new TreeNode<StarSystemEntity>(null, systemAdapter);
            
            var stack = new Stack<TreeNode<StarSystemEntity>>();
            stack.Push(root);
            
            while (stack.Count > 0)
            {
                var next = stack.Pop();
                var dependentEntities = FindOrbitDependentEntities(entities, 
                    in next.Value.Relation);

                foreach (var entity in dependentEntities)
                {
                    if (root.Find(x => x.Value == entity, root) == null)
                    {
                        var entityNode = new TreeNode<StarSystemEntity>(next, entity);
                        next.Add(entityNode);
                        stack.Push(entityNode);
                    }
                }
            }

            return new StarSystemImage(root);
        }

        public IEnumerable<StarSystemEntity> FindOrbitDependentEntities(StarSystemEntity[] entities,
            in InSystemRelation relation)
        {
            var id = relation.Id;
            return entities.Where(x => x.Relation.OrbitId == id);
        }
    }

    public class StarSystemToStarSystemEntityAdapter: StarSystemEntity
    {
        public readonly StarSystem StarSystem;

        public StarSystemToStarSystemEntityAdapter(InSystemPosition position, InSystemRelation relation, float radius,
            StarSystem starSystem)
            : base(position, relation, radius)
        {
            StarSystem = starSystem;
        }
    }

    public static class ListExtensions
    {
        public static TB[] WithArray<T,TU,TB>(this List<T> list,List<TU> anotherList)
            where T : TB
            where TU : TB
        {
            var array = new TB[list.Count + anotherList.Count];

            for (var i = 0; i < list.Count; i++)
            {
                array[i] = list[i];
            }

            for (var j = list.Count; j < anotherList.Count + list.Count; j++)
            {
                array[j] = anotherList[j - list.Count];
            }

            return array;
        }
    }
}