using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public TMP_Text lastQuestionText;
    public TMP_Text correctAnswerText;
    public TMP_Text explanationText;
    public TMP_Text correctAnswersText;

    void Start()
    {
        correctAnswersText.text = "正解数: " + GameData.CorrectAnswers;
        lastQuestionText.text = "最後の問題: " + GameData.LastQuestion;
        correctAnswerText.text = "正解: " + GameData.LastCorrectAnswer;
        explanationText.text = "解説: " + GameData.LastExplanation;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // ゲームシーンの名前に合わせて変更してください
    }
}