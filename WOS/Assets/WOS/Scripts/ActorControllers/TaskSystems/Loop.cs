using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using TNRD;
using UnityEngine;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class Loop : ITask
    {
        [field: SerializeField]
        private List<SerializableInterface<ITask>> tasks;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                foreach (var task in tasks)
                {
                    if (cancellationToken.IsCancellationRequested)
                    {
                        return;
                    }

                    await task.Value.RunAsync(actor, cancellationToken);
                }
            }
        }
    }
}
