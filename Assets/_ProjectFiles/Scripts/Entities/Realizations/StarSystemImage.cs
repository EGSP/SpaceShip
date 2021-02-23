
namespace Game.Entities
{
    public struct StarSystemImage{
        public readonly TreeNode<StarSystemEntity> EntitiesRoot;

        public StarSystemImage(TreeNode<StarSystemEntity> entitiesRoot)
        {
            EntitiesRoot = entitiesRoot;
        }

        
        public StarSystemImage ResolveRelations()
        {
            TreeNode<StarSystemEntity>.ResolveRelations(EntitiesRoot);
            return this;
        }
    }
}