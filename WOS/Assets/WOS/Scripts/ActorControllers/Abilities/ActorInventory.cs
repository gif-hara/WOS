namespace WOS.ActorControllers.Abilities
{
    public class ActorInventory : IActorAbility
    {
        public Inventory Inventory { get; private set; }

        public void Activate(Actor actor)
        {
            Inventory = new Inventory(actor.Document.Q<PlacementPoint>("PlacementPoint"));
        }
    }
}
