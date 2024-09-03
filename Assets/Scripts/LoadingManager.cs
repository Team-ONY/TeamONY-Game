using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingWindow;
    public Image progressBarFill;
    public TextMeshProUGUI percentageText;
    public Button closeButton;

    public float minStepDelay = 0.3f;
    public float maxStepDelay = 1.2f;
    public float minProgressIncrement = 0.03f;
    public float maxProgressIncrement = 0.12f;

    public float dotInterval = 0.5f; // ドットの表示間隔

    private int dotCount = 0;
    private const int maxDots = 3;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseLoadingWindow);
        StartCoroutine(SimulateLoading());
        StartCoroutine(AnimateDots());
    }

    private IEnumerator SimulateLoading()
    {
        float progress = 0f;

        while (progress < 1f)
        {
            float increment = Random.Range(minProgressIncrement, maxProgressIncrement);
            progress = Mathf.Min(progress + increment, 1f);

            UpdateLoadingUI(progress);

            yield return new WaitForSeconds(Random.Range(minStepDelay, maxStepDelay));
        }

        UpdateLoadingUI(1f);
    }

    private void UpdateLoadingUI(float progress)
    {
        progressBarFill.fillAmount = progress;
        int percentage = Mathf.RoundToInt(progress * 100);
        UpdatePercentageText(percentage);
    }

    private void UpdatePercentageText(int percentage)
    {
        string dots = new string('.', dotCount);
        percentageText.text = $"{percentage}%{dots}";
    }

    private IEnumerator AnimateDots()
    {
        while (true)
        {
            dotCount = (dotCount + 1) % (maxDots + 1);
            UpdatePercentageText(Mathf.RoundToInt(progressBarFill.fillAmount * 100));
            yield return new WaitForSeconds(dotInterval);
        }
    }

    private void CloseLoadingWindow()
    {
        loadingWindow.SetActive(false);
    }
}