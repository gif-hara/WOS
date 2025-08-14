namespace WOS.ActorControllers.UpgradeActions
{
    public interface IUpgradeAction
    {
        void Execute(bool restoreFromSaveData);
    }
}
