// ChatGPTに問題生成させる(p1)
// どちらか(true or false)を選んだかをAIに問い合わせる
// 不正解だったときの解答用の文字を出力(p2)
//
//
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class ChatGPT : MonoBehaviour
{
    [System.Serializable]
    public class MessageModel
    {
        public string role;
        public string content;
    }

    [System.Serializable]
    public class CompletionRequestModel
    {
        public string model;
        public List<MessageModel> messages;
    }

    [System.Serializable]
    public class ChatGPTRecieveModel
    {
        public string id;
        public string @object;
        public int created;
        public Choice[] choices;
        public Usage usage;

        [System.Serializable]
        public class Choice
        {
            public int index;
            public MessageModel message;
            public string finish_reason;
        }

        [System.Serializable]
        public class Usage
        {
            public int prompt_tokens;
            public int completion_tokens;
            public int total_tokens;
        }
    }

    public TMP_Text chatGPTResponseText;

    private MessageModel assistantModel = new MessageModel
    {
        role = "system",
        content = "あなたはネットワークのスペシャリストです。私にネットワークについての問題を出してください。また解答もつけてください"
    };

    private readonly string apiKey = "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";
    private List<MessageModel> communicationHistory = new List<MessageModel>();

    void Start()
    {
        chatGPTResponseText = GameObject.Find("ChatGPTResponseText").GetComponent<TMP_Text>();
        chatGPTResponseText.font = Resources.Load<TMP_FontAsset>("YourJapaneseFont"); // フォントを設定

        communicationHistory.Add(assistantModel);
        MessageSubmit("私にネットワークについての問題を正しいか正しくないかで答えられる問題を一つ出してください。");
    }

    private void Communication(string newMessage, Action<MessageModel> result)
    {
        Debug.Log(newMessage);
        communicationHistory.Add(new MessageModel()
        {
            role = "user",
            content = newMessage
        });

        var apiUrl = "https://api.openai.com/v1/chat/completions";
        var jsonOptions = JsonUtility.ToJson(
            new CompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = communicationHistory
            }, true);

        var headers = new Dictionary<string, string>
        {
            {"Authorization", "Bearer " + apiKey},
            {"Content-type", "application/json"},
            {"X-Slack-No-Retry", "1"}
        };

        var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(operation.webRequest.error);
                throw new Exception();
            }
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                Debug.Log(responseObject.choices[0].message.content);
                chatGPTResponseText.text = responseObject.choices[0].message.content;
            }
            request.Dispose();
        };
    }

    public void MessageSubmit(string sendMessage)
    {
        Communication(sendMessage, (result) =>
        {
            Debug.Log(result.content);
        });
    }
}
