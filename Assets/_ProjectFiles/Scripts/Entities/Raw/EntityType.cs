namespace Game.Entities
{
    public abstract class EntityType
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var type = obj as EntityType;
            if (type == null)
                return false;

            return this.GetType() == type.GetType();
        }
    }

    public abstract class SystemEntityType : EntityType{}
}