using System;
using System.Threading;
using R3;
using R3.Triggers;
using UnityEngine;

namespace WOS.ActorControllers.Brains
{
    [Serializable]
    public sealed class Tree : IActorBrain
    {
        public void Activate(Actor actor, CancellationToken cancellationToken)
        {
            Debug.Log("Tree brain activated");

            actor.Document.Q<Collider>("Trigger")
                .OnTriggerEnterAsObservable()
                .Subscribe(x =>
                {
                    Debug.Log("Trigger entered by: " + x.name);
                })
                .RegisterTo(cancellationToken);
        }
    }
}
