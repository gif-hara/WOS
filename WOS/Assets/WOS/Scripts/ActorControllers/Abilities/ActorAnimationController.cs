using UnityEngine;

namespace WOS.ActorControllers.Abilities
{
    public class ActorAnimationController : IActorAbility
    {
        private Animator animator;

        private readonly int
            IdleHash = Animator.StringToHash("Idle"),
            AttackHash = Animator.StringToHash("Attack");


        public void Activate(Actor actor)
        {
            animator = actor.Document.Q<Animator>("Animator");
        }

        public void RequestAttack()
        {
            animator.SetTrigger(AttackHash);
        }
    }
}
