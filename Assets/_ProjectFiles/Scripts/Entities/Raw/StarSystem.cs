using System;
using System.Collections.Generic;
using System.Linq;
using Game.Entities.Factories;

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
        public readonly InGalaxyRelation GalaxyRelation;

        public readonly StarSystemObject RootSystemObject;

        public StarSystem()
        {
            RootSystemObject = StarSystemObject.NewInvalid;
        }

        public StarSystem(InGalaxyRelation galaxyRelation, StarSystemObject rootSystemObject)
        {
            GalaxyRelation = galaxyRelation;
            RootSystemObject = rootSystemObject;
        }

        public IEnumerable<SystemEntity> GetEntities()
        {
            return RootSystemObject.GetEntities();
        }

        [Obsolete]
        public IEnumerable<SystemEntity> FindOrbitDependentEntities(IEnumerable<SystemEntity> entities,
            in InSystemRelation relation)
        {
            var id = relation.Id;
            return entities.Where(x => x.Relation.OrbitId == id);
        }

        public override EntityType EntityType { get; }
    }

    // public class SystemToSystemEntityAdapter: SystemEntity
    // {
    //     public readonly StarSystem StarSystem;
    //
    //     public SystemToSystemEntityAdapter(StarSystem starSystem,float size, float rotation, float orbitRotation = 0,
    //         bool clockwise = false)
    //         : base(size, rotation, orbitRotation, clockwise)
    //     {
    //         StarSystem = starSystem;
    //     }
    // }

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