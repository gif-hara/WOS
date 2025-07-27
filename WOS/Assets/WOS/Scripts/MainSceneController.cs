using UnityEngine;
using UnityEngine.InputSystem;
using WOS.ActorControllers;
using WOS.ActorControllers.Brains;

namespace WOS
{
    public class MainSceneController : MonoBehaviour
    {
        [field: SerializeField]
        private Actor player;

        [field: SerializeField]
        private PlayerInput playerInput;

        [field: SerializeField]
        private Camera worldCamera;

        void Start()
        {
            var playerBrain = new Player(playerInput, worldCamera);
            player.BrainController.Change(playerBrain);
        }
    }
}
