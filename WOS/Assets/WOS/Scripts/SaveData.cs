using System;
using System.Collections.Generic;
using UnityEngine;

namespace WOS
{
    [Serializable]
    public class SaveData
    {
        public const string Path = "SaveData.dat";

        [field: SerializeField]
        public List<string> Stats { get; private set; } = new();
    }
}
