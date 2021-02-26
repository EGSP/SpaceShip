using System;
using System.Collections.Generic;
using System.Linq;
using Egsp.Extensions.Collections;
using JetBrains.Annotations;
using Sirenix.Serialization;
using UnityEngine;

namespace Game.Entities.Factories
{

    public class StarSystemObject
    {
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
        [OdinSerialize] public float DistanceFromParent { get; set; }

        

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

        public void JoinLine(LinkedList<StarSystemObject> line)
        {
            Line.Join(line, x => x.Parent = this);
            RecalculateArea();
        }

        private void RecalculateArea()
        {
            Area = this.GetSize() + Line.Sum(x => x.GetDistanceWithHalfSize());
        }

        public void AddLast(StarSystemObject systemObject)
        {
            systemObject.Parent = this;
            Line.AddLast(systemObject);
            RecalculateArea();
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
                lineChild.DistanceFromParent = distance + (lineChild.Area * 0.5f);
                root.AddLast(lineChild);
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
            obj.DistanceFromParent = distance + (obj.Area * 0.5f);
            return obj;
        }
    }

    public static class StarSystemObjectExtensions
    {
        public static float GetSize(this StarSystemObject systemObject)
        {
            return systemObject.Entity == null ? 0 : systemObject.Entity.Size;
        }

        public static float GetPosition(this StarSystemObject systemObject)
        {
            if (systemObject.Parent == null)
                return -1;

            return systemObject.Parent.GetPosition() 
                   + systemObject.Parent.GetSize() / 2f 
                   + GetDistanceWithHalfAreaSize(systemObject);
        }
        
        public static float GetDistanceWithHalfSize(this StarSystemObject systemObject)
        {
            return systemObject.DistanceFromParent + (systemObject.GetSize() / 2f);
        }

        public static float GetDistanceWithHalfAreaSize(this StarSystemObject systemObject)
        {
            return systemObject.DistanceFromParent + (systemObject.Area / 2f);
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