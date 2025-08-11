using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class FillInventory : ITask
    {
        [field: SerializeField]
        private Actor targetActor;

        [field: SerializeField]
        private string itemId;

        [field: SerializeField]
        private int needAmount;

        [field: SerializeField]
        private float takeInterval;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            var myInventory = new Inventory(actor.Document.Q<PlacementPoint>("PlacementPoint"));
            var targetInventory = targetActor.GetAbility<ActorInventory>();

            while (myInventory.GetItemCount(itemId) < needAmount && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(takeInterval), cancellationToken: cancellationToken);
                var index = targetInventory.Inventory.FindLastItemIndex(itemId);
                if (index == -1)
                {
                    continue;
                }

                var element = targetInventory.Inventory.RemoveItem(index);
                myInventory.AddItems(new List<Inventory.Element> { element });
            }
        }
    }
}
