using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class ProgressColumn : ITask
    {
        [field: SerializeField]
        private Column column;

        [field: SerializeField]
        private float moveSpeed;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            var oldColumnIndex = -1;
            var columnIndex = column.GetLastNotFilledIndex();
            while (columnIndex >= 0 && !cancellationToken.IsCancellationRequested)
            {
                while (column.IsFilled(columnIndex))
                {
                    await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken);
                }
                var target = column.Get(columnIndex);
                if (target == null)
                {
                    Debug.LogError($"Target at index {columnIndex} is null in Column {column.name}");
                    return;
                }

                if (oldColumnIndex != -1)
                {
                    column.SetFilled(oldColumnIndex, false);
                }
                column.SetFilled(columnIndex, true);

                actor.transform.rotation = Quaternion.LookRotation(target.position - actor.transform.position);

                await LMotion.Create(actor.transform.position, target.position, Vector3.Distance(actor.transform.position, target.position) / moveSpeed)
                    .BindToPosition(actor.transform)
                    .ToUniTask(cancellationToken: cancellationToken);

                oldColumnIndex = columnIndex;
                columnIndex--;
            }
        }
    }
}
