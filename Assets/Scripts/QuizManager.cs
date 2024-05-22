using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public ChatGPT chatGPT; // ChatGPTスクリプトへの参照
    public TMP_Text questionText; // 問題を表示するテキスト
    public TMP_Text countText; // CountTextオブジェクトへの参照を追加
    private string correctAnswer; // 正解を保持するための変数
    private int correctCount = 0; // 正解数を保持する変数

    void Start()
    {
        // ChatGPTに問題を生成させる
        chatGPT.MessageSubmit("ネットワークに関する、〇か×で答えられる二者択一形式の問題を生成してください。出力は以下の形式で行ってください。\n\n問題: ネットワークに関する問題文\n正解: 〇または×");
        countText.text = "正解数: " + correctCount; // 正解数を表示
    }

    // ChatGPTから問題を受け取るメソッド
    public void ReceiveQuestion(string question)
    {
        // デバッグログで受け取ったデータを確認
        Debug.Log("Received Question: " + question);

        // 問題文と正解を分割
        string[] parts = question.Split(new[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length >= 2)
        {
            // 問題文と正解のフォーマットを確認
            string questionPart = parts[0].Trim();
            string answerPart = parts[1].Trim();

            if (questionPart.StartsWith("問題: ") && answerPart.StartsWith("正解: "))
            {
                questionText.text = questionPart.Substring("問題: ".Length).Trim();
                correctAnswer = answerPart.Substring("正解: ".Length).Trim();

                // デバッグログでパースした問題文と正解を確認
                Debug.Log("Parsed Question: " + questionText.text);
                Debug.Log("Parsed Correct Answer: " + correctAnswer);
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

    // ユーザーの回答をチェックするメソッド
    public void CheckAnswer(bool userAnswer)
    {
        //これはなにをしているの
        // 〇か×を文字列に変換
        string userAnswerText = userAnswer ? "〇" : "×";
        Debug.Log("User Answer: " + userAnswerText);
        Debug.Log("Correct Answer: " + correctAnswer);

        if (userAnswerText == correctAnswer)
        {
            Debug.Log("正解です！");
            correctCount++; // 正解数をインクリメント
            countText.text = "正解数: " + correctCount.ToString(); // 正解数を更新
        }
        else
        {
            Debug.Log("不正解です。");
        }
        Debug.Log(correctCount);
        GenerateNewQuestion(); // 新しい問題を生成
    }
    private void GenerateNewQuestion()
    {
        string prompt = "ネットワークに関する、〇か×で答えられる二者択一形式の問題を生成してください。問題文と正解(〇か×)を出力してください。";
        chatGPT.MessageSubmit(prompt);
    }
}

/*
using UnityEngine;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public ChatGPT chatGPT;
    public TMP_Text questionText;
    private string correctAnswer;
    private int correctCount = 0; // 正解数を保持する変数

    void Awake()
    {
        chatGPT = FindObjectOfType<ChatGPT>();
    }

    void Start()
    {
        GenerateNewQuestion();
    }

    public void ReceiveQuestion(string question)
    {
        // 問題文と正解を抽出する処理は省略
    }

    public void CheckAnswer(bool userAnswer)
    {
        string userAnswerText = userAnswer ? "〇" : "×";
        Debug.Log("User Answer: " + userAnswerText);
        Debug.Log("Correct Answer: " + correctAnswer);

        if (userAnswerText == correctAnswer)
        {
            Debug.Log("正解です！");
            correctCount++; // 正解数をインクリメント
        }
        else
        {
            Debug.Log("不正解です。");
        }

        GenerateNewQuestion(); // 新しい問題を生成
    }

    private void GenerateNewQuestion()
    {
        string prompt = "ネットワークに関する、〇か×で答えられる二者択一形式の問題を生成してください。問題文と正解(〇か×)を出力してください。";
        chatGPT.MessageSubmit(prompt);
    }
}
*/