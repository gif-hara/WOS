namespace WOS.ActorControllers.Abilities
{
    public class ActorStatus : IActorAbility
    {
        private Actor actor;

        private readonly int hitPointMax;

        private int hitPoint;

        public ActorStatus(int hitPoint)
        {
            hitPointMax = hitPoint;
            hitPoint = hitPointMax;
        }

        public void Activate(Actor actor)
        {
            this.actor = actor;
        }
    }
}
