using System.Threading;
using Cysharp.Threading.Tasks;

namespace WOS.ActorControllers.TaskSystems
{
    public interface ITask
    {
        UniTask RunAsync(Actor actor, CancellationToken cancellationToken);
    }
}
