using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using WOS.ActorControllers.TaskSystems;

namespace WOS.ActorControllers.Brains
{
    public sealed class TaskRunner : IActorBrain
    {
        private readonly List<ITask> tasks = new();

        public TaskRunner(IEnumerable<ITask> tasks)
        {
            this.tasks.AddRange(tasks);
        }

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            RunTasksAsync(actor, cancellationToken).Forget();
        }

        private async UniTask RunTasksAsync(Actor actor, CancellationToken cancellationToken)
        {
            foreach (var task in tasks)
            {
                await task.RunAsync(actor, cancellationToken);
            }
        }
    }
}
