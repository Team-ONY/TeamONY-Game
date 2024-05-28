/*
░█████╗░███╗░░██╗██╗░░░██╗  ░██████╗░░█████╗░███╗░░░███╗███████╗
██╔══██╗████╗░██║╚██╗░██╔╝  ██╔════╝░██╔══██╗████╗░████║██╔════╝
██║░░██║██╔██╗██║░╚████╔╝░  ██║░░██╗░███████║██╔████╔██║█████╗░░
██║░░██║██║╚████║░░╚██╔╝░░  ██║░░╚██╗██╔══██║██║╚██╔╝██║██╔══╝░░
╚█████╔╝██║░╚███║░░░██║░░░  ╚██████╔╝██║░░██║██║░╚═╝░██║███████╗
░╚════╝░╚═╝░░╚══╝░░░╚═╝░░░  ░╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝╚══════╝
*/

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
    public QuizManager quizManager; // QuizManagerスクリプトへの参照
    private List<MessageModel> communicationHistory = new List<MessageModel>();

    void Start()
    {
        chatGPTResponseText = GameObject.Find("ChatGPTResponseText").GetComponent<TMP_Text>();
        string prompt = "ネットワークの問題を生成してください。問題文と正解（〇か×）を出力してください。";
        MessageSubmit(prompt);
    }

    private void Communication(string newMessage, Action<MessageModel> result)
    {
        communicationHistory.Add(new MessageModel()
        {
            role = "user",
            content = newMessage
        });


        //GOで開いたAPIのURLを指定
        var apiUrl = "http://localhost:3000/api/openai"; // バックエンドのURLに変更
        var jsonOptions = JsonUtility.ToJson(
            new CompletionRequestModel()
            {
                model = "gpt-3.5-turbo",
                messages = communicationHistory,
            }, true);

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
                var responseString = operation.webRequest.downloadHandler.text;
                var responseObject = JsonUtility.FromJson<ChatGPTRecieveModel>(responseString);
                communicationHistory.Add(responseObject.choices[0].message);
                Debug.Log("Response: " + responseObject.choices[0].message.content);
                chatGPTResponseText.text = responseObject.choices[0].message.content;
                quizManager.ReceiveQuestion(responseObject.choices[0].message.content);
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