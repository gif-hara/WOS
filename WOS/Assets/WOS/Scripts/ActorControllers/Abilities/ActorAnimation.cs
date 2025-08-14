using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

namespace WOS.ActorControllers.Abilities
{
    public class ActorAnimation : IActorAbility
    {
        private Animator animator;

        private ObservableStateMachineTrigger[] stateMachineTriggers;

        private readonly int
            IdleHash = Animator.StringToHash("Idle"),
            AttackHash = Animator.StringToHash("Attack");


        public void Activate(Actor actor)
        {
            animator = actor.Document.Q<Animator>("Animator");
            stateMachineTriggers = animator.GetBehaviours<ObservableStateMachineTrigger>();
        }

        public void RequestAttack()
        {
            animator.SetTrigger(AttackHash);
        }

        public UniTask WaitForAttackAsync(CancellationToken cancellationToken)
        {
            return stateMachineTriggers.Select(x => x.OnStateExitAsObservable().Where(y => y.StateInfo.IsName("Attack")))
                .Merge()
                .FirstAsync(cancellationToken: cancellationToken).AsUniTask();
        }
    }
}
