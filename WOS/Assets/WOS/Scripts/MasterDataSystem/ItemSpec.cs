using System;
using HK;
using UnityEngine;

namespace WOS.MasterDataSystem
{
    [Serializable]
    public class ItemSpec
    {
        [field: SerializeField]
        public string Id { get; private set; }

        [field: SerializeField]
        public GameObject SceneViewPrefab { get; private set; }

        [Serializable]
        public class DictionaryList : DictionaryList<string, ItemSpec>
        {
            public DictionaryList() : base(x => x.Id)
            {
            }
        }
    }
}
