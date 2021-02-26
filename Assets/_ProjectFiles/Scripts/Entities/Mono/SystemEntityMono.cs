using UnityEngine;

namespace Game.Entities
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class SystemEntityMono : EntityMono
    {
        public MeshFilter MeshFilter { get; protected set; }
        public MeshRenderer MeshRenderer{ get; protected set; }
        
        public abstract SystemEntity SystemEntity { get; } 

        protected virtual void Awake()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }
    }
}