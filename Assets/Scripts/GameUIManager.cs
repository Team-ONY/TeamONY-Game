using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    // バツボタン(右上)関連のUI
    public Button closeButton;
    public Button mysteryButton;

    // ポーズ画面関連のUI
    public GameObject pauseMenuPanel;
    public Button onPanelCloseButton;   // onPanelCloseButtonはPanelの子オブジェクト
    public Button onPausePopupResumeButton;
    public Button onPausePopupReturnTitleButton;

    // 謎のポップアップ画面関連のUI
    public GameObject mysteryPopupWindow;
    public Button onMysteryPopupCloseButton;
    public Button onMysteryPopupCancelButton;
    public Button onMysteryPopupErrorButton;

    // 効果音
    public AudioClip mysteryPopupSound; // 効果音のAudioClip
    private AudioSource audioSource;    // AudioSourceコンポーネント

    void Start()
    {
        // 効果音
        audioSource = GetComponent<AudioSource>();

        // 初期状態でポーズメニューを非表示にする
        pauseMenuPanel.SetActive(false);
        closeButton.onClick.AddListener(TogglePauseMenu);
        onPanelCloseButton.onClick.AddListener(TogglePauseMenu);
        onPausePopupResumeButton.onClick.AddListener(TogglePauseMenu);
        onPausePopupReturnTitleButton.onClick.AddListener(ReturnToMainMenu);

        // 初期状態で謎のポップアップ画面を非表示にする
        mysteryPopupWindow.SetActive(false);
        mysteryButton.onClick.AddListener(ToggleMysteryPopupWindow);
        onMysteryPopupCloseButton.onClick.AddListener(CloseMysteryPopupWindow);
        onMysteryPopupCancelButton.onClick.AddListener(CancelMysteryPopupWindow);
        onMysteryPopupErrorButton.onClick.AddListener(ResetQuizTimer);
    }

    void Update()
    {
        // エスケープキーでもポーズメニューを切り替えられるようにする
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
    }

    // バツボタン(右上)を押すとゲームがポーズされる
    public void TogglePauseMenu()
    {
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        Time.timeScale = pauseMenuPanel.activeSelf ? 0 : 1; // ポーズ中は時間を止める
    }

    // ゲーム再開
    private void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    // タイトルに戻る
    private void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start");
    }
    public void ToggleMysteryPopupWindow()
    {
        Debug.Log("ToggleMysteryPopupWindowメソッドが呼ばれました");
        mysteryPopupWindow.SetActive(true);

        // 効果音の再生
        if (audioSource != null && mysteryPopupSound != null)
        {
            audioSource.PlayOneShot(mysteryPopupSound);
        }
    }
    private void CloseMysteryPopupWindow()
    {
        mysteryPopupWindow.SetActive(false);
    }
    private void CancelMysteryPopupWindow()
    {
        mysteryPopupWindow.SetActive(false);
    }
    private void ResetQuizTimer()
    {
        // クイズのタイマーをリセットする
        Debug.Log("ResetQuizTimerメソッドが呼ばれました");

        //TimerCounterのコンポーネントのインスタンスを取得
        TimeCounter timeCounter = FindFirstObjectByType<TimeCounter>();
        if (timeCounter != null)
        {
            timeCounter.ResetTimer();
            mysteryPopupWindow.SetActive(false);
        }
        else
        {
            Debug.LogError("TimerCounterコンポーネントが見つかりません");
        }
    }
}