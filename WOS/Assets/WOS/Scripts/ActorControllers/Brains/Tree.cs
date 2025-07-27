using System;
using System.Threading;
using UnityEngine;

namespace WOS.ActorControllers.Brains
{
    [Serializable]
    public sealed class Tree : IActorBrain
    {
        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            Debug.Log("Tree brain activated");
        }
    }
}
