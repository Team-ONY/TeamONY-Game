using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    public TMP_Text titleText;
    public string fullText;
    public float typingSpeed = 0.1f;

    private void Start(){
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText(){
        titleText.text = "";
        foreach(char letter in fullText.ToCharArray()){
            titleText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}