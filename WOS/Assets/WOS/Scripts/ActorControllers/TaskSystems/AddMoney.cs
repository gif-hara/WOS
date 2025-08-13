using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using HK;
using UnityEngine;
using UnityEngine.Assertions;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class AddMoney : ITask
    {
        [field: SerializeField]
        private string actorName;

        [field: SerializeField]
        private int amount;

        public async UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            var target = TinyServiceLocator.Resolve<Actor>(actorName);
            Assert.IsNotNull(target, $"Actor {actorName} not found.");
            if (target.TryGetAbility<ActorInventory>(out var inventory))
            {
                inventory.AddMoney(amount);
            }
        }
    }
}
