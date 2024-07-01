using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleDropAnimation : MonoBehaviour
{
    public float dropDuration = 6f;  // アニメーションの持続時間（秒）
    public float startYPosition = 1080f;  // 開始Y位置（画面上部外）
    public float endYPosition = 0f;  // 終了Y位置（画面中央）
    public AnimationCurve dropCurve = new AnimationCurve(
        new Keyframe(0, 0, 0, 0),
        new Keyframe(0.3f, 0.05f, 0.1f, 0.1f),
        new Keyframe(0.7f, 0.2f, 0.5f, 0.5f),
        new Keyframe(1, 1, 2, 0)
    );  // カスタムアニメーションカーブ
    public Image titleImage;  // タイトル用のImage component

    private RectTransform rectTransform;

    void Start()
    {
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
        
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);
        
        rectTransform.anchoredPosition = new Vector2(0, startYPosition);
        
        StartCoroutine(SlowDropTitle());
    }

    IEnumerator SlowDropTitle()
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