using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class ChaseActor : ITask
    {
        [field: SerializeField]
        private Actor targetActor;

        [field: SerializeField]
        private float chaseSpeed;

        [field: SerializeField]
        private float stoppingDistance;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            var actorMovement = actor.GetAbility<ActorMovement>();
            while (Vector3.Distance(actor.transform.position, targetActor.transform.position) > stoppingDistance && !cancellationToken.IsCancellationRequested)
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
