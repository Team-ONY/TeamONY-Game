using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    /*追加したもの 2024/08/27
    [SerializeField] Fade fade;
    fade.fadeIn(時間,() => 完了した時にやりたいこと)
    */
    [SerializeField] Fade fade;
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void LoadExplainScene()
    {
        SceneManager.LoadScene("GameManualScene");
    }
    public void LoadGameScene()
    {
        fade.FadeIn(1f,() => SceneManager.LoadScene("Main"));
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene("Result");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}