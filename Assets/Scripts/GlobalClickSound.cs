using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class GlobalClickSound : MonoBehaviour
{
    void Update()
    {
        // スペースキーのテストコードは残しておく
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key pressed");
            PlaySound();
        }

        // マウスクリックの検出を変更
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked");
            PlaySound();
        }
    }

    private void PlaySound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayClickSound();
        }
        else
        {
            Debug.LogError("SoundManager Instance is null");
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}