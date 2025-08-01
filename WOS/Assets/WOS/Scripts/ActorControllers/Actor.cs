using HK;
using StandardAssets.Characters.Physics;
using UnityEngine;
using VitalRouter;

namespace WOS.ActorControllers
{
    public sealed class Actor : MonoBehaviour
    {
        [field: SerializeField]
        public HKUIDocument Document { get; private set; }

        [field: SerializeField]
        private OpenCharacterController characterController;

        public readonly Router Router = new();

        public ActorMovementController MovementController { get; private set; }

        public ActorBrainController BrainController { get; private set; }

        public ActorInteractionController InteractionController { get; private set; }

        public ActorAnimationController AnimationController { get; private set; }

        private void Awake()
        {
            MovementController = new ActorMovementController(this, characterController);
            BrainController = new ActorBrainController(this);
            AnimationController = new ActorAnimationController(Document.Q<Animator>("Animator"));
            InteractionController = new ActorInteractionController(this);

            MovementController.Activate();
        }
    }
}
