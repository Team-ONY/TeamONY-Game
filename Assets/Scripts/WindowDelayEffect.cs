using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WindowDelayEffect : MonoBehaviour
{
    public GameObject targetUI;  // 表示したいUIオブジェクト
    public float delay = 2.0f;   // 表示までの遅延時間

    void Awake()
    {
        StartCoroutine(ShowUIAfterDelay());
    }

    IEnumerator ShowUIAfterDelay()
    {
        yield return new WaitForSeconds(delay); // 遅延
        targetUI.SetActive(true); // UIを表示
    }
}
