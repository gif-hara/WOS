using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using HK;
using R3;
using R3.Triggers;
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
        public ItemDropData[] ItemDrops { get; private set; }

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
            this.BeginObserveInteraction(actor, Trigger)
                .RegisterTo(cancellationToken);
        }

        public async UniTask InteractAsync(Actor actor, CancellationToken cancellationToken)
        {
            actor.GetAbility<ActorAnimation>().RequestAttack();
            await actor.Router.AsObservable<ActorEvent.OnAttack>().FirstAsync(cancellationToken: cancellationToken);
            SceneViewTree.SetActive(false);
            SceneViewStump.SetActive(true);
            Trigger.enabled = false;
            BeginRecoveryAsync(actor.destroyCancellationToken).Forget();
            var inventoryElements = new List<Inventory.Element>();
            foreach (var itemDrop in ItemDrops)
            {
                if (UnityEngine.Random.value < itemDrop.Probability)
                {
                    var itemSpec = TinyServiceLocator.Resolve<MasterData>().ItemSpecs.Get(itemDrop.ItemId);
                    for (var i = 0; i < itemDrop.Amount; i++)
                    {
                        var itemObject = UnityEngine.Object.Instantiate(itemSpec.ItemPrefab, this.actor.transform.position, Quaternion.identity);
                        inventoryElements.Add(new Inventory.Element(itemSpec, itemObject));
                    }
                }
            }
            actor.GetAbility<ActorInventory>().AddItems(inventoryElements);
        }

        private async UniTask BeginRecoveryAsync(CancellationToken cancellationToken)
        {
            await UniTask.Delay(3000, cancellationToken: cancellationToken);
            SceneViewTree.SetActive(true);
            SceneViewStump.SetActive(false);
            Trigger.enabled = true;
        }
    }
}
