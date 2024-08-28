using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance;

    public Texture2D defaultCursor;
    public Texture2D hoverCursor;

    public Vector2 defaultHotSpot = Vector2.zero;
    public Vector2 hoverHotSpot = Vector2.zero;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetDefaultCursor();
    }

    public void SetDefaultCursor()
    {
        Cursor.SetCursor(defaultCursor, defaultHotSpot, CursorMode.Auto);
    }

    public void SetHoverCursor()
    {
        Cursor.SetCursor(hoverCursor, hoverHotSpot, CursorMode.Auto);
    }
}