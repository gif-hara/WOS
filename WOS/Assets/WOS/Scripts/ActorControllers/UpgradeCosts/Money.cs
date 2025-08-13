using System;
using HK;
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
        private string actorName;

        [field: SerializeField]
        private int cost;

        public void BeginObserveView()
        {
            text.text = cost.ToString();
        }

        public bool IsEnough()
        {
            return TinyServiceLocator.Resolve<Actor>(actorName).GetAbility<ActorInventory>().Money >= cost;
        }
    }
}
