using System;
using UnityEngine;

namespace WOS
{
    [Serializable]
    public class PlayerSpec
    {
        [field: SerializeField]
        public float MoveSpeed { get; private set; }
    }
}
