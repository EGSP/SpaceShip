namespace Game.Entities
{
    public class Planet: SystemEntity
    {
        public Planet(float size, float rotation, float orbitRotation = 0, bool clockwise = false)
            : base(size, rotation, orbitRotation, clockwise)
        {
        }
    }
}