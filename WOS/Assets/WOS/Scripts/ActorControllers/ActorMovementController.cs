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

        private Quaternion rotation;

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

                    var currentRotation = @this.characterController.transform.rotation;
                    @this.characterController.transform.rotation = Quaternion.Slerp(currentRotation, @this.rotation, Time.deltaTime * 10f);
                })
                .RegisterTo(actor.destroyCancellationToken);
        }

        public void Move(Vector3 velocity)
        {
            this.velocity += velocity;
        }

        public void Rotate(Quaternion rotation)
        {
            this.rotation = rotation;
        }
    }
}
