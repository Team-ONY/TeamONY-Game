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
    public QuizManager quizManager;
    private List<MessageModel> communicationHistory = new List<MessageModel>();

    void Start()
    {
        chatGPTResponseText = GameObject.Find("ChatGPTResponseText").GetComponent<TMP_Text>();
        string prompt = "ネットワークに関する、〇か×で答えられる二者択一形式の問題を1つだけ生成してください。複数の問題は絶対に生成しないでください。以下の形式で厳密に出力してください：\n\n問題: [ここに1つの問題文を入れてください]\n正解: [〇または×]\n解説: [ここに解説を入れてください]";
        MessageSubmit(prompt);
    }

    private void Communication(string newMessage, Action<MessageModel> result)
    {
        var messages = new List<MessageModel>
    {
        new MessageModel
        {
            role = "user",
            content = newMessage
        }
    };

        var requestBody = new CompletionRequestModel
        {
            model = "gpt-3.5-turbo",
            messages = messages
        };

        var jsonOptions = JsonUtility.ToJson(requestBody);
        Debug.Log("Sending JSON: " + jsonOptions);

        var apiUrl = "https://830f-118-20-242-58.ngrok-free.app/api/openai"; // ngrokのURLを適切に変更してください
        var headers = new Dictionary<string, string>
    {
        {"Content-type", "application/json"},
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
            Debug.Log($"Response Code: {operation.webRequest.responseCode}");
            Debug.Log($"Response Headers: {operation.webRequest.GetResponseHeaders()}");
            Debug.Log($"Response Body: {operation.webRequest.downloadHandler.text}");

            if (operation.webRequest.result == UnityWebRequest.Result.ConnectionError ||
                operation.webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                string errorCode = operation.webRequest.responseCode.ToString();
                string errorMessage = operation.webRequest.error.ToString();
                Debug.LogError($"API Error: Code {errorCode}, Message: {errorMessage}");
                Debug.LogError($"Response Body: {operation.webRequest.downloadHandler.text}");
                chatGPTResponseText.text = $"Error: {errorMessage}";
            }
            else
            {
                var responseString = operation.webRequest.downloadHandler.text;
                try
                {
                    var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                    if (responseObject.choices != null && responseObject.choices.Length > 0 && responseObject.choices[0].message != null)
                    {
                        var responseMessage = responseObject.choices[0].message;
                        Debug.Log("Response: " + responseMessage.content);
                        chatGPTResponseText.text = responseMessage.content;
                        quizManager.ReceiveQuestion(responseMessage.content);
                        result(responseMessage);
                    }
                    else
                    {
                        Debug.LogError("Invalid response structure");
                        chatGPTResponseText.text = "Error: Invalid response structure";
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"Failed to parse response: {e.Message}");
                    Debug.LogError($"Response string: {responseString}");
                    chatGPTResponseText.text = "Error: Failed to parse response";
                }
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