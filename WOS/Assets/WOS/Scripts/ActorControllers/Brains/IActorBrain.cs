using System.Threading;

namespace WOS.ActorControllers.Brains
{
    public interface IActorBrain
    {
        void Activate(Actor actor, CancellationToken cancellationToken);
    }
}
