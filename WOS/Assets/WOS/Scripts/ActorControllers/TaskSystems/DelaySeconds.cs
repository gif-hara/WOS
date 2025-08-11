using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class DelaySeconds : ITask
    {
        [field: SerializeField]
        private float waitTime;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: cancellationToken);
        }
    }
}
