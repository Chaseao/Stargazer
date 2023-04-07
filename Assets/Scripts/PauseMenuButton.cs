using UnityEngine;
using UnityEngine.UI;

public class PauseMenuButton : EventButton
{
    [SerializeField] Image selectionMaker;

    private void Start()
    {
        selectionMaker.enabled = false;
    }

    public override void ToggleSelected(bool isSelected)
    {
        this.isSelected = isSelected;
        selectionMaker.enabled = isSelected;
    }
}