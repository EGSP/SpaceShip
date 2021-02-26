namespace Game.Entities
{
    public class Star: SystemEntity
    {
        public Star(float size, float rotation, float orbitRotation = 0, bool clockwise = false)
            : base(size, rotation, orbitRotation, clockwise)
        {
        }
    }
}