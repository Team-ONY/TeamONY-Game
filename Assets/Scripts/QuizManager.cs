using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public ChatGPT chatGPT;
    public TMP_Text questionText;
    private string currentExplanation;
    public TMP_Text countText;
    private string correctAnswer;
    private int correctCount = 0;
    private bool gameOver = false;

    void Start()
    {
        chatGPT.MessageSubmit("ネットワークに関する、〇か×で答えられる二者択一形式の問題を生成してください。出力は以下の形式で行ってください。\n\n問題: ネットワークに関する問題文\n正解: 〇または×");
        countText.text = "正解数: " + correctCount;
    }

    public void ReceiveQuestion(string question)
    {
        if (!gameOver) // ゲームオーバーでない場合にのみ問題を受け取る
        {
            string[] parts = question.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 3)
            {
                string questionPart = parts[0].Trim();
                string answerPart = parts[1].Trim();
                string explanationPart = parts[2].Trim();

                if (questionPart.StartsWith("問題: ") && answerPart.StartsWith("正解: ") && explanationPart.StartsWith("解説: "))
                {
                    questionText.text = questionPart.Substring("問題: ".Length).Trim();
                    correctAnswer = answerPart.Substring("正解: ".Length).Trim();
                    currentExplanation = explanationPart.Substring("解説: ".Length).Trim();
                }
                else
                {
                    Debug.LogError("Received data is not in the expected format.");
                }
            }
            else
            {
                Debug.LogError("Received data does not contain both question and answer.");
            }
        }
    }

    public void CheckAnswer(bool userAnswer)
    {
        if (gameOver) return;

        string userAnswerText = userAnswer ? "〇" : "×";
        Debug.Log("ユーザーの回答: " + userAnswerText);
        Debug.Log("正解: " + correctAnswer);
        Debug.Log("解説: " + currentExplanation);

        if (userAnswerText == correctAnswer)
        {
            Debug.Log("正解です！");
            correctCount++;
            countText.text = "正解数: " + correctCount.ToString();
        }
        else
        {
            Debug.Log("不正解です。");
            gameOver = true;
            Debug.Log("ゲームオーバー");
            questionText.text = "不正解です。ゲームオーバー";

            PlayerPrefs.SetString("LastExplanation", currentExplanation);
            PlayerPrefs.SetString("LastQuestion", questionText.text);
            PlayerPrefs.SetString("LastCorrectAnswer", correctAnswer);
            PlayerPrefs.SetInt("CorrectCount", correctCount);
            PlayerPrefs.Save();

            SceneManager.LoadScene("GameOverScene");
        }

        if (!gameOver) // ゲームオーバーでない場合のみ新しい問題を生成する
        {
            GenerateNewQuestion();
        }
    }

    private void GenerateNewQuestion()
    {
        string prompt = "ネットワークに関する、〇か×で答えられる二者択一形式の問題を生成してください。問題文、正解(〇か×)、および解説を出力してください。";
        chatGPT.MessageSubmit(prompt);
    }
}