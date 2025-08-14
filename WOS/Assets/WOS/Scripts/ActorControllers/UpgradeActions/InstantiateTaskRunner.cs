using UnityEngine;

namespace WOS.ActorControllers.UpgradeActions
{
    public class InstantiateTaskRunner : IUpgradeAction
    {
        [field: SerializeField]
        private TaskRunnerSpawnData spawnData;

        public void Execute(bool restoreFromSaveData)
        {
            spawnData.Spawn();
        }
    }
}
