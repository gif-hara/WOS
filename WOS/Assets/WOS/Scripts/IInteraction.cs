using System.Threading;
using Cysharp.Threading.Tasks;
using WOS.ActorControllers;

namespace WOS
{
    public interface IInteraction
    {
        UniTask InteractAsync(Actor actor, CancellationToken cancellationToken);
    }
}
