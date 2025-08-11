using UnityEngine;

namespace WOS
{
    public class TaskRunnerSpawnInterval : MonoBehaviour
    {
        [field: SerializeField]
        private TaskRunnerSpawnData spawnData;

        [field: SerializeField]
        private float spawnInterval = 5f;
    }
}
