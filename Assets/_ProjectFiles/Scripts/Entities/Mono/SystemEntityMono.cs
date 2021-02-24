using UnityEngine;

namespace Game.Entities
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class SystemEntityMono<TEntity> : EntityMono<TEntity> where TEntity : SystemEntity
    {
        public MeshFilter MeshFilter { get; protected set; }
        public MeshRenderer MeshRenderer{ get; protected set; }

        protected virtual void Awake()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }
    }
}