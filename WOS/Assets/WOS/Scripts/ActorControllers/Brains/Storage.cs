using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

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

        public UniTask InteractAsync(Actor interactedActor, CancellationToken cancellationToken)
        {
            Debug.Log($"Storage Interact: {interactedActor.name}");
            return UniTask.CompletedTask;
        }
    }
}
