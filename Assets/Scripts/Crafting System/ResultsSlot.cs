using UnityEngine;
using UnityEngine.UI;

public class ResultsSlot : MonoBehaviour
{
    [SerializeField] Image image;

    private void Awake()
    {
        image.enabled = false;
    }

    public void SetSprite(Sprite sprite)
    {
        image.sprite = sprite;
        image.enabled = sprite != null;
    }
}