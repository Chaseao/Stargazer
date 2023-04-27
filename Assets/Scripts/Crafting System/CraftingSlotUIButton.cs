using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using static InventoryHelper;

public class CraftingSlotUIButton : UIButton
{
    [SerializeField] Image image;
    [SerializeField] GameObject selection;

    public ItemData item;

    public ItemData Item => item;
    public bool IsFilled => item != null;

    private void Awake()
    {
        image.sprite = null;
        ToggleFrame(isSelected);
    }

    public void ToggleFrame(bool selected)
    {
        isSelected = selected;
        if (selection != null)
        {
            selection.SetActive(isSelected);
        }
    }

    public void SetItem(ItemData item)
    {
        this.item = item;
        image.sprite = item?.itemImage;
        image.enabled = image.sprite != null;
    }

    public override void Use()
    {
        if(item == null) return;
    }
}
