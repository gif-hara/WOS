using StandardAssets.Characters.Physics;
using UnityEngine;

namespace WOS.ActorControllers
{
    public sealed class Actor : MonoBehaviour
    {
        [field: SerializeField]
        private OpenCharacterController characterController;

        public ActorMovementController MovementController { get; private set; }

        public ActorBrainController BrainController { get; private set; }

        private void Awake()
        {
            MovementController = new ActorMovementController(this, characterController);
            BrainController = new ActorBrainController(this);

            MovementController.Activate();
        }
    }
}
