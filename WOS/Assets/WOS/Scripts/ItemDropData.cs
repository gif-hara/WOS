using System;
using UnityEngine;

namespace WOS
{
    [Serializable]
    public class ItemDropData
    {
        [field: SerializeField, Range(0, 1)]
        public float Probability { get; private set; }

        [field: SerializeField]
        public string ItemId { get; private set; }

        [field: SerializeField]
        public int Amount { get; private set; }
    }
}
