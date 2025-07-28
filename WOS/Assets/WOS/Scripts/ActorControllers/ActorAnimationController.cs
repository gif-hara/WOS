using UnityEngine;

namespace WOS.ActorControllers
{
    public class ActorAnimationController
    {
        private readonly Animator animator;

        private readonly int
            IdleHash = Animator.StringToHash("Idle"),
            AttackHash = Animator.StringToHash("Attack");


        public ActorAnimationController(Animator animator)
        {
            this.animator = animator;
        }

        public void RequestAttack()
        {
            animator.SetTrigger(AttackHash);
        }
    }
}
