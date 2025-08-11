using System.Collections.Generic;
using UnityEngine;
using WOS.MasterDataSystem;

namespace WOS.ActorControllers.Abilities
{
    public class ActorInventory : IActorAbility
    {
        private Inventory inventory;

        public void Activate(Actor actor)
        {
            inventory = new Inventory(actor.Document.Q<PlacementPoint>("PlacementPoint"));
        }

        public void AddItems(List<Inventory.Element> elements)
        {
            inventory.AddItems(elements);
        }
    }
}
