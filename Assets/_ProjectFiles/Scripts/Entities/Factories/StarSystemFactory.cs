using System.Collections.Generic;
using Game;
using Game.Entities;
using UnityEngine;

namespace Game.Entities.Factories
{

    public static class StarSystemFactory
    {
        public static StarSystem CreateRandomStarSystem()
        {
            var starSystem = new StarSystem(new InGalaxyRelation(0));

            return starSystem;
        }

        public static StarSystemObject GenerateRandomSystem()
        {
            var root = new StarSystemObject(0, null);
            var generationProperties = new TreeGenerationProperties(
                new MinMaxFloat(3, 8), new MinMaxFloat(5, 8), new MinMaxFloat(0.5f, 1f),
                new MinMaxFloat(1, 3), new MinMaxFloat(1, 4), new MinMaxFloat(1, 2.5f),
                new MinMaxFloat(0.1f, 0.5f));
            root.SetLine(GenerateTree(root, in generationProperties),false);

            return root;
        }

        private static LinkedList<StarSystemObject> GenerateTree(StarSystemObject root, in TreeGenerationProperties
            properties)
        {
            var list = new LinkedList<StarSystemObject>();
            list.AddLast(root);

            var entitiesCount = (int)properties.EntitiesCount.Random;
            while (list.Count < entitiesCount + 1)
            {
                var size = properties.Size.Random;
                var obj = new StarSystemObject(size, list.Last.Value);

                var distance = (int)properties.Distance.Random;
                
                var innerTrees = (int)properties.InnerTrees.Random;
                if (innerTrees > 0)
                {
                    var innerEntitiesMax = (int)properties.InnerEntitiesCount.Random;
                    var line =
                        GenerateInnerTree(obj, in properties, innerEntitiesMax, innerTrees);

                    obj.SetLine(line);
                }

                // Дистанция похожа на "чупик" (палка и круг).
                obj.Distance = distance + (obj.AreaSize * 0.5f);
                list.AddLast(obj);
            }

            // Убираем себя из своего же листа.
            list.RemoveFirst();

            return list;
        }

        private static LinkedList<StarSystemObject> GenerateInnerTree(StarSystemObject root,
            in TreeGenerationProperties properties, int innerEntitiesMax,int innerTreesMax)
        {
            var list = new LinkedList<StarSystemObject>();
            list.AddLast(root);
            
            var entitiesCount = Random.Range(0, innerEntitiesMax);
            while (list.Count < entitiesCount + 1)
            {
                var size = properties.InnerSize.Random;
                var obj = new StarSystemObject(size, list.Last.Value);
                
                var distance = (int)properties.InnerDistance.Random;

                if (innerTreesMax != 0)
                {
                    var innerTrees = Random.Range(0, innerTreesMax);
                    if (innerTrees > 0)
                    {
                        var line =
                            GenerateInnerTree(obj, in properties, 
                                innerEntitiesMax-1,innerTreesMax - 1);

                        obj.SetLine(line);
                    }
                }

                // Дистанция похожа на "чупик" (палка и круг).
                obj.Distance = distance + (obj.AreaSize * 0.5f);

                list.AddLast(obj);
            }

            // Убираем себя из своего же листа.
            list.RemoveFirst();

            return list;
        }

        public readonly struct TreeGenerationProperties
        {
            public readonly MinMaxFloat EntitiesCount;
            public readonly MinMaxFloat Distance;
            public readonly MinMaxFloat Size;
            
            public readonly MinMaxFloat InnerTrees;
            public readonly MinMaxFloat InnerEntitiesCount;
            public readonly MinMaxFloat InnerDistance;
            public readonly MinMaxFloat InnerSize;

            public TreeGenerationProperties(MinMaxFloat entitiesCount, MinMaxFloat distance, MinMaxFloat size,
                MinMaxFloat innerTrees, MinMaxFloat innerEntitiesCount, MinMaxFloat innerDistance, MinMaxFloat innerSize)
            {
                EntitiesCount = entitiesCount;
                Distance = distance;
                InnerTrees = innerTrees;
                InnerEntitiesCount = innerEntitiesCount;
                InnerDistance = innerDistance;
                InnerSize = innerSize;
                Size = size;
            }
        }
    }


    public static class StarFactory
    {
        private static StarCreationPropeties _defaultProperties;

        public static StarCreationPropeties Propeties { get; set; }

        static StarFactory()
        {
            _defaultProperties = new StarCreationPropeties(new MinMaxFloat(5, 10),
                new MinMaxFloat(1, 5));

            Propeties = _defaultProperties;
        }
    }


    public class StarCreationPropeties
    {
        public readonly MinMaxFloat Size;
        public readonly MinMaxFloat Rotation;

        public StarCreationPropeties(MinMaxFloat size, MinMaxFloat rotation)
        {
            Size = size;
            Rotation = rotation;
        }
    }

    public readonly struct MinMaxFloat
    {
        public readonly float Min;
        public readonly float Max;

        public float Random => UnityEngine.Random.Range(Min, Max);

        public MinMaxFloat(float min, float max)
        {
            Min = Mathf.Abs(min);
            Max = Mathf.Abs(max);

            if ((Max - Min) < float.Epsilon)
                Max += 1f;
        }
    }
}