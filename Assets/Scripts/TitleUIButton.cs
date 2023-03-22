using UnityEngine;

public class TitleUIButton : MenuUIButton
{
    [SerializeField] GameObject starImage;

    public override void ToggleSelected(bool isSelected)
    {
        starImage.SetActive(isSelected);
    }
}