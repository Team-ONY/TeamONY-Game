using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class WindowDelayEffect : MonoBehaviour
{       
    public GameObject uiObject;  // 表示するUIオブジェクト
    public float delay = 2.0f;   // 表示までの遅延時間

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(ShowUIAfterDelay());
    }

    IEnumerator ShowUIAfterDelay()
    {
        yield return new WaitForSeconds(delay); // 遅延
        uiObject.SetActive(true); // UIを表示
    }
}
