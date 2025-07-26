using R3;
using R3.Triggers;
using StandardAssets.Characters.Physics;
using UnityEngine;

namespace WOS.ActorControllers
{
    public class ActorMovementController
    {
        private readonly Actor actor;

        private readonly OpenCharacterController characterController;

        private Vector3 velocity;

        public ActorMovementController(Actor actor, OpenCharacterController characterController)
        {
            this.actor = actor;
            this.characterController = characterController;
        }

        public void Activate()
        {
            actor.UpdateAsObservable()
                .Subscribe(this, static (_, @this) =>
                {
                    if (@this.velocity != Vector3.zero)
                    {
                        @this.characterController.Move(@this.velocity);
                        @this.velocity = Vector3.zero;
                    }
                })
                .RegisterTo(actor.destroyCancellationToken);
        }

        public void Move(Vector3 velocity)
        {
            this.velocity += velocity;
        }
    }
}
