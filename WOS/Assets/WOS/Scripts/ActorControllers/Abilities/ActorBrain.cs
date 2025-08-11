using System.Threading;
using WOS.ActorControllers.Brains;

namespace WOS.ActorControllers.Abilities
{
    public sealed class ActorBrain : IActorAbility
    {
        private Actor actor;

        private CancellationTokenSource scope;

        public void Activate(Actor actor)
        {
            this.actor = actor;
        }

        public void Change(IActorBrain brain)
        {
            scope?.Cancel();
            scope?.Dispose();
            scope = CancellationTokenSource.CreateLinkedTokenSource(actor.destroyCancellationToken);
            brain.Activate(actor, scope.Token);
        }
    }
}
