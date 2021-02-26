using System;
using System.Collections.Generic;
using Egsp.Extensions.Linq;
using Egsp.Extensions.Primitives;
using UnityEngine;

namespace Game.Entities.Factories
{
    public abstract class ColorFactory
    {
        public abstract Type EntityType { get; }
        
        protected IEnumerable<Color> Colorset = Array.Empty<Color>();
        
        protected ColorFactory()
        {
        }

        public virtual bool CheckEntityType(EntityType entityType)
        {
            if (entityType.GetType().IsSubclassOf(EntityType))
                return true;
            
            return false;
        }

        public abstract Color GetColor();
    }

    public class ColorFactory<TEntityType> : ColorFactory where TEntityType : EntityType
    {
        public override Type EntityType => typeof(TEntityType);

        public ColorFactory(IEnumerable<Color> colorset = null)
        {
            Colorset = colorset;
        }
        
        public override Color GetColor()
        {
            if(Colorset == null)
                return Color.magenta;
            
            return Colorset.RandomBySeed();
        }
    }
    
    public class RandomColorFactory<TEntityType> : ColorFactory<TEntityType> where TEntityType : EntityType
    {
        public RandomColorFactory(IEnumerable<Color> colorset = null) : base(colorset)
        {
        }

        public override Color GetColor() => ColorExtensions.Random();
    }
}