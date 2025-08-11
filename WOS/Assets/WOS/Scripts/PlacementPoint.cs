using System.Collections.Generic;
using UnityEngine;

namespace WOS
{
    public class PlacementPoint : MonoBehaviour
    {
        [field: SerializeField]
        public List<Transform> Points { get; private set; }
    }
}
