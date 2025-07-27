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
                        moveDirection = @this.camera.transform.TransformDirection(moveDirection);
                        moveDirection.y = 0; // Keep movement on the horizontal plane
                        actor.MovementController.Move(moveDirection * @this.playerSpec.MoveSpeed * Time.deltaTime);
                    }
                })
                .RegisterTo(actor.destroyCancellationToken);
        }
    }
}
