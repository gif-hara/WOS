using System;
using System.Threading;
using HK;
using R3;
using TMPro;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.UpgradeCosts
{
    [Serializable]
    public sealed class Money : IUpgradeCost
    {
        [field: SerializeField]
        private TMP_Text text;

        [field: SerializeField]
        private Color enoughColor;

        [field: SerializeField]
        private Color notEnoughColor;

        [field: SerializeField]
        private string actorName;

        [field: SerializeField]
        private int cost;

        public void BeginObserveView(CancellationToken cancellationToken)
        {
            var actor = TinyServiceLocator.Resolve<Actor>(actorName);
            var actorInventory = actor.GetAbility<ActorInventory>();
            actorInventory.MoneyAsObservable()
                .Subscribe(money =>
                {
                    text.color = money >= cost ? enoughColor : notEnoughColor;
                })
                .RegisterTo(cancellationToken);
            text.text = cost.ToString();
        }

        public bool IsEnough()
        {
            return TinyServiceLocator.Resolve<Actor>(actorName).GetAbility<ActorInventory>().IsEnoughMoney(cost);
        }

        public void Consume()
        {
            var inventory = TinyServiceLocator.Resolve<Actor>(actorName).GetAbility<ActorInventory>();
            inventory.ConsumeMoney(cost);
        }
    }
}
