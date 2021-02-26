using System;
using System.Collections.Generic;
using System.Linq;
using Egsp.Extensions.Collections;
using Egsp.Extensions.Primitives;
using JetBrains.Annotations;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Entities.Factories
{

    public class StarSystemObject
    {
        public static StarSystemObject NewInvalid => new StarSystemObject(-1, null);
        
        public readonly int Id;
        
        [CanBeNull] public readonly SystemEntity Entity;

        [CanBeNull] public StarSystemObject Parent
        {
            get => _parent;
            set
            {
                if (value == this)
                    value = null;
                
                _parent = value;
                ResetEntityRelation();
            }
        }
        [CanBeNull]
        [OdinSerialize] private StarSystemObject _parent;

        [OdinSerialize] public LinkedList<StarSystemObject> Line { get; private set; }

        /// <summary>
        /// Область - это собственный размер + длина основной линии спутников.
        /// </summary>
        [OdinSerialize] public float Area { get; private set; }
        [OdinSerialize] public float DistanceFromPrevious { get; private set; }
        
        [OdinSerialize] public float Position { get; private set; }

        public bool IsValid => Id != -1;

        

        public StarSystemObject(int id, [CanBeNull] SystemEntity entity) : this()
        {
            Entity = entity;
            Area = this.GetSize();
            Id = id;
            ResetEntityRelation();
        }

        private StarSystemObject()
        {
            Line = new LinkedList<StarSystemObject>();
        }

        private void ResetEntityRelation()
        {
            if (Entity == null)
                return;

            Entity.Relation = new InSystemRelation(Id, Parent?.Id ?? 0);
        }

        private float RecalculateArea()
        {
            if (Line.Count == 0)
            {
                Area = this.GetSize();
            }

            Area = this.GetSize() + Line.Sum(x => x.Area + x.DistanceFromPrevious);
            return Area;
        }

        private float RecalculateAllAreas()
        {
            if (Line.Count == 0)
            {
                Area = this.GetSize();
            }
            
            Area = this.GetSize() + Line.Sum(x => RecalculateArea());
            return Area;
        }

        public void AddChild(StarSystemObject systemObject)
        {
            systemObject.Parent = this;
            systemObject.DistanceFromPrevious += systemObject.Area * 0.5f;
            Line.AddLast(systemObject);
            RecalculateArea();
        }
        
        public void JoinLine(LinkedList<StarSystemObject> line)
        {
            Line.Join(line, x => x.Parent = this);
            RecalculateArea();
        }

        public static IEnumerable<StarSystemObject> QueueTraverse(StarSystemObject root)
        {
            var queue = new Queue<StarSystemObject>();
            queue.Enqueue(root);
            
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                yield return next;
                foreach (var child in next.Line)
                    queue.Enqueue(child);
            } 
        }
        
        public static void QueueTraverse(StarSystemObject root, Action<StarSystemObject> action)
        {
            foreach (var systemObject in QueueTraverse(root))
            {
                action(systemObject);
            } 
        }
        
        public static void CalculatePositions(StarSystemObject root, ref StarSystemObject previous)
        {
            if (!root.IsValid)
                return;
            
            // Предыдущим объектом может быть и чей-то спутник.
            if (previous.IsValid)
            {
                // Считываем свою позицию
                root.Position = previous.Position + root.DistanceFromPrevious + root.Area * 0.5f;
                
                // Debug.Log($"Prevpos:{previous?.Position.ToString(1)}; MyPos:{root.Position.ToString(1)};" +
                //           $" MyHalfArea:{(root.Area*0.5f).ToString(1)}" +
                //           $" Dist:{root.DistanceFromPrevious.ToString(1)}");
            }
            else
            {
                root.Position = 0;
            }

            if (root.Entity != null)
                root.Entity.Position = new InSystemPosition(root.Position);
            
            previous = root;
            

            if (root.Line.Count == 0)
                return;
            
            foreach (var child in root.Line)
            {
                CalculatePositions(child, ref previous);
            }
        }
        
        public static IEnumerable<SystemEntity> QueueEntityTraverse(StarSystemObject root)
        {
            var queue = new Queue<StarSystemObject>();
            queue.Enqueue(root);
            
            while (queue.Count > 0)
            {
                var next = queue.Dequeue();
                yield return next.Entity;
                foreach (var child in next.Line)
                    queue.Enqueue(child);
            } 
        }

        public static StarSystemObject GenerateTree<TEntity>(
            StarSystemObject root, in TreeGenerationProperties properties,
            SystemEntityFactory<TEntity> factory, ref int id, int level = 0)
            where TEntity : SystemEntity
        {
            var levelFactor = properties.Factor(level);
            var entitiesCount = (int)(properties.EntitiesCount.Random * levelFactor);
            
            while (entitiesCount > 0)
            {
                entitiesCount--;
                id++;
                
                var systemEntity = factory.CreateEntity(level, in properties);
                var lineChild = new StarSystemObject(id, systemEntity);

                var distance = Mathf.Clamp(properties.Distance.Random * levelFactor,
                    properties.Distance.Min, properties.Distance.Max);
                
                if (level < properties.MaxTreeLevel)
                {
                    // Сюда можно добавить еще один рандомайзер на генерацию.
                    // Хотя внутри может и так ничего не сгенерироваться.
                    
                    GenerateTree(lineChild, in properties, factory, ref id, level + 1);
                }

                // Дистанция похожа на "чупик" (палка и круг).
                lineChild.DistanceFromPrevious = distance + (lineChild.Area * 0.5f);
                root.AddChild(lineChild);
            }

            return root;
        }

        public static StarSystemObject GenerateOne<TEntity>(in TreeGenerationProperties properties,
            SystemEntityFactory<TEntity> factory, ref int id) where TEntity : SystemEntity
        {
            id++;
            var level = 0;
            var levelFactor = properties.Factor(level);

            var systemEntity = factory.CreateEntity(level, in properties);
            var obj = new StarSystemObject(id, systemEntity);

            var distance = properties.Distance.Random * levelFactor;

            // Дистанция похожа на "чупик" (палка и круг).
            obj.DistanceFromPrevious = distance;
            return obj;
        }
    }

    public static class StarSystemObjectExtensions
    {
        public static float GetSize(this StarSystemObject systemObject)
        {
            return systemObject.Entity == null ? 0 : systemObject.Entity.Size;
        }

        public static float GetDistanceWithHalfSize(this StarSystemObject systemObject)
        {
            return systemObject.DistanceFromPrevious + (systemObject.GetSize() / 2f);
        }

        public static float GetDistanceWithHalfAreaSize(this StarSystemObject systemObject)
        {
            return systemObject.DistanceFromPrevious + (systemObject.Area / 2f);
        }

        public static IEnumerable<SystemEntity> GetEntities(this StarSystemObject systemObject)
        {
            if (systemObject.IsValid)
                return StarSystemObject.QueueEntityTraverse(systemObject);
            
            return Array.Empty<SystemEntity>();
        }

        public static byte[] Serialize(this StarSystemObject systemObject, DataFormat format = DataFormat.JSON)
        {
            var json = SerializationUtility.SerializeValue(systemObject, format);
            return json;
        }

        public static StarSystemObject Deserialize(byte[] data, DataFormat format = DataFormat.JSON)
        {
            var obj = SerializationUtility.DeserializeValue<StarSystemObject>(data, format);
            return obj;
        }
    }
}