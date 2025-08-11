using System;
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
    [Serializable]
    public class TaskRunnerSpawnData
    {
        [field: SerializeField]
        public Actor TaskRunnerPrefab { get; private set; }

        [field: SerializeField]
        public Transform SpawnPoint { get; private set; }

        [field: SerializeField]
        public List<SerializableInterface<ITask>> Tasks { get; private set; }

        public void Spawn()
        {
            var taskRunner = UnityEngine.Object.Instantiate(TaskRunnerPrefab, SpawnPoint.position, SpawnPoint.rotation);
            taskRunner.AddAbility<ActorBrain>().Change(new TaskRunner(Tasks.Select(x => x.Value)));
        }
    }
}
