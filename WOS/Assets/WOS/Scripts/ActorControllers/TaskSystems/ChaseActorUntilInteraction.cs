using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class ChaseActorUntilInteraction : ITask
    {
        [field: SerializeField]
        private Actor targetActor;

        [field: SerializeField]
        private float chaseSpeed;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            var actorMovement = actor.GetAbility<ActorMovement>();
            var actorInteraction = actor.GetAbility<ActorInteraction>();
            while (!actorInteraction.ContainsInteraction(targetActor) && !cancellationToken.IsCancellationRequested)
            {
                Vector3 direction = (targetActor.transform.position - actor.transform.position).normalized;

                var velocity = direction * chaseSpeed * Time.deltaTime;
                actorMovement.Move(velocity);
                actorMovement.Rotate(Quaternion.LookRotation(direction));

                await UniTask.Yield(cancellationToken);
            }
        }
    }
}
