using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WOS.ReactionSystems
{
    public sealed class PlayParticleSystem : IReaction
    {
        [field: SerializeField]
        private ParticleSystem target;

        [field: SerializeField]
        private bool withChildren = true;

        public UniTask InvokeAsync(CancellationToken cancellationToken)
        {
            target.Play(withChildren);
            return UniTask.CompletedTask;
        }
    }
}
