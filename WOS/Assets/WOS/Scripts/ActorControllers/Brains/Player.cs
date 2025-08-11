using System.Threading;
using R3;
using R3.Triggers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WOS.ActorControllers.Brains
{
    public class Player : IActorBrain
    {
        private readonly PlayerSpec playerSpec;

        private readonly PlayerInput playerInput;

        private readonly Camera camera;

        public Player(PlayerSpec playerSpec, PlayerInput playerInput, Camera camera)
        {
            this.playerSpec = playerSpec;
            this.playerInput = playerInput;
            this.camera = camera;
        }

        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            actor.UpdateAsObservable()
                .Subscribe((this, actor), static (_, t) =>
                {
                    var (@this, actor) = t;
                    var moveInput = @this.playerInput.actions["Move"].ReadValue<Vector2>();
                    var moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
                    if (moveDirection != Vector3.zero)
                    {
                        var forward = @this.camera.transform.forward;
                        forward.y = 0;
                        forward.Normalize();
                        var right = @this.camera.transform.right;
                        right.y = 0;
                        right.Normalize();
                        var moveVelocity = forward * moveDirection.z + right * moveDirection.x;
                        actor.MovementController.Move(moveVelocity * @this.playerSpec.MoveSpeed * Time.deltaTime);
                        actor.MovementController.Rotate(Quaternion.LookRotation(moveVelocity));
                    }
                })
                .RegisterTo(actor.destroyCancellationToken);
        }
    }
}
