using Cysharp.Threading.Tasks;

namespace WOS.ActorControllers.Abilities
{
    public class ActorStatus : IActorAbility
    {
        private Actor actor;

        private readonly int hitPointMax;

        private int hitPoint;

        public bool IsDead => hitPoint <= 0;

        public ActorStatus(int hitPoint)
        {
            hitPointMax = hitPoint;
            this.hitPoint = hitPointMax;
        }

        public void Activate(Actor actor)
        {
            this.actor = actor;
        }

        public void TakeDamage(int damage, Actor attackingActor)
        {
            if (hitPoint <= 0)
            {
                return;
            }

            hitPoint -= damage;
            actor.Router.PublishAsync(new ActorEvent.OnTakeDamage(damage)).AsUniTask().Forget();
            if (hitPoint <= 0)
            {
                hitPoint = 0;
                actor.Router.PublishAsync(new ActorEvent.OnDie(attackingActor)).AsUniTask().Forget();
            }
        }

        public void Revive()
        {
            hitPoint = hitPointMax;
        }
    }
}
