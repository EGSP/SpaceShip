using System;
using Egsp.Extensions.Primitives;
using Egsp.Utils.MeshUtilities;
using UnityEngine;

namespace Game.Entities.Factories
{
    public abstract class SystemEntityMonoFactory<TSystemEntity, TSystemEntityMono> : 
        EntityMonoFactory<TSystemEntity, TSystemEntityMono>
        where TSystemEntity : SystemEntity
        where TSystemEntityMono : SystemEntityMono
    {
        public Color[] Colorset { get; set; } = Array.Empty<Color>(); 
        
        public void InstancePrefab(TSystemEntity systemEntity, Action<TSystemEntityMono> callback)
        {
            if (Prefab == null)
            {
                if (!Handle.IsValid())
                {
                    LoadPrefab(() => InstancePrefabInternal(systemEntity, callback));
                }
                else
                {
                    Handle.Completed += (handle) => InstancePrefabInternal(systemEntity, callback);
                }
                return;
            }

            InstancePrefabInternal(systemEntity,callback);
        }
        
        protected void InstancePrefabInternal(TSystemEntity rawObject, Action<TSystemEntityMono> callback)
        {
            callback?.Invoke(InstancePrefabImmediately(rawObject));
        }

        public TSystemEntityMono InstancePrefabImmediately(TSystemEntity systemEntity)
        {
            var mesh = CreateMesh(systemEntity);
            SetColor(mesh, GetColor(systemEntity));
            var inst = UnityEngine.Object.Instantiate(Prefab).GetComponent<TSystemEntityMono>();
            
            inst.MeshFilter.mesh = mesh;
            inst.MeshRenderer.material = GetMaterial();

            AcceptRawObject(inst, systemEntity);
            return inst;
        }
        
        public abstract void AcceptRawObject(TSystemEntityMono systemEntityMono, TSystemEntity systemEntity);

        protected virtual Mesh CreateMesh(TSystemEntity systemEntity)
        {
            var mesh = MeshUtils.Diamond(systemEntity.Size, systemEntity.Size);
            return mesh;
        }

        protected virtual Material GetMaterial()
        {
            return MeshUtils.GetSpriteDefaultMaterial();
        }
    }
}