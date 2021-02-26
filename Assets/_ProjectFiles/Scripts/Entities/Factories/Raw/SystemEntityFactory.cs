namespace Game.Entities.Factories
{
    public abstract class SystemEntityFactory<TEntity> where TEntity: SystemEntity
    {
        public abstract TEntity CreateEntity(int treeLevel, in TreeGenerationProperties properties);
    }
}