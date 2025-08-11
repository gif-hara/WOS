using System.Collections.Generic;
using System.Linq;
using TNRD;
using UnityEngine;
using WOS.ActorControllers;
using WOS.ActorControllers.Abilities;
using WOS.ActorControllers.Brains;
using WOS.ActorControllers.TaskSystems;

namespace WOS
{
    public class TaskRunnerSpawner : MonoBehaviour
    {
        [field: SerializeField]
        private Actor taskRunnerPrefab;

        [field: SerializeField]
        private Transform spawnPoint;

        [field: SerializeField]
        private List<SerializableInterface<ITask>> tasks;

        void Start()
        {
            SpawnTaskRunner();
        }

        private void SpawnTaskRunner()
        {
            var taskRunner = Instantiate(taskRunnerPrefab, spawnPoint.position, spawnPoint.rotation);
            taskRunner.AddAbility<ActorBrain>().Change(new TaskRunner(tasks.Select(x => x.Value)));
        }
    }
}
