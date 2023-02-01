using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// DOTween に関する拡張メソッドを管理するクラス
    /// </summary>
    public static class DOTweenAddToExtensionMethods
    {
        //================================================================================
        // プロパティ(static)
        //================================================================================
        public static TweenCancelBehaviour DefaultTweenCancelBehaviour { private get; set; } = TweenCancelBehaviour.KillAndCancelAwait;

        //================================================================================
        // 関数(static)
        //================================================================================
        /// <summary>
        /// 指定された CancellationToken がキャンセルされた時に Tween をキャンセルします
        /// </summary>
        public static async UniTask AddTo
        (
            this Tween            self,
            CancellationToken     cancellationToken,
            TweenCancelBehaviour? tweenCancelBehaviour = null
        )
        {
            await self.ToUniTask
            (
                tweenCancelBehaviour: tweenCancelBehaviour ?? DefaultTweenCancelBehaviour,
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        /// 指定されたゲームオブジェクトが破棄された時に Tween をキャンセルします
        /// </summary>
        public static async UniTask AddTo
        (
            this Tween            self,
            GameObject            gameObject,
            TweenCancelBehaviour? tweenCancelBehaviour = null
        )
        {
            if ( gameObject == null ) throw new OperationCanceledException();

            var cancellationToken = gameObject.GetCancellationTokenOnDestroy();

            await self
                    .SetLink( gameObject )
                    .ToUniTask( tweenCancelBehaviour ?? DefaultTweenCancelBehaviour, cancellationToken )
                ;
        }

        /// <summary>
        /// 指定されたゲームオブジェクトが破棄された時に Tween をキャンセルします
        /// </summary>
        public static async UniTask AddTo
        (
            this Tween            self,
            Component             component,
            TweenCancelBehaviour? tweenCancelBehaviour = null
        )
        {
            if ( component == null ) throw new OperationCanceledException();

            await self.AddTo( component.gameObject, tweenCancelBehaviour );
        }
    }
}