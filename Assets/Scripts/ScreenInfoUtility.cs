using UnityEngine;

public static class ScreenInfoUtility
{
    public static void DisplayScreenInfo()
    {
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;
        Debug.Log($"Screen Resolution: {screenWidth} x {screenHeight}");

        Debug.Log($"Screen DPI: {Screen.dpi}");
        Debug.Log($"Device Screen Resolution: {Screen.currentResolution}");
        Debug.Log($"Is Full Screen: {Screen.fullScreen}");
    }

    public static Vector2 GetScreenDimensions()
    {
        return new Vector2(Screen.width, Screen.height);
    }
}