using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace WOS.ActorControllers.Abilities
{
    public class ActorInteraction : IActorAbility
    {
        private Actor actor;

        private readonly List<IInteraction> interactions = new();

        private IInteraction currentInteraction;

        private CancellationTokenSource interactScope;

        public void Activate(Actor actor)
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

        public bool ContainsInteraction(Actor actor)
        {
            foreach (var interaction in interactions)
            {
                if (interaction.Actor == actor)
                {
                    return true;
                }
            }
            return false;
        }

        private async UniTask BeginInteraction(IInteraction interaction)
        {
            interactScope = CancellationTokenSource.CreateLinkedTokenSource(actor.destroyCancellationToken);
            try
            {
                var scope = interactScope.Token;
                actor.GetAbility<ActorMovement>().BeginLookAt(interaction.Actor.transform);
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
                if (actor != null)
                {
                    actor.GetAbility<ActorMovement>().EndLookAt();
                }
            }
        }
    }
}
