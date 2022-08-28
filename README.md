# Kogane DOTween Add To Extension Methods

DOTween の Tween に AddTo 拡張メソッドを追加するパッケージ

## 使用例

```csharp
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Kogane;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Example : MonoBehaviour
{
    private async UniTaskVoid Start()
    {
        await transform
                .DOMoveX( 1, 10 )
                .AddTo( gameObject )
            ;

        await transform
                .DOMoveX( -1, 10 )
                .AddTo( gameObject, TweenCancelBehaviour.Kill )
            ;
    }

    private void Update()
    {
        if ( Input.GetKeyDown( KeyCode.Space ) )
        {
            SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex )
        }
    }
}
```