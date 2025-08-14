using System;
using UnityEngine;

namespace WOS.MasterDataSystem
{
    [Serializable]
    public class PlayerSpec
    {
        [field: SerializeField]
        public float MoveSpeed { get; private set; }

        [field: SerializeField]
        public int AttackPower { get; private set; }
    }
}
