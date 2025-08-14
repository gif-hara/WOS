using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HK;
using R3;
using UnityEngine;
using VitalRouter.R3;
using WOS.ActorControllers.Abilities;
using WOS.MasterDataSystem;

namespace WOS.ActorControllers.Brains
{
    [Serializable]
    public sealed class Tree : IActorBrain, IInteraction
    {
        [field: SerializeField]
        private int hitPoint;

        [field: SerializeField]
        public ItemDropData[] ItemDrops { get; private set; }

        private Actor actor;

        private ActorStatus actorStatus;

        public Transform Transform => actor.transform;

        private GameObject SceneViewTree => actor.Document.Q("SceneView.Tree");

        private GameObject SceneViewStump => actor.Document.Q("SceneView.Stump");

        private Collider Trigger => actor.Document.Q<Collider>("Trigger");

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            actorStatus = actor.AddAbility(new ActorStatus(hitPoint));
            SceneViewTree.SetActive(true);
            SceneViewStump.SetActive(false);
            this.SubscribeOnTrigger(actor, Trigger)
                .RegisterTo(cancellationToken);
            actor.Router.AsObservable<ActorEvent.OnDie>()
                .Subscribe(this, static (x, @this) =>
                {
                    @this.SceneViewTree.SetActive(false);
                    @this.SceneViewStump.SetActive(true);
                    @this.BeginRecoveryAsync(@this.actor.destroyCancellationToken).Forget();
                    var inventoryElements = new List<Inventory.Element>();
                    foreach (var itemDrop in @this.ItemDrops)
                    {
                        if (UnityEngine.Random.value < itemDrop.Probability)
                        {
                            var itemSpec = TinyServiceLocator.Resolve<MasterData>().ItemSpecs.Get(itemDrop.ItemId);
                            for (var i = 0; i < itemDrop.Amount; i++)
                            {
                                var itemObject = UnityEngine.Object.Instantiate(itemSpec.ItemPrefab, @this.actor.transform.position, Quaternion.identity);
                                inventoryElements.Add(new Inventory.Element(itemSpec, itemObject));
                            }
                        }
                    }
                    x.AttackingActor.GetAbility<ActorInventory>().Inventory.AddItems(inventoryElements);
                });
        }

        public async UniTask InteractAsync(Actor interactedActor, CancellationToken cancellationToken)
        {
            if (interactedActor.TryGetAbility<ActorAttack>(out var actorAttack))
            {
                actorAttack.AddTarget(actor);
                await UniTask.WaitUntilCanceled(cancellationToken);
                actorAttack.RemoveTarget(actor);
            }
        }

        private async UniTask BeginRecoveryAsync(CancellationToken cancellationToken)
        {
            await UniTask.Delay(3000, cancellationToken: cancellationToken);
            SceneViewTree.SetActive(true);
            SceneViewStump.SetActive(false);
            actorStatus.Revive();
        }
    }
}
