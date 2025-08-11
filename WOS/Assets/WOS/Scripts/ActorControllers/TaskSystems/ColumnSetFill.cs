using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class ColumnSetFill : ITask
    {
        [field: SerializeField]
        private Column column;

        [field: SerializeField]
        private int index;

        [field: SerializeField]
        private bool isFill;

        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            column.SetFilled(index, isFill);
            return UniTask.CompletedTask;
        }
    }
}
