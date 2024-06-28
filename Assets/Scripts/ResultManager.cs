using UnityEngine;
using TMPro;

public class ResultManager : MonoBehaviour
{
    public TMP_Text lastQuestionText;
    public TMP_Text lastCorrectAnswerText;
    public TMP_Text explanationText;
    public TMP_Text scoreText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastQuestionText.text = "間違った問題: " + PlayerPrefs.GetString("LastQuestion","");
        lastCorrectAnswerText.text = "正解: " + PlayerPrefs.GetString("LastCorrectAnswer","");
        explanationText.text = "解説: " + PlayerPrefs.GetString("LastExplanation","");
        scoreText.text = "正解数: " + PlayerPrefs.GetInt("CorrectCount",0).ToString();
    }
}
