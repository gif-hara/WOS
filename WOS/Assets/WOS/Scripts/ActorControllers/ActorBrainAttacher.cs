using TNRD;
using UnityEngine;
using WOS.ActorControllers.Brains;

namespace WOS.ActorControllers
{
    public class ActorBrainAttacher : MonoBehaviour
    {
        [field: SerializeField]
        private Actor actor;

        [field: SerializeField]
        private SerializableInterface<IActorBrain> brain;

        void Start()
        {
            actor.BrainController.Change(brain.Value);
        }
    }
}
