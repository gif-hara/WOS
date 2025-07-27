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
        private Actor actor;

        public Transform Transform => actor.transform;

        private GameObject SceneViewTree => actor.Document.Q("SceneView.Tree");

        private GameObject SceneViewStump => actor.Document.Q("SceneView.Stump");

        private Collider Trigger => actor.Document.Q<Collider>("Trigger");

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            SceneViewTree.SetActive(true);
            SceneViewStump.SetActive(false);
            Trigger
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
            Trigger
                .OnTriggerExitAsObservable()
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
                    collidedActor.InteractionController.RemoveInteraction(@this);
                })
                .RegisterTo(cancellationToken);
        }

        public async UniTask InteractAsync(Actor actor, CancellationToken cancellationToken)
        {
            Debug.Log("Begin interaction with Tree");
            await UniTask.Delay(1000, cancellationToken: cancellationToken); // Simulate some interaction delay
            SceneViewTree.SetActive(false);
            SceneViewStump.SetActive(true);
            Trigger.enabled = false;
            BeginRecoveryAsync(actor.destroyCancellationToken).Forget();
            Debug.Log("Interaction with Tree completed");
        }

        private async UniTask BeginRecoveryAsync(CancellationToken cancellationToken)
        {
            Debug.Log("Begin recovery from Tree interaction");
            await UniTask.Delay(3000, cancellationToken: cancellationToken); // Simulate recovery delay
            SceneViewTree.SetActive(true);
            SceneViewStump.SetActive(false);
            Trigger.enabled = true;
        }
    }
}
