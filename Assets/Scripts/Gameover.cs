using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public Text gameOverText;      // "繧ｲ繝ｼ繝繧ｪ繝ｼ繝舌�" 繝�く繧ｹ繝
    public Button restartButton;   // "繧ｹ繧ｿ繝ｼ繝育判髱｢縺ｫ謌ｻ繧" 繝懊ち繝ｳ
    public Text scoreText;         // 豁｣隗｣謨ｰ繧定｡ｨ遉ｺ縺吶ｋ繝�く繧ｹ繝

    void Start()
    {
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