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
            var yMax = Mathf.Max(fromPosition.y, toPositionSelector().y) + 2.0f;
            var fromRotation = transform.rotation;
            await LSequence.Create()
                .Join(
                    LMotion.Create(0.0f, 1.0f, 0.5f)
                        .Bind(x =>
                        {
                            var toPosition = toPositionSelector();
                            var position = transform.position;
                            position.x = Mathf.Lerp(fromPosition.x, toPosition.x, x);
                            position.z = Mathf.Lerp(fromPosition.z, toPosition.z, x);
                            transform.position = position;
                        })
                )
                .Join(
                    LSequence.Create()
                        .Append(
                            LMotion.Create(0.0f, 1.0f, 0.25f)
                                .WithEase(Ease.OutQuad)
                                .Bind(x =>
                                {
                                    var toPosition = toPositionSelector();
                                    var position = transform.position;
                                    position.y = Mathf.Lerp(fromPosition.y, yMax, x);
                                    transform.position = position;
                                })
                        )
                        .Append(
                            LMotion.Create(0.0f, 1.0f, 0.25f)
                                .WithEase(Ease.InQuad)
                                .Bind(x =>
                                {
                                    var position = transform.position;
                                    position.y = Mathf.Lerp(yMax, toPositionSelector().y, x);
                                    transform.position = position;
                                })
                        )
                        .Run()
                )
                .Join(
                    LMotion.Create(0.0f, 1.0f, 0.5f)
                        .Bind(x =>
                        {
                            transform.rotation = Quaternion.Lerp(fromRotation, parent.rotation, x);
                        })
                )
                .Run()
                .ToUniTask(moveScope.Token);
            transform.SetParent(parent);
            transform.position = toPositionSelector();
            transform.localRotation = Quaternion.identity;
        }
    }
}
