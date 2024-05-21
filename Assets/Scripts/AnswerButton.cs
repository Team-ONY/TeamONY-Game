using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    public bool isTrue; // 〇ボタンかどうかを識別するフラグ
    public QuizManager quizManager; // QuizManagerスクリプトへの参照

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        // ボタンがクリックされたときの処理
        quizManager.CheckAnswer(isTrue);
    }
}
