using UnityEngine;
using System.Collections;

public class TitleLogoAnimation : MonoBehaviour
{
    public float animationDuration = 1.5f;
    public AnimationCurve animationCurve;

    // 終了位置を指定するための変数を追加
    public Vector2 endPosition = new Vector2(5.3406e-05f, 183.0f);

    private RectTransform rectTransform;
    private Vector2 startPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        float halfHeight = rectTransform.rect.height / 2;
        startPosition = new Vector2(endPosition.x, Screen.height + halfHeight);
        
        // 初期位置を設定
        rectTransform.anchoredPosition = startPosition;
    }

    // 外部から呼び出せるようにpublicメソッドを追加
    public void StartLogoAnimation() {
        StartCoroutine(AnimateLogo());
    }
    
    IEnumerator AnimateLogo()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            // アニメーションカーブを適用
            float curveValue = animationCurve.Evaluate(t);

            // 位置を更新
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, curveValue);

            yield return null;
        }

        // 最終位置を確実に設定
        rectTransform.anchoredPosition = endPosition;
    }
}