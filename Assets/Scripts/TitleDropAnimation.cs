using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleDropAnimation : MonoBehaviour
{
    public float dropDuration = 2f;  // アニメーションの持続時間
    public float startYPosition = 500f;  // 開始Y位置（画面上部）
    public float endYPosition = 0f;  // 終了Y位置（画面中央）
    public AnimationCurve dropCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);  // アニメーションカーブ
    public Image titleImage;  // タイトル用のImage component

    private RectTransform rectTransform;

    void Start()
    {
        // Image componentが設定されていない場合、自動的に取得
        if (titleImage == null)
        {
            titleImage = GetComponent<Image>();
        }

        if (titleImage == null)
        {
            Debug.LogError("Image component is not found. Please assign it in the inspector or add it to this GameObject.");
            return;
        }

        rectTransform = titleImage.rectTransform;
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, startYPosition);
        StartCoroutine(DropTitle());
    }

    IEnumerator DropTitle()
    {
        float elapsedTime = 0f;

        while (elapsedTime < dropDuration)
        {
            float t = elapsedTime / dropDuration;
            float yPosition = Mathf.Lerp(startYPosition, endYPosition, dropCurve.Evaluate(t));
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, yPosition);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, endYPosition);
    }
}