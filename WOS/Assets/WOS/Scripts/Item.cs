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

        public async UniTask BeginMoveAsync(Transform parent, int heightIndex, float delaySeconds, CancellationToken cancellationToken)
        {
            moveScope?.Cancel();
            moveScope?.Dispose();
            moveScope = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            await UniTask.Delay(TimeSpan.FromSeconds(delaySeconds), cancellationToken: moveScope.Token);

            transform.SetParent(null);
            var fromPosition = transform.position;
            var yMax = Mathf.Max(fromPosition.y, heightIndex * 0.25f) + 2.0f;
            var fromRotation = transform.rotation;
            const float duration = 0.5f;
            await LSequence.Create()
                .Join(
                    LMotion.Create(0.0f, 1.0f, duration)
                        .Bind(x =>
                        {
                            var toPosition = parent.position;
                            var position = transform.position;
                            position.x = Mathf.Lerp(fromPosition.x, toPosition.x, x);
                            position.z = Mathf.Lerp(fromPosition.z, toPosition.z, x);
                            transform.position = position;
                        })
                )
                .Join(
                    LSequence.Create()
                        .Append(
                            LMotion.Create(0.0f, 1.0f, duration * 0.5f)
                                .WithEase(Ease.OutQuad)
                                .Bind(x =>
                                {
                                    var position = transform.position;
                                    position.y = Mathf.Lerp(fromPosition.y, yMax, x);
                                    transform.position = position;
                                })
                        )
                        .Append(
                            LMotion.Create(0.0f, 1.0f, duration * 0.5f)
                                .WithEase(Ease.InQuad)
                                .Bind(x =>
                                {
                                    var position = transform.position;
                                    position.y = Mathf.Lerp(yMax, parent.position.y + heightIndex * 0.25f, x);
                                    transform.position = position;
                                })
                        )
                        .Run()
                )
                .Join(
                    LMotion.Create(0.0f, 1.0f, duration)
                        .Bind(x =>
                        {
                            transform.rotation = Quaternion.Lerp(fromRotation, parent.rotation, x);
                        })
                )
                .Run()
                .ToUniTask(moveScope.Token);
            transform.SetParent(parent);
            transform.localPosition = new Vector3(0, heightIndex * 0.25f, 0);
            transform.localRotation = Quaternion.identity;
        }
    }
}
