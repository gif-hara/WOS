using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TNRD;
using UnityEngine;
using WOS.Common.Conditions;

namespace WOS
{
    public class TaskRunnerSpawnInterval : MonoBehaviour
    {
        [field: SerializeField]
        private TaskRunnerSpawnData spawnData;

        [field: SerializeField]
        private float spawnInterval;

        [field: SerializeField]
        private SerializableInterface<ICondition> condition;

        void Start()
        {
            SpawnInterval(destroyCancellationToken).Forget();
        }

        private async UniTask SpawnInterval(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(spawnInterval), cancellationToken: cancellationToken);
                if (condition == null || condition.Value.Evaluate())
                {
                    spawnData.Spawn();
                }
            }
        }
    }
}
