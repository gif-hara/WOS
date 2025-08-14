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
        private float reviveDelaySeconds;

        [field: SerializeField]
        public ItemDropData[] ItemDrops { get; private set; }

        [field: SerializeField]
        private GameObject sceneViewTree;

        [field: SerializeField]
        private GameObject sceneViewStump;

        [field: SerializeField]
        private Collider interactionTrigger;

        private Actor actor;

        private ActorStatus actorStatus;

        public Actor Actor => actor;

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            actorStatus = actor.AddAbility(new ActorStatus(hitPoint));
            sceneViewTree.SetActive(true);
            sceneViewStump.SetActive(false);
            this.SubscribeOnTrigger(actor, interactionTrigger)
                .RegisterTo(cancellationToken);
            ProcessDieAsync(cancellationToken).Forget();
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

        private async UniTask ProcessDieAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var onDie = await actor.Router.AsObservable<ActorEvent.OnDie>().FirstAsync(cancellationToken: cancellationToken);
                sceneViewTree.SetActive(false);
                sceneViewStump.SetActive(true);
                var inventoryElements = new List<Inventory.Element>();
                foreach (var itemDrop in ItemDrops)
                {
                    if (UnityEngine.Random.value < itemDrop.Probability)
                    {
                        var itemSpec = TinyServiceLocator.Resolve<MasterData>().ItemSpecs.Get(itemDrop.ItemId);
                        for (var i = 0; i < itemDrop.Amount; i++)
                        {
                            var itemObject = UnityEngine.Object.Instantiate(itemSpec.ItemPrefab, actor.transform.position, Quaternion.identity);
                            inventoryElements.Add(new Inventory.Element(itemSpec, itemObject));
                        }
                    }
                }
                onDie.AttackingActor.GetAbility<ActorInventory>().Inventory.AddItems(inventoryElements);
                await UniTask.Delay(TimeSpan.FromSeconds(reviveDelaySeconds), cancellationToken: cancellationToken);
                sceneViewTree.SetActive(true);
                sceneViewStump.SetActive(false);
                actorStatus.Revive();
            }
        }
    }
}
