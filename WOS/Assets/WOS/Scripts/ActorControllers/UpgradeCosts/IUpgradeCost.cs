namespace WOS.ActorControllers.UpgradeCosts
{
    public interface IUpgradeCost
    {
        void BeginObserveView();

        bool IsEnough();

        void InvokeUpgrade();
    }
}
