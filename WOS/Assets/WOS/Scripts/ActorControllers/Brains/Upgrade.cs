using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using TNRD;
using UnityEngine;
using WOS.ActorControllers.UpgradeActions;
using WOS.ActorControllers.UpgradeCosts;

namespace WOS.ActorControllers.Brains
{
    public sealed class Upgrade : IActorBrain, IInteraction
    {
        [field: SerializeField]
        private SerializableInterface<IUpgradeCost> cost;

        [field: SerializeField]
        private List<SerializableInterface<IUpgradeAction>> actions;

        private Actor actor;

        public Actor Actor => actor;

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            this.actor = actor;
            this.SubscribeOnTrigger(actor, actor.Document.Q<Collider>("Trigger"))
                .RegisterTo(cancellationToken);
            cost.Value.BeginObserveView(cancellationToken);
        }

        public UniTask InteractAsync(Actor interactedActor, CancellationToken cancellationToken)
        {
            if (cost.Value.IsEnough())
            {
                foreach (var action in actions)
                {
                    action.Value.Execute();
                }
                cost.Value.Consume();
                actor.gameObject.SetActive(false);
            }
            return UniTask.CompletedTask;
        }
    }
}
