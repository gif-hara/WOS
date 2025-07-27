using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using R3.Triggers;
using UnityEngine;

namespace WOS.ActorControllers.Brains
{
    [Serializable]
    public sealed class Tree : IActorBrain, IInteraction
    {
        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            Debug.Log("Tree brain activated");

            actor.Document.Q<Collider>("Trigger")
                .OnTriggerEnterAsObservable()
                .Subscribe((this, actor), static (x, t) =>
                {
                    var (@this, actor) = t;
                    if (x.attachedRigidbody == null)
                    {
                        return;
                    }
                    if (!x.attachedRigidbody.TryGetComponent<Actor>(out var collidedActor))
                    {
                        return;
                    }
                    collidedActor.InteractionController.AddInteraction(@this);
                })
                .RegisterTo(cancellationToken);
        }

        public async UniTask InteractAsync(Actor actor, CancellationToken cancellationToken)
        {
            Debug.Log("Begin interaction with Tree");
            await UniTask.Delay(1000, cancellationToken: cancellationToken); // Simulate some interaction delay
            Debug.Log("Interaction with Tree completed");
        }
    }
}
