using R3;
using R3.Triggers;
using StandardAssets.Characters.Physics;
using UnityEngine;
using UnityEngine.Assertions;

namespace WOS.ActorControllers.Abilities
{
    public class ActorMovementController : IActorAbility
    {
        private OpenCharacterController characterController;

        private Vector3 velocity;

        private Quaternion rotation;

        private Transform lookAtTarget;

        public void Activate(Actor actor)
        {
            characterController = actor.TryGetComponent<OpenCharacterController>(out var controller) ? controller : null;
            Assert.IsNotNull(characterController, $"{actor.name} must have an {nameof(OpenCharacterController)} component.");

            actor.UpdateAsObservable()
                .Subscribe(this, static (_, @this) =>
                {
                    if (@this.velocity != Vector3.zero)
                    {
                        @this.characterController.Move(@this.velocity);
                        @this.velocity = Vector3.zero;
                    }

                    var currentRotation = @this.characterController.transform.rotation;
                    if (@this.lookAtTarget != null)
                    {
                        @this.rotation = Quaternion.LookRotation(@this.lookAtTarget.position - @this.characterController.transform.position);
                    }
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

        public void BeginLookAt(Transform target)
        {
            this.lookAtTarget = target;
        }

        public void EndLookAt()
        {
            this.lookAtTarget = null;
        }
    }
}
