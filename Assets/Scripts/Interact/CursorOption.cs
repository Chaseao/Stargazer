using UnityEngine;

[CreateAssetMenu(menuName = "SO/Cursor Option", fileName = "New Cursor Option")]
public class CursorOption : ScriptableObject
{
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D hoverCursor;

    public void InteractiveCursorTexture()
    {
        Vector2 hotspot = new Vector2(hoverCursor.width / 2, 0);
        Cursor.SetCursor(hoverCursor, hotspot, CursorMode.Auto);
    }

    public void DefaultCursorTexture()
    {
        if (defaultCursor == null)
        {
            Cursor.SetCursor(default, default, default);
        }
        else
        {
            Vector2 hotspot = new Vector2(defaultCursor.width / 2, 0);
            Cursor.SetCursor(defaultCursor, hotspot, CursorMode.Auto);
        }

    }
}