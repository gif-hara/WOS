using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using UnityEngine;

namespace WOS
{
    public class Item : MonoBehaviour
    {
        private CancellationTokenSource moveScope;

        public async UniTask BeginMoveAsync(Func<Vector3> toPositionSelector, Transform parent, CancellationToken cancellationToken)
        {
            moveScope?.Cancel();
            moveScope?.Dispose();
            moveScope = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            var fromPosition = transform.position;
            await LMotion.Create(0.0f, 1.0f, 0.5f)
                .Bind(x => transform.position = Vector3.Lerp(fromPosition, toPositionSelector(), x))
                .ToUniTask(moveScope.Token);
            transform.SetParent(parent);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
