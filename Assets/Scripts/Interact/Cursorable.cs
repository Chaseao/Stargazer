using UnityEngine;
using UnityEngine.EventSystems;

public class Cursorable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] CursorOption cursorOption;
    private static int totalCursables;

    public void SetCursor(CursorOption cursorOption)
    {
        this.cursorOption = cursorOption;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(totalCursables == 0)
        {
            cursorOption.InteractiveCursorTexture();
        }
        totalCursables++;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        totalCursables--;
        if(totalCursables == 0)
        {
            cursorOption.DefaultCursorTexture();
        }
    }
}
