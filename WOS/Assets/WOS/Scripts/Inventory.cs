using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using WOS.MasterDataSystem;

namespace WOS
{
    public class Inventory
    {
        private readonly PlacementPoint placementPoint;

        private readonly List<Element> elements = new();

        public Inventory(PlacementPoint placementPoint)
        {
            this.placementPoint = placementPoint;
        }

        public void AddItem(ItemSpec itemSpec, GameObject itemObject)
        {
            Assert.IsNotNull(itemSpec, "ItemSpec cannot be null.");
            Assert.IsNotNull(itemObject, "ItemObject cannot be null.");

            var element = new Element(itemSpec, itemObject);
            var point = placementPoint.Points[elements.Count % placementPoint.Points.Count];
            element.ItemObject.transform.SetParent(point);
            element.ItemObject.transform.localPosition = new Vector3(0, elements.Count / placementPoint.Points.Count * 0.25f, 0);
            element.ItemObject.transform.localRotation = Quaternion.identity;
            elements.Add(element);
        }

        public class Element
        {
            public ItemSpec ItemSpec { get; }

            public GameObject ItemObject { get; }

            public Element(ItemSpec itemSpec, GameObject itemObject)
            {
                ItemSpec = itemSpec;
                ItemObject = itemObject;
            }
        }
    }
}
