using UnityEngine;

public class TitleUIManager : MonoBehaviour
{
    public PopupWindowAnimation popupWindow;
    public GameObject titleLogo;
    public GameObject startButton;
    public GameObject CloseButton;
    public GameObject HelpButton;

    void Start()
    {
        // タイトルロゴとスタートボタンを初期状態で非表示にする
        titleLogo.SetActive(false);
        startButton.SetActive(false);
        CloseButton.SetActive(false);
        HelpButton.SetActive(false);

        // ポップアップウィンドウのアニメーションを開始
        popupWindow.StartAnimation(OnPopupAnimationComplete);
    }

    // ポップアップアニメーション完了時のコールバック
    private void OnPopupAnimationComplete()
    {
        // タイトルロゴとスタートボタンを表示
        titleLogo.SetActive(true);
        startButton.SetActive(true);
        CloseButton.SetActive(true);
        HelpButton.SetActive(true);

        // タイトルロゴのアニメーションを開始（必要に応じて）
        TitleLogoAnimation titleLogoAnim = titleLogo.GetComponent<TitleLogoAnimation>();
        if (titleLogoAnim != null)
        {
            titleLogoAnim.StartLogoAnimation();
        }
    }
}