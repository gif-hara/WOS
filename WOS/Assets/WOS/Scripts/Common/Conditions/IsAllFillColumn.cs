using System;
using UnityEngine;

namespace WOS.Common.Conditions
{
    [Serializable]
    public sealed class IsAllFillColumn : ICondition
    {
        [field: SerializeField]
        private Column column;

        [field: SerializeField]
        private bool isTrue;

        public bool Evaluate()
        {
            return column.IsAllFilled() == isTrue;
        }
    }
}
