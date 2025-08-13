using System.Threading;

namespace WOS.ActorControllers.UpgradeCosts
{
    public interface IUpgradeCost
    {
        void BeginObserveView(CancellationToken cancellationToken);

        bool IsEnough();

        void Consume();
    }
}
