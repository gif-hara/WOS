using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using VitalRouter.R3;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class WaitUntilTargetActorDie : ITask
    {
        [field: SerializeField]
        private Actor target;

        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            return target.Router.AsObservable<ActorEvent.OnDie>().FirstAsync(cancellationToken: cancellationToken).AsUniTask();
        }
    }
}
