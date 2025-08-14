using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using VitalRouter;

namespace WOS
{
    public sealed class UserData
    {
        public Router Router { get; } = new();

        private readonly HashSet<string> stats = new();

        public void AddStat(string stat)
        {
            if (ContainsStat(stat))
            {
                return;
            }
            stats.Add(stat);
            Router.PublishAsync(new UserEvent.AddedStat(stat)).AsUniTask().Forget();
        }

        public void RemoveStat(string stat)
        {
            stats.Remove(stat);
        }

        public bool ContainsStat(string stat)
        {
            return stats.Contains(stat);
        }

        public void RestoreFromSaveData(SaveData saveData)
        {
            stats.Clear();
            stats.UnionWith(saveData.Stats);
        }

        public IReadOnlyCollection<string> Stats => stats;
    }
}
