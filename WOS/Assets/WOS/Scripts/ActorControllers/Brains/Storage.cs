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
        private string requireItemId;

        private Actor actor;

        public Actor Actor => actor;

        private Collider Trigger => actor.Document.Q<Collider>("Trigger");

        private ActorInventory actorInventory;

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            actorInventory = actor.AddAbility<ActorInventory>();
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

                var index = interactedActorInventory.Inventory.FindLastItemIndex(requireItemId);
                if (index == -1)
                {
                    continue;
                }

                var element = interactedActorInventory.Inventory.RemoveItem(index);
                actorInventory.Inventory.AddItems(new List<Inventory.Element> { element });
            }
        }
    }
}
