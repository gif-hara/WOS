using R3;

namespace WOS.ActorControllers.Abilities
{
    public class ActorInventory : IActorAbility
    {
        public Inventory Inventory { get; private set; }

        private readonly ReactiveProperty<int> money = new(0);

        public ReadOnlyReactiveProperty<int> MoneyAsObservable() => money;

        public int Money => money.Value;

        public void Activate(Actor actor)
        {
            Inventory = new Inventory(actor.Document.Q<PlacementPoint>("PlacementPoint"));
        }

        public void AddMoney(int amount)
        {
            money.Value += amount;
        }

        public void ConsumeMoney(int amount)
        {
            money.Value -= amount;
        }

        public bool IsEnoughMoney(int amount)
        {
            return money.Value >= amount;
        }
    }
}
