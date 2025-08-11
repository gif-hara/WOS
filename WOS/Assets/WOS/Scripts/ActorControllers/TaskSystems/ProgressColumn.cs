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
            var columnIndex = column.GetLastNotFilledIndex();
            while (columnIndex >= 0 || !cancellationToken.IsCancellationRequested)
            {
                var target = column.Get(columnIndex);
                if (target == null)
                {
                    Debug.LogError($"Target at index {columnIndex} is null in Column {column.name}");
                    return;
                }

                column.SetFilled(columnIndex, true);

                await LMotion.Create(actor.transform.position, target.position, Vector3.Distance(actor.transform.position, target.position) / moveSpeed)
                    .BindToPosition(actor.transform)
                    .ToUniTask(cancellationToken: cancellationToken);

                columnIndex = column.GetLastNotFilledIndex();
            }
        }
    }
}
