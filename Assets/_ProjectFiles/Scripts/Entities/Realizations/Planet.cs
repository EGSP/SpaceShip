using Game.Entities.Planets;
using JetBrains.Annotations;

namespace Game.Entities
{
    public class Planet: SystemEntity
    {
        [NotNull] public readonly PlanetType Type;
        
        public Planet(PlanetType type, float size, float rotation, float orbitRotation = 0, bool clockwise = false)
            : base(size, rotation, orbitRotation, clockwise)
        {
            if (type == null)
                type = new SolidType.DesertType();
            
            Type = type;
        }
    }
}

namespace Game.Entities.Planets
{
    public abstract class PlanetType
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var type = obj as PlanetType;
            if (type == null)
                return false;

            return this.GetType() == type.GetType();
        }
    }

    public abstract class GasType : PlanetType
    {
        public class GasDwarfType : GasType{}
        public class GasGigantPlanetType : GasType{}
    }

    public abstract class SolidType : PlanetType
    {
        public class DesertType : SolidType{}
    }

    public abstract class LiquidType : PlanetType
    {
        public class OceanType : LiquidType{}
        public class LavaType : LiquidType{}
    }
}