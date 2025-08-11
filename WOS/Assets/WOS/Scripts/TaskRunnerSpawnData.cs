using System;
using System.Collections.Generic;
using TNRD;
using UnityEngine;
using WOS.ActorControllers;
using WOS.ActorControllers.TaskSystems;

namespace WOS
{
    [Serializable]
    public class TaskRunnerSpawnData
    {
        [field: SerializeField]
        private Actor taskRunnerPrefab;

        [field: SerializeField]
        private Transform spawnPoint;

        [field: SerializeField]
        private List<SerializableInterface<ITask>> tasks;
    }
}
