using UnityEngine;
using System.Collections;

public class TitleLogoAnimation : MonoBehaviour
{
    public float animationDuration = 1.5f;
    public AnimationCurve animationCurve;

    // 終了位置を指定するための変数を追加
    public Vector2 endPosition = new Vector2(5.3406e-05f, 183.0f);

    // 上下の動きの幅
    public float floatAmplitude = 3f;
    // 上下の動きの速さ
    public float floatFrequency = 0.5f;

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
    public void StartLogoAnimation()
    {
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

        // 浮遊効果の開始
        StartCoroutine(FloatLogo());
    }

    IEnumerator FloatLogo()
    {
        Vector2 basePosition = endPosition;
        float elapsedTime = 0f;

        while (true) // 無限ループ（必要に応じて終了条件を追加）
        {
            elapsedTime += Time.deltaTime;

            // Sin波を使用して上下の動きを作成
            float yOffset = Mathf.Sin(elapsedTime * floatFrequency * 2 * Mathf.PI) * floatAmplitude;

            Vector2 newPosition = basePosition + new Vector2(0, yOffset);
            rectTransform.anchoredPosition = newPosition;

            yield return null;
        }
    }
}