using Game.Entities.Factories;

namespace Game.Entities
{
    public class PlanetMono : SystemEntityMono
    {
        public float opacity;
        private Planet _planetRaw;

        public void Accept(Planet planetRaw)
        {
            _planetRaw = planetRaw;
            transform.position = planetRaw.Position.MultipliedDirection;
        }

        public override SystemEntity SystemEntity => _planetRaw;
    }
}