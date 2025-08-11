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

        public void AddItems(List<Element> elements)
        {
            this.elements.AddRange(elements);
            for (var i = 0; i < elements.Count; i++)
            {
                var index = this.elements.Count - elements.Count + i;
                var element = elements[i];
                var point = placementPoint.Points[index % placementPoint.Points.Count];
                element.ItemObject.BeginMoveAsync(
                    point,
                    index / placementPoint.Points.Count,
                    i * 0.1f,
                    element.ItemObject.destroyCancellationToken
                    )
                    .Forget();
            }
        }

        public void RemoveItem(int index)
        {
            Assert.IsTrue(index >= 0 && index < elements.Count, "Index out of range");
            elements.RemoveAt(index);
            for (var i = index; i < elements.Count; i++)
            {
                var point = placementPoint.Points[i % placementPoint.Points.Count];
                elements[i].ItemObject.BeginMoveAsync(
                    point,
                    i / placementPoint.Points.Count,
                    i * 0.1f,
                    elements[i].ItemObject.destroyCancellationToken
                ).Forget();
            }
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
