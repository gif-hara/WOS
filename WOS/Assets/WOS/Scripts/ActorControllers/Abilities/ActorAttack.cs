using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VitalRouter.R3;

namespace WOS.ActorControllers.Abilities
{
    public sealed class ActorAttack : IActorAbility
    {
        private int power;

        private readonly List<Actor> targets = new();

        private Actor currentTarget;

        private Actor actor;

        private ActorAnimation actorAnimation;

        public ActorAttack(int power)
        {
            this.power = power;
        }

        public void Activate(Actor actor)
        {
            this.actor = actor;
            actorAnimation = actor.GetAbility<ActorAnimation>();
            AttackAsync(actor.destroyCancellationToken).Forget();
        }

        public void AddTarget(Actor target)
        {
            if (target == null || targets.Contains(target))
            {
                return;
            }

            targets.Add(target);
            if (currentTarget == null)
            {
                currentTarget = target;
            }
        }

        public void RemoveTarget(Actor target)
        {
            if (target == null || !targets.Contains(target))
            {
                return;
            }

            targets.Remove(target);
            if (currentTarget == target)
            {
                currentTarget = targets.Count > 0 ? targets[0] : null;
            }
        }

        private async UniTask AttackAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.WaitUntil(this, static @this => @this.currentTarget != null && !@this.currentTarget.GetAbility<ActorStatus>().IsDead, cancellationToken: cancellationToken);
                actorAnimation.RequestAttack();
                await actor.Router.AsObservable<ActorEvent.OnAttack>().FirstAsync(cancellationToken: cancellationToken);
                if (currentTarget != null)
                {
                    var currentTargetStatus = currentTarget.GetAbility<ActorStatus>();
                    if (!currentTargetStatus.IsDead)
                    {
                        currentTargetStatus.TakeDamage(power, actor);
                    }
                }
                await actorAnimation.WaitForAttackAsync(cancellationToken);
            }
        }
    }
}
