namespace WOS.ActorControllers.Abilities
{
    public class ActorInventory : IActorAbility
    {
        public Inventory Inventory { get; private set; }

        public int Money { get; private set; }

        public void Activate(Actor actor)
        {
            Inventory = new Inventory(actor.Document.Q<PlacementPoint>("PlacementPoint"));
            Money = 0;
        }

        public void AddMoney(int amount)
        {
            Money += amount;
        }

        public void ConsumeMoney(int amount)
        {
            Money -= amount;
        }

        public bool IsEnoughMoney(int amount)
        {
            return Money >= amount;
        }
    }
}
