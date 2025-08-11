using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class DestroyActor : ITask
    {
        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            Object.Destroy(actor.gameObject);
            return UniTask.CompletedTask;
        }
    }
}
