using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using static InventoryHelper;

public class CraftingSlotUIButton : UIButton
{
    [SerializeField] Image image;
    [SerializeField] Image frame;
    [SerializeField] Color frameSelected;

    Color frameStart;
    ItemData item;

    public ItemData Item => item;
    public bool IsFilled => item != null;

    private void Awake()
    {
        frameStart = frame.color;
    }

    public void ToggleFrame(bool selected)
    {
        frame.color = selected ? frameSelected : frameStart;
    }

    public void SetItem(ItemData item)
    {
        this.item = item;
        image.sprite = item?.itemImage;
    }

    public override void Use()
    {
        if(item == null) return;

        Debug.Log("Selected: " + item.name);
    }
}
