using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PopupWindowAnimation popupWindow;
    public GameObject titleLogo;
    public GameObject startButton;
    public GameObject CloseButton;
    public GameObject MaximiseButton;
    public GameObject MinimiseButton;

    void Start()
    {
        // タイトルロゴとスタートボタンを初期状態で非表示にする
        titleLogo.SetActive(false);
        startButton.SetActive(false);
        CloseButton.SetActive(false);
        MaximiseButton.SetActive(false);
        MinimiseButton.SetActive(false);

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
        CloseButton.SetActive(true);
        MaximiseButton.SetActive(true);
        MinimiseButton.SetActive(true);

        
    }
}