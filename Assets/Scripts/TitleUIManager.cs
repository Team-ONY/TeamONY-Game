using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopupWindowAnimation popupWindow;
    public GameObject titleLogo;
    public GameObject startButton;

    void Start()
    {
        // タイトルロゴとスタートボタンを初期状態で非表示にする
        titleLogo.SetActive(false);
        startButton.SetActive(false);

        // ポップアップウィンドウのアニメーションを開始
        popupWindow.StartAnimation(OnPopupAnimationComplete);
    }

    // ポップアップアニメーション完了時のコールバック
    private void OnPopupAnimationComplete()
    {
        // タイトルロゴとスタートボタンを表示
        titleLogo.SetActive(true);
        // タイトルロゴのアニメーションを開始（必要に応じて）
        TitleLogoAnimation titleLogoAnim = titleLogo.GetComponent<TitleLogoAnimation>();
        if (titleLogoAnim != null)
        {
            titleLogoAnim.StartLogoAnimation();
        }
        
        startButton.SetActive(true);

        
    }
}