using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public ChatGPT chatGPT;
    public TMP_Text questionText;
    public TMP_Text countText;
    private string correctAnswer;
    private int correctCount = 0;
    private bool gameOver = false;
    private string currentQuestion;
    private string currentExplanation;
    public TimeCounter timeCounter;
    public SoundManager soundManager;

    void Start()
    {
        chatGPT.MessageSubmit("ネットワークに関する、〇か×で答えられる二者択一形式の問題を生成してください。出力は以下の形式で行ってください。\n\n問題: ネットワークに関する問題文\n正解: 〇または×");
        countText.text = "正解数: " + correctCount;
    }

    public void ReceiveQuestion(string question)
    {
        if (!gameOver)
        {
            string[] parts = question.Split('\n');

            string questionPart = "";
            string answerPart = "";
            string explanationPart = "";

            foreach (string part in parts)
            {
                if (part.StartsWith("問題:"))
                    questionPart = part.Substring("問題:".Length).Trim();
                else if (part.StartsWith("正解:"))
                    answerPart = part.Substring("正解:".Length).Trim();
                else if (part.StartsWith("解説:"))
                    explanationPart = part.Substring("解説:".Length).Trim();
            }

            if (!string.IsNullOrEmpty(questionPart) && !string.IsNullOrEmpty(answerPart))
            {
                currentQuestion = questionPart;
                questionText.text = currentQuestion;
                correctAnswer = answerPart;
                currentExplanation = explanationPart;

                Debug.Log("Question parsed: " + currentQuestion);
                Debug.Log("Answer parsed: " + correctAnswer);
                Debug.Log("Explanation parsed: " + currentExplanation);
            }
            else
            {
                Debug.LogError("Failed to parse question: " + question);
            }
        }
    }

    public void CheckAnswer(bool userAnswer)
    {
        if (gameOver) return;

        string userAnswerText = userAnswer ? "〇" : "×";
        Debug.Log("ユーザーの回答: " + userAnswerText);
        Debug.Log("正解: " + correctAnswer);
        if (userAnswerText == correctAnswer)
        {
            Debug.Log("正解です！");
            correctCount++;
            GameData.CorrectAnswers = correctCount;
            countText.text = "正解数: " + correctCount.ToString();
            questionText.text = "正解です！";

            if (soundManager != null)
            {
                Debug.Log("PlayCorrectSound called");
                soundManager.PlayCorrectSound();
            }
        }
        else
        {
            Debug.Log("不正解です。");
            gameOver = true;
            Debug.Log("ゲームオーバー");
            questionText.text = "不正解です。ゲームオーバー";

            // データを保存
            GameData.LastQuestion = currentQuestion;
            GameData.LastCorrectAnswer = correctAnswer;
            GameData.LastExplanation = currentExplanation;

            Debug.Log("Last Question: " + GameData.LastQuestion);
            Debug.Log("Last Correct Answer: " + GameData.LastCorrectAnswer);
            Debug.Log("Last Explanation: " + GameData.LastExplanation);

            SceneManager.LoadScene("GameOver");
        }

        if (!gameOver) // ゲームオーバーでない場合のみ新しい問題を生成する
        {
            GenerateNewQuestion();
        }
    }

    private void GenerateNewQuestion()
    {
        Debug.Log("GenerateNewQuestion called");
        string prompt = "ネットワークに関する、〇か×で答えられる二者択一形式の問題を1つだけ生成してください。複数の問題は絶対に生成しないでください。以下の形式で厳密に出力してください：\n\n問題: [ここに1つの問題文を入れてください]\n正解: [〇または×]\n解説: [ここに解説を入れてください]";
        chatGPT.MessageSubmit(prompt);
        timeCounter.ResetTimer();
    }
}