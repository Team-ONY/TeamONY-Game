using UnityEngine;
using UnityEngine.EventSystems;

public class ClickSoundHandler : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            SoundManager.Instance.PlayClickSound();
        }
    }
}