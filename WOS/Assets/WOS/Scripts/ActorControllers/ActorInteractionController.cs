using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace WOS.ActorControllers
{
    public class ActorInteractionController
    {
        private readonly Actor actor;

        private readonly List<IInteraction> interactions = new();

        private IInteraction currentInteraction;

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
            await interaction.InteractAsync(actor, actor.destroyCancellationToken);
            RemoveInteraction(interaction);
        }
    }
}
