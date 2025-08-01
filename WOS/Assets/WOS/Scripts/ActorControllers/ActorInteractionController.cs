using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace WOS.ActorControllers
{
    public class ActorInteractionController
    {
        private readonly Actor actor;

        private readonly List<IInteraction> interactions = new();

        private IInteraction currentInteraction;

        private CancellationTokenSource interactScope;

        public ActorInteractionController(Actor actor)
        {
            this.actor = actor;
        }

        public void AddInteraction(IInteraction interaction)
        {
            interactions.Add(interaction);

            if (interactions.Count == 1)
            {
                currentInteraction = interaction;
                BeginInteraction(interaction).Forget();
            }
        }

        public void RemoveInteraction(IInteraction interaction)
        {
            interactions.Remove(interaction);
            if (interaction == currentInteraction)
            {
                interactScope?.Cancel();
                interactScope?.Dispose();
                currentInteraction = null;
                if (interactions.Count > 0)
                {
                    currentInteraction = interactions[0];
                    BeginInteraction(currentInteraction).Forget();
                }
            }
        }

        private async UniTask BeginInteraction(IInteraction interaction)
        {
            interactScope = CancellationTokenSource.CreateLinkedTokenSource(actor.destroyCancellationToken);
            try
            {
                var scope = interactScope.Token;
                actor.MovementController.BeginLookAt(interaction.Transform);
                await interaction.InteractAsync(actor, scope);
                if (!scope.IsCancellationRequested)
                {
                    interactScope.Cancel();
                    interactScope.Dispose();
                    interactScope = null;
                }
                RemoveInteraction(interaction);
            }
            finally
            {
                actor.MovementController.EndLookAt();
            }
        }
    }
}
