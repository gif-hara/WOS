using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WOS.ActorControllers;

namespace WOS
{
    public interface IInteraction
    {
        UniTask InteractAsync(Actor actor, CancellationToken cancellationToken);

        Transform Transform { get; }
    }
}
