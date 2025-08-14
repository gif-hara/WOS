using HK;
using R3;
using R3.Triggers;
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

        private UserData userData;

        void Start()
        {
            if (SaveSystem.Contains(SaveData.Path))
            {
                var saveData = SaveSystem.Load<SaveData>(SaveData.Path);
                if (saveData != null)
                {
                    userData = new UserData();
                    userData.RestoreFromSaveData(saveData);
                }
                else
                {
                    userData = new UserData();
                }
            }
            else
            {
                userData = new UserData();
            }
            TinyServiceLocator.Register(userData);
            TinyServiceLocator.Register(masterData);
            TinyServiceLocator.Register(audioManager);
            TinyServiceLocator.Register("Player", player);
            var playerBrain = new Player(masterData.PlayerSpec, playerInput, worldCamera);
            player.AddAbility<ActorBrain>().Change(playerBrain);

            var uiViewHUD = new UIViewHUD(hudDocument);
            uiViewHUD.Activate(player);

#if DEBUG
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    if (Keyboard.current.f1Key.wasPressedThisFrame)
                    {
                        player.GetAbility<ActorInventory>()
                            .AddMoney(1000000);
                    }
                    if (Keyboard.current.f2Key.wasPressedThisFrame)
                    {
                    }
                })
                .RegisterTo(destroyCancellationToken);
#endif
        }

        void OnApplicationQuit()
        {
            var saveData = new SaveData();
            saveData.Stats.AddRange(userData.Stats);
            SaveSystem.Save(saveData, SaveData.Path);
        }
    }
}
