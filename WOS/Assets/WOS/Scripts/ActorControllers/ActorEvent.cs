using VitalRouter;

namespace WOS.ActorControllers
{
    public class ActorEvent
    {
        public readonly struct OnAttack : ICommand
        {
        }

        public readonly struct OnTakeDamage : ICommand
        {
            public int Damage { get; }

            public OnTakeDamage(int damage)
            {
                Damage = damage;
            }
        }

        public readonly struct OnDie : ICommand
        {
            public Actor AttackingActor { get; }

            public OnDie(Actor attackingActor)
            {
                AttackingActor = attackingActor;
            }
        }
    }
}
