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
        /// 指定されたゲームオブジェクトが破棄された時に Tween をキャンセルします
        /// </summary>
        public static UniTask AddTo
        (
            this Tween            self,
            GameObject            gameObject,
            TweenCancelBehaviour? tweenCancelBehaviour = null
        )
        {
            var cancellationToken = gameObject.GetCancellationTokenOnDestroy();

            return self
                    .SetLink( gameObject )
                    .ToUniTask( tweenCancelBehaviour ?? DefaultTweenCancelBehaviour, cancellationToken )
                ;
        }

        /// <summary>
        /// 指定されたゲームオブジェクトが破棄された時に Tween をキャンセルします
        /// </summary>
        public static UniTask AddTo
        (
            this Tween            self,
            Component             component,
            TweenCancelBehaviour? tweenCancelBehaviour = null
        )
        {
            return self.AddTo( component.gameObject, tweenCancelBehaviour );
        }
    }
}