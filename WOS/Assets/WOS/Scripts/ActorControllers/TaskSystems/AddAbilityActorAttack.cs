using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class AddAbilityActorAttack : ITask
    {
        [field: SerializeField]
        private int power;

        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            actor.AddAbility(new ActorAttack(power));
            return UniTask.CompletedTask;
        }
    }
}
