using System.Threading;
using WOS.ActorControllers.Brains;

namespace WOS.ActorControllers
{
    public sealed class ActorBrainController
    {
        private readonly Actor actor;

        private CancellationTokenSource scope;

        public ActorBrainController(Actor actor)
        {
            this.actor = actor;
        }

        public void Change(IActorBrain brain)
        {
            scope?.Cancel();
            scope?.Dispose();
            scope = CancellationTokenSource.CreateLinkedTokenSource(actor.destroyCancellationToken);
            brain.Activate(scope.Token);
        }
    }
}
