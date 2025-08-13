using HK;
using UnityEngine;

namespace WOS.ActorControllers.UpgradeActions
{
    public class PlaySfx : IUpgradeAction
    {
        [field: SerializeField]
        private string sfxName;

        public void Execute()
        {
            TinyServiceLocator.Resolve<AudioManager>().PlaySfx(sfxName);
        }
    }
}
