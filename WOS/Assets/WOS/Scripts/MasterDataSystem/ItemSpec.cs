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
        public Item ItemPrefab { get; private set; }

        [field: SerializeField]
        public string BeginCollectSfxName { get; private set; }

        [field: SerializeField]
        public string EndCollectSfxName { get; private set; }

        [Serializable]
        public class DictionaryList : DictionaryList<string, ItemSpec>
        {
            public DictionaryList() : base(x => x.Id)
            {
            }
        }
    }
}
