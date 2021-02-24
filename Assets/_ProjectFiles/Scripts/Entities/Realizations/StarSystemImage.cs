
using Sirenix.Serialization;

namespace Game.Entities
{
    public partial struct StarSystemImage{
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
    
    public partial struct StarSystemImage
    {
        public static byte[] GetJson(StarSystemImage image)
        {
            return SerializationUtility.SerializeValue(image, DataFormat.JSON);
        }

        public static StarSystemImage GetImage(byte[] bytes, DataFormat format = DataFormat.JSON)
        {
            var image = SerializationUtility.DeserializeValue<StarSystemImage>(bytes, format)
                .ResolveRelations();
            return image;
        }
    }
}