using JetBrains.Annotations;
using UnityEngine;

namespace Game.Entities
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class SystemEntityMono : EntityMono
    {
        public MeshFilter MeshFilter { get; protected set; }
        public MeshRenderer MeshRenderer{ get; protected set; }
        
        [NotNull] public abstract SystemEntity SystemEntity { get; } 
        
        [CanBeNull] public SystemEntityMono Parent { get; private set; }

        protected virtual void Awake()
        {
            MeshFilter = GetComponent<MeshFilter>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }

        public void SetParent(SystemEntityMono parent)
        {
            Parent = parent;
            transform.SetParent(parent.transform, true);
        }
    }
}