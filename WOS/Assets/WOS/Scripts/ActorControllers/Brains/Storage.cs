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
        private Actor actor;

        public Transform Transform => actor.transform;

        private Collider Trigger => actor.Document.Q<Collider>("Trigger");

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            this.SubscribeOnTrigger(actor, Trigger)
                .RegisterTo(cancellationToken);
        }

        public UniTask InteractAsync(Actor actor, CancellationToken cancellationToken)
        {
            Debug.Log($"Storage Interact: {actor.name}");
            return UniTask.CompletedTask;
        }
    }
}
