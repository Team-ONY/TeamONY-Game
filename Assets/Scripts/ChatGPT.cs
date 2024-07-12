using System;
using System.Collections;
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
        public bool stream;
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
    public QuizManager quizManager; // QuizManagerスクリプトへの参�?�
    private readonly string apiKey = "API key"; // APIキー
    private List<MessageModel> communicationHistory = new List<MessageModel>();

    void Start()
    {
        chatGPTResponseText = GameObject.Find("ChatGPTResponseText").GetComponent<TMP_Text>();
        //chatGPTResponseText.font = Resources.Load<TMP_FontAsset>("YourJapaneseFont"); // フォントを設�?

        // 問題生成用のプロンプト
        string prompt = "ネットワークの問題を生�?�してください。問題文と正解?���?か×）を出力してください�?";

        // ChatGPTに問題を生�?�させる
        MessageSubmit(prompt);
    }

    //private void Communication(string newMessage, Action<MessageModel> result)
    private IEnumerator Communication(string newMessage)
    {
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
                messages = communicationHistory,
                stream = true
            }, true);

        var headers = new Dictionary<string, string>
        {
            {"Authorization", "Bearer " + apiKey},
            {"Content-type", "application/json"},
            //{"X-Slack-No-Retry", "1"}
        };

        // 追�?
        yield return StartCoroutine(SendRequest(apiUrl, jsonOptions, headers));
        /*
        // Webリクエスト�?��?
        var request = new UnityWebRequest(apiUrl, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonOptions)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        // ヘッダーの設�?
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        // Webリクエスト�?�送信
        var operation = request.SendWebRequest();

        operation.completed += _ =>
        {
            // Webリクエスト�?��?のエラー処�?
            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                string errorCode = operation.webRequest.responseCode.ToString();
                string errorMessage = operation.webRequest.error.ToString();
                Debug.LogError(operation.webRequest.error);
                chatGPTResponseText.text = errorMessage;
                throw new Exception();
            }
            // Webリクエスト�?��?の成功処�?
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                Debug.Log("Response: " + responseObject.choices[0].message.content);
                chatGPTResponseText.text = responseObject.choices[0].message.content;

                // QuizManagerに問題を送信
                quizManager.ReceiveQuestion(responseObject.choices[0].message.content);
            }
            request.Dispose();
        };
        */
    }

    //追�?
    private IEnumerator SendRequest(string url, string json, Dictionary<string, string> headers)
    {
        /*
        var request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };
        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }
        var operation = request.SendWebRequest();
        while(!operation.isDone)
        {
            yield return null;
        }
        if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
            operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            string errorCode = operation.webRequest.responseCode.ToString();
            string errorMessage = operation.webRequest.error.ToString();
            Debug.LogError(operation.webRequest.error);
            chatGPTResponseText.text = errorMessage;
            throw new Exception();
        }
        else
        {
            var responseStream = operation.webRequest.downloadHandler.text;
            StartCoroutine(DisplayText(responseStream));
        }

        request.Dispose();
        */

        // 追�?
        var request = new UnityWebRequest(url, "POST")
        {
            uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json)),
            downloadHandler = new DownloadHandlerBuffer()
        };

        foreach (var header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        var operation = request.SendWebRequest();

        while (!operation.isDone)
        {
            yield return null;
        }

        if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
            operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            string errorCode = operation.webRequest.responseCode.ToString();
            string errorMessage = operation.webRequest.error.ToString();
            Debug.LogError(operation.webRequest.error);
            chatGPTResponseText.text = errorMessage;
            throw new Exception(errorMessage);
        }
        else
        {
            var responseStream = operation.webRequest.downloadHandler.text;
            yield return StartCoroutine(DisplayText(responseStream));
        }

        request.Dispose();
    }

    private IEnumerator DisplayText(string text){
        chatGPTResponseText.text = "";
        foreach(char c in text)
        {
            chatGPTResponseText.text += c;
            yield return new WaitForSeconds(0.05f);
        }
        //完�?�なメ�?セージをQuizManagerに送信
        quizManager.ReceiveQuestion(chatGPTResponseText.text);
    }
    public void MessageSubmit(string sendMessage)
    {
        //追�?
        /*
        Communication(sendMessage, (result) =>
        {
            Debug.Log(result.content);
        });
        */
        StartCoroutine(Communication(sendMessage));
    }
}