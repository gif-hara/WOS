using System;
using R3;
using R3.Triggers;
using UnityEngine;
using WOS.ActorControllers;
using WOS.ActorControllers.Abilities;

namespace WOS
{
    public static partial class Extensions
    {
        public static IDisposable SubscribeOnTrigger(this IInteraction self, Actor actor, Collider collider)
        {
            return new CompositeDisposable
            {
                collider
                    .OnTriggerEnterAsObservable()
                    .Subscribe((self, actor), static (x, t) =>
                    {
                        var (@this, actor) = t;
                        if (x.attachedRigidbody == null)
                        {
                            return;
                        }
                        if (!x.attachedRigidbody.TryGetComponent<Actor>(out var collidedActor))
                        {
                            return;
                        }
                        if(collidedActor.TryGetAbility<ActorInteraction>(out var interaction))
                        {
                            interaction.AddInteraction(@this);
                        }
                    }),
                collider
                    .OnTriggerExitAsObservable()
                    .Subscribe((self, actor), static (x, t) =>
                    {
                        var (@this, actor) = t;
                        if (x.attachedRigidbody == null)
                        {
                            return;
                        }
                        if (!x.attachedRigidbody.TryGetComponent<Actor>(out var collidedActor))
                        {
                            return;
                        }
                        if (collidedActor.TryGetAbility<ActorInteraction>(out var interaction))
                        {
                            interaction.RemoveInteraction(@this);
                        }
                    })
            };

        }
    }
}
