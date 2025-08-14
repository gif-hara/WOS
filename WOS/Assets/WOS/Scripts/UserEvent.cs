using UnityEngine;
using VitalRouter;

namespace WOS
{
    public class UserEvent
    {
        public readonly struct AddedStat : ICommand
        {
            public string Stat { get; }

            public AddedStat(string stat)
            {
                Stat = stat;
            }
        }
    }
}