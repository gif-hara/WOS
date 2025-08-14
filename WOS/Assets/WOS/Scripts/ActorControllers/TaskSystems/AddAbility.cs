using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    [Serializable]
    public sealed class AddAbility : ITask
    {
        [field: SerializeField]
        private Define.ActorAbilityType[] abilityTypes;

        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            foreach (var abilityType in abilityTypes)
            {
                switch (abilityType)
                {
                    case Define.ActorAbilityType.Movement:
                        actor.AddAbility<ActorMovement>();
                        break;
                    case Define.ActorAbilityType.Interaction:
                        actor.AddAbility<ActorInteraction>();
                        break;
                    case Define.ActorAbilityType.Animation:
                        actor.AddAbility<ActorAnimation>();
                        break;
                    case Define.ActorAbilityType.Inventory:
                        actor.AddAbility<ActorInventory>();
                        break;
                    case Define.ActorAbilityType.Attack:
                        actor.AddAbility<ActorAttack>();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(abilityType), abilityType, null);
                }
            }

            return UniTask.CompletedTask;
        }
    }
}
