using System;
using System.Collections.Generic;
using System.Linq;
using Egsp.Extensions.Linq;
using Egsp.Extensions.Primitives;
using Game.Entities;
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

        protected abstract IEnumerable<ColorFactory> AddColorFactories();

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
    }
}