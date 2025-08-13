using HK;
using UnityEngine;
using UnityEngine.InputSystem;
using WOS.ActorControllers;
using WOS.ActorControllers.Abilities;
using WOS.ActorControllers.Brains;
using WOS.MasterDataSystem;
using WOS.UIViews;

namespace WOS
{
    public class MainSceneController : MonoBehaviour
    {
        [field: SerializeField]
        private Actor player;

        [field: SerializeField]
        private MasterData masterData;

        [field: SerializeField]
        private PlayerInput playerInput;

        [field: SerializeField]
        private Camera worldCamera;

        [field: SerializeField]
        private AudioManager audioManager;

        [field: SerializeField]
        private HKUIDocument hudDocument;

        void Start()
        {
            TinyServiceLocator.Register(masterData);
            TinyServiceLocator.Register(audioManager);
            TinyServiceLocator.Register("Player", player);
            var playerBrain = new Player(masterData.PlayerSpec, playerInput, worldCamera);
            player.AddAbility<ActorBrain>().Change(playerBrain);

            var uiViewHUD = new UIViewHUD(hudDocument);
            uiViewHUD.Activate(player);
        }
    }
}
