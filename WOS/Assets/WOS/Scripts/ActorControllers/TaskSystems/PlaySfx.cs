using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HK;
using UnityEngine;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class PlaySfx : ITask
    {
        [field: SerializeField]
        private string sfxName;

        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            TinyServiceLocator.Resolve<AudioManager>().PlaySfx(sfxName);
            return UniTask.CompletedTask;
        }
    }
}
