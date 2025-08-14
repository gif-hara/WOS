using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WOS.ReactionSystems
{
    public sealed class PlayParticleSystem : IReaction
    {
        [field: SerializeField]
        private ParticleSystem target;

        public UniTask InvokeAsync(CancellationToken cancellationToken)
        {
            target.Play(true);
            return UniTask.CompletedTask;
        }
    }
}
