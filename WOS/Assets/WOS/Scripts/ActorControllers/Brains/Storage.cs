using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.Brains
{
    [Serializable]
    public sealed class Storage : IActorBrain, IInteraction
    {
        [field: SerializeField]
        private PlacementPoint placementPoint;

        [field: SerializeField]
        private string requireItemId;

        private Actor actor;

        public Transform Transform => actor.transform;

        private Collider Trigger => actor.Document.Q<Collider>("Trigger");

        private Inventory inventory;

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            inventory = new Inventory(placementPoint);
            this.SubscribeOnTrigger(actor, Trigger)
                .RegisterTo(cancellationToken);
        }

        public async UniTask InteractAsync(Actor interactedActor, CancellationToken cancellationToken)
        {
            var interactedActorInventory = interactedActor.TryGetAbility<ActorInventory>(out var value) ? value : null;
            if (interactedActorInventory == null)
            {
                return;
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancellationToken);

                var index = interactedActorInventory.Inventory.FindLastItem(requireItemId);
                if (index == -1)
                {
                    continue;
                }

                var element = interactedActorInventory.Inventory.RemoveItem(index);
                inventory.AddItems(new List<Inventory.Element> { element });
            }
        }
    }
}
