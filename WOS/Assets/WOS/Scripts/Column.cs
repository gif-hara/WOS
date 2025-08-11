using System.Collections.Generic;
using UnityEngine;

namespace WOS
{
    public class Column : MonoBehaviour
    {
        private readonly List<bool> isFilleds = new();

        void Start()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                isFilleds.Add(false);
            }
        }

        public void SetFilled(int index, bool isFilled)
        {
            if (index < 0 || index >= isFilleds.Count)
            {
                Debug.LogError($"Index {index} out of range for Column {name}");
                return;
            }
            isFilleds[index] = isFilled;
        }

        public Transform Get(int index)
        {
            if (index < 0 || index >= transform.childCount)
            {
                Debug.LogError($"Index {index} out of range for Column {name}");
                return null;
            }
            return transform.GetChild(index);
        }

        public int GetLastNotFilledIndex()
        {
            for (int i = isFilleds.Count - 1; i >= 0; i--)
            {
                if (!isFilleds[i])
                {
                    return i;
                }
            }
            return -1; // No not filled column found
        }
    }
}
