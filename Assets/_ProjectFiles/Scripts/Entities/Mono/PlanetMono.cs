using Game.Entities.Factories;

namespace Game.Entities
{
    public class PlanetMono : SystemEntityMono
    {
        private Planet _planetRaw;
        
        public override SystemEntity SystemEntity => _planetRaw;

        public void Accept(Planet planetRaw)
        {
            _planetRaw = planetRaw;
            transform.position = planetRaw.Position.MultipliedDirection;
        }
    }
}