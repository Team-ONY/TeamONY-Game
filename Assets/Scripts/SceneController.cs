using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadTitleScene()
    {
        SceneManager.LoadScene("Start");
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("Main");
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