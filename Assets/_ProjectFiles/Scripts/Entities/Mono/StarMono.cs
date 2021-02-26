namespace Game.Entities
{
    public class StarMono : SystemEntityMono
    {
        private Star _star;

        public override SystemEntity SystemEntity => _star;

        public void Accept(Star star)
        {
            _star = star;
            transform.position = star.Position.MultipliedDirection;
        }
    }
}