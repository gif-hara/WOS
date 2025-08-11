using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;

namespace WOS
{
    public class Item : MonoBehaviour
    {
        public async UniTask BeginMoveAsync(Func<Vector3> toPositionSelector, Transform parent, CancellationToken cancellationToken)
        {
            var fromPosition = transform.position;
            await LMotion.Create(0.0f, 1.0f, 0.5f)
                .Bind(x => transform.position = Vector3.Lerp(fromPosition, toPositionSelector(), x))
                .ToUniTask(cancellationToken);
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
