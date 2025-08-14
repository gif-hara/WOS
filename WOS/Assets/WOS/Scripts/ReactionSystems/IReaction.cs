using System.Threading;
using Cysharp.Threading.Tasks;

namespace WOS.ReactionSystems
{
    public interface IReaction
    {
        UniTask InvokeAsync(CancellationToken cancellationToken);
    }
}
