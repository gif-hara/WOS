using Cysharp.Threading.Tasks;
using HK;
using UnityEngine;

namespace WOS.ActorControllers
{
    public class ActorAnimationEventController : MonoBehaviour
    {
        private Actor actor;

        void Start()
        {
            actor = GetComponentInParent<Actor>();
        }

        public void Attack()
        {
            if (actor == null)
            {
                return;
            }

            actor.Router.PublishAsync(new ActorEvent.OnAttack(), actor.destroyCancellationToken).AsUniTask().Forget();
        }

        public void PlaySfx(string sfxName)
        {
            TinyServiceLocator.Resolve<AudioManager>().PlaySfx(sfxName);
        }
    }
}
