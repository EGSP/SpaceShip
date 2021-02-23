
namespace Game.Entities
{
    public struct StarSystemImage{
        public readonly TreeNode<SystemEntity> EntitiesRoot;

        public StarSystemImage(TreeNode<SystemEntity> entitiesRoot)
        {
            EntitiesRoot = entitiesRoot;
        }

        
        public StarSystemImage ResolveRelations()
        {
            TreeNode<SystemEntity>.ResolveRelations(EntitiesRoot);
            return this;
        }
    }
}