using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion.Animation;
using UnityEngine;

namespace WOS.ReactionSystems
{
    public sealed class PlayLitMotionAnimation : IReaction
    {
        [field: SerializeField]
        private LitMotionAnimation target;

        public UniTask InvokeAsync(CancellationToken cancellationToken)
        {
            target.Restart();
            return UniTask.CompletedTask;
        }
    }
}
