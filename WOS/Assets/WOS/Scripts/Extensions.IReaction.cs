using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using TNRD;
using WOS.ReactionSystems;

namespace WOS
{
    public static partial class Extensions
    {
        public static async UniTask InvokeAsync(this IEnumerable<SerializableInterface<IReaction>> reactions, CancellationToken cancellationToken)
        {
            foreach (var reaction in reactions)
            {
                await reaction.Value.InvokeAsync(cancellationToken);
            }
        }
    }
}
