using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class PopupWindowAnimation : MonoBehaviour
{
    public float animationDuration = 0.5f;
    public AnimationCurve scaleCurve;
    public AnimationCurve moveCurve;
    public TitleLogoAnimation titleLogoAnimation;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 startScale;
    private Vector2 endScale;
    private Action onAnimationComplete;

    public void StartAnimation(Action callback)
    {
        onAnimationComplete = callback;
        StartCoroutine(AnimateWindow());
    }

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 初期設定
        startPosition = new Vector2(0, -rectTransform.rect.height);
        endPosition = Vector2.zero;
        startScale = Vector2.zero;
        endScale = Vector2.one;

        rectTransform.anchoredPosition = startPosition;
        rectTransform.localScale = startScale;
        canvasGroup.alpha = 0;
    }

    IEnumerator AnimateWindow()
    {
        float elapsedTime = 0f;

        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / animationDuration;

            // スケーリング
            float scaleValue = scaleCurve.Evaluate(t);
            rectTransform.localScale = Vector2.Lerp(startScale, endScale, scaleValue);

            // 移動
            float moveValue = moveCurve.Evaluate(t);
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, endPosition, moveValue);

            // フェードイン
            canvasGroup.alpha = Mathf.Lerp(0, 1, t);

            yield return null;
        }

        // 最終位置とスケールを確実に設定
        rectTransform.anchoredPosition = endPosition;
        rectTransform.localScale = endScale;
        canvasGroup.alpha = 1;

        // わずかな跳ね返り効果
        StartCoroutine(BounceEffect());

        onAnimationComplete?.Invoke();
    }

    IEnumerator BounceEffect()
    {
        Vector2 slightlyLargerScale = endScale * 1.05f;
        float bounceTime = 0.1f;

        // わずかに大きく
        for (float t = 0; t < bounceTime; t += Time.deltaTime)
        {
            rectTransform.localScale = Vector2.Lerp(endScale, slightlyLargerScale, t / bounceTime);
            yield return null;
        }

        // 元のサイズに戻る
        for (float t = 0; t < bounceTime; t += Time.deltaTime)
        {
            rectTransform.localScale = Vector2.Lerp(slightlyLargerScale, endScale, t / bounceTime);
            yield return null;
        }

        rectTransform.localScale = endScale;

        // ポップアップウィンドウのアニメーション完了後、タイトルロゴのアニメーションを開始
        if (titleLogoAnimation != null)
        {
            titleLogoAnimation.StartLogoAnimation();
        }
    }
}