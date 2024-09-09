using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class LoadingManager : MonoBehaviour
{   
    public GameObject loadingWindow;
    public GameObject LoadingPanel; // パネルオブジェクト
    public Image progressBarFill;
    public TextMeshProUGUI percentageText;
    public Button closeButton;
    public Button OkayButton; // ボタンオブジェクト

    public float minStepDelay = 0.3f;
    public float maxStepDelay = 1.2f;
    public float minProgressIncrement = 0.03f;
    public float maxProgressIncrement = 0.12f;

    public float dotInterval = 0.5f; // ドットの表示間隔

    private int dotCount = 0;
    private const int maxDots = 3;
    private bool isButtonClicked = false; // ボタンがクリックされたかどうかを管理するフラグ

    private void Start()
    {
        LoadingPanel.SetActive(false); // パネルを最初に非表示にする
        closeButton.onClick.AddListener(CloseLoadingWindow);
        OkayButton.onClick.AddListener(OnOkayButtonClick); // ボタンがクリックされたときの処理を追加

        StartCoroutine(WaitForButtonClick()); // ボタンがクリックされるのを待つコルーチンを開始
    }

    private void OnOkayButtonClick()
    {
        isButtonClicked = true; // フラグをtrueにして、ボタンがクリックされたことを示す
        LoadingPanel.SetActive(true); // パネルを表示する
    }

    private IEnumerator WaitForButtonClick()
    {
        yield return null; // 1フレーム待機する
        // ボタンがクリックされるまで待機する
        yield return new WaitUntil(() => isButtonClicked);
        
        // パネルが表示されるのを待つ
        yield return new WaitForSeconds(0.1f); 

        // ボタンがクリックされたらロード処理を開始
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