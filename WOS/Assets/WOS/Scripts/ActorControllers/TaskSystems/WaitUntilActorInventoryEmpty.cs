using System.Threading;
using Cysharp.Threading.Tasks;
using WOS.ActorControllers.Abilities;

namespace WOS.ActorControllers.TaskSystems
{
    public sealed class WaitUntilActorInventoryEmpty : ITask
    {
        public UniTask RunAsync(Actor actor, CancellationToken cancellationToken)
        {
            return actor.GetAbility<ActorInventory>().Inventory.WaitUntilInventoryEmpty(cancellationToken);
        }
    }
}
