using TNRD;
using UnityEngine;
using WOS.ActorControllers.Brains;

namespace WOS.ActorControllers
{
    public class ActorBrainAttacher : MonoBehaviour
    {
        [field: SerializeField]
        private SerializableInterface<IActorBrain> brain;
    }
}
