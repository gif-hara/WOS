using Cysharp.Threading.Tasks;
using UnityEngine;
using VitalRouter;

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
    }
}
