using System;
using UnityEngine;

namespace WOS
{
    [CreateAssetMenu(fileName = "MasterData", menuName = "WOS/MasterData", order = 1)]
    public class MasterData : ScriptableObject
    {
        [field: SerializeField]
        public PlayerSpec PlayerSpec { get; private set; }
    }
}
