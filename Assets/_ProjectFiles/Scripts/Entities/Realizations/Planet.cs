namespace Game.Entities
{
    public class Planet: StarSystemEntity
    {
        public Planet(InSystemPosition position, InSystemRelation relation, float radius)
            : base(position, relation, radius)
        {
        }
    }
}