using Sirenix.OdinInspector;

namespace Game.Entities
{
    public abstract class EntityMono<TEntity> : SerializedMonoBehaviour
        where TEntity : Entity
    {
        public virtual void Accept(TEntity rawObject)
        {
        }
    }
}