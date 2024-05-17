/*
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Text;

public class CallOpenAIAPI : MonoBehaviour
{
    private string apiKey = "sk-proj-4dGRRMLBooF12IjcmJLnT3BlbkFJGKPMpkLQjGrz3dFfUJNJ"; // OpenAI APIキーを入力
    private string prompt = "Hello, I'd like you to act as a helpful assistant."; // プロンプトを入力
    private string model = "text-davinci-003"; // 使用するモデルを指定

    void Start()
    {
        Debug.Log("ここまできた");
        StartCoroutine(SendRequest(prompt));
    }

    IEnumerator SendRequest(string prompt)
    {
        // リクエストURLを作成
        string url = "https://api.openai.com/v1/chat/completions";

        // リクエストボディを作成
        string jsonBody = "{\"model\":\"" + model + "\",\"prompt\":\"" + prompt + "\",\"max_tokens\":50}";

        // UnityWebRequestを作成
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            // リクエストボディを設定
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);

            // ヘッダーを設定
            request.SetRequestHeader("Authorization", "Bearer " + apiKey);
            request.SetRequestHeader("Content-Type", "application/json");

            // リクエストを送信
            yield return request.SendWebRequest();

            // レスポンスを処理
            if (request.result == UnityWebRequest.Result.Success)
            {
                // レスポンスの文字列を取得
                string responseText = request.downloadHandler.text;
                Debug.Log("Response: " + responseText);
            }
            //失敗したときの
            else
            {
                Debug.Log("エラああああああああああああ");
                Debug.LogError("Error: " + request.responseCode + " - " + request.error);
            }
        }
    }
}
*/