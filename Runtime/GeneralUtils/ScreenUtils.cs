using UnityEngine;

public static class ScreenUtils
{
    public readonly static Vector2 lowerLeftPoint = Vector2.zero;
    public readonly static Vector2 lowerRightPoint = new Vector2(Screen.width, 0);
    public readonly static Vector2 upperLeftPoint = new Vector2(0, Screen.height);
    public readonly static Vector2 upperRightPoint = new Vector2(Screen.width, Screen.height);
    public readonly static Vector2 center = new Vector2(Screen.width / 2, Screen.height / 2);
}
