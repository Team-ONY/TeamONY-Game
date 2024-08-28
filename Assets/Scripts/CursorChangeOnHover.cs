using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChangeOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        CursorManager.Instance.SetHoverCursor();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CursorManager.Instance.SetDefaultCursor();
    }
}