using System;
using System.Collections.Generic;
using Egsp.Extensions.Primitives;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Entities.Factories
{
    public abstract class EntityMonoFactory<TEntity, TEntityMono>
        where TEntity : Entity
        where TEntityMono : EntityMono
    {
        protected GameObject Prefab;
        protected AsyncOperationHandle<GameObject> Handle;
        
        protected abstract string PathToPrefab { get; }

        protected EntityMonoFactory()
        {
            LoadPrefab(null);
        }
        
        protected void LoadPrefab(Action callback)
        {
            Handle = Addressables.LoadAssetAsync<GameObject>(PathToPrefab);
            Handle.Completed += (handle) => Prefab = handle.Result;
        }

        public void SetColor(Mesh mesh, Color color)
        {
            var verticies = mesh.vertexCount;
            var colors = new Color[verticies];

            for (var i = 0; i < verticies; i++)
            {
                colors[i] = color;
            }

            mesh.colors = colors;
        }
        
        public virtual Color GetColor(TEntity systemEntity) => ColorExtensions.Random();
    }

    public abstract class ColorFactory
    {
        protected abstract Type EntityType { get; }
        
        protected IEnumerable<Color> Colorset = Array.Empty<Color>();
        
        protected ColorFactory()
        {
            
        }
    }
    
    public class ColorFactory<TEntity> : ColorFactory
    {
        protected override Type EntityType => typeof(TEntity);

        public ColorFactory(IEnumerable<Color> colorset)
        {
            Colorset = colorset;
        }
    }
}