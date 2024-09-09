using System.Collections;  // これを追加
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;   //追加:Imageを扱うために必要

public class SceneController : MonoBehaviour
{
    /*追加したもの 2024/08/27
    [SerializeField] Fade fade;
    fade.fadeIn(時間,() => 完了した時にやりたいこと)
    */
    [SerializeField] Fade fade;
    [SerializeField] Image fillImage;   //追加:ProgressBarのFillイメージへの参照

    private bool isTransitioning = false; //シーン遷移中かどうかを確認するフラグ

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
       if (!isTransitioning)
        {
            StartCoroutine(WaitForFillAndLoadScene());  // Fillが満タンになるのを待つコルーチンを開始
        }
    }

    public void LoadResultScene()
    {
        SceneManager.LoadScene("Result");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    // 追加: Fillが満タンになるのを待ってからフェードインとシーン遷移を行うコルーチン
    private IEnumerator WaitForFillAndLoadScene()
    {
        yield return new WaitUntil(() => fillImage.fillAmount >= 1f);  // Fillが満タンになるのを待つ

        isTransitioning = true;  // シーン遷移中にフラグを立てる
        fade.FadeIn(3f, () => SceneManager.LoadScene("Main"));  // フェードインしてからシーンをMainに移動
    }
}