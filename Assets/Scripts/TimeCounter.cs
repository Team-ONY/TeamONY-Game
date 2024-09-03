using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class TimeCounter : MonoBehaviour
{
    public int countdownSeconds = 11;
    private float remainingSeconds;
    private TMP_Text timeText;

    private void Start()
    {
        InitializeTimer();
    }

    private void InitializeTimer()
    {
        GameObject countObject = GameObject.Find("TimeText");

        if (countObject == null)
        {
            Debug.LogError("TimeTextという名前のオブジェクトが見つかりません。");
            return;
        }

        timeText = countObject.GetComponent<TMP_Text>();

        if (timeText == null)
        {
            Debug.LogError("TimeTextオブジェクトにTMP_Textコンポーネントが見つかりません。");
            return;
        }

        ResetTimer();
    }

    public void ResetTimer()
    {
        remainingSeconds = countdownSeconds;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (timeText == null) return;

        remainingSeconds -= Time.deltaTime;
        UpdateTimerDisplay();

        if (remainingSeconds <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    private void UpdateTimerDisplay()
    {
        var span = new TimeSpan(0, 0, (int)remainingSeconds);
        timeText.text = span.ToString(@"mm\:ss");
    }
}