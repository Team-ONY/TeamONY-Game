using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text gameOverText;      // "ゲームオーバー" テキスト
    public Button restartButton;   // "スタート画面に戻る" ボタン
    public Text scoreText;         // 正解数を表示するテキスト

    void Start()
    {
        // UI要素を表示する
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        // スコアを外部から受け取る (例：PlayerPrefsから取得)
        int score = PlayerPrefs.GetInt("Score", 0);
        scoreText.text = "Score: " + score;
    }

    public void OnRestartButton()
    {
        // スタート画面に戻る処理（スタート画面のシーン名を指定）
        SceneManager.LoadScene("StartScene");
    }
}