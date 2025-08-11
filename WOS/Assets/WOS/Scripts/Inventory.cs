using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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

        public void AddItem(ItemSpec itemSpec, Item itemObject)
        {
            Assert.IsNotNull(itemSpec, "ItemSpec cannot be null.");
            Assert.IsNotNull(itemObject, "ItemObject cannot be null.");

            var element = new Element(itemSpec, itemObject);
            var point = placementPoint.Points[elements.Count % placementPoint.Points.Count];
            var index = elements.Count;
            element.ItemObject.BeginMoveAsync(() => new Vector3(point.position.x, point.position.y + (index / placementPoint.Points.Count * 0.25f), point.position.z), point, default).Forget();
            elements.Add(element);
        }

        public class Element
        {
            public ItemSpec ItemSpec { get; }

            public Item ItemObject { get; }

            public Element(ItemSpec itemSpec, Item itemObject)
            {
                ItemSpec = itemSpec;
                ItemObject = itemObject;
            }
        }
    }
}
