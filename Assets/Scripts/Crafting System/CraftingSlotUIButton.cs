using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using static InventoryHelper;

public class CraftingSlotUIButton : UIButton
{
    [SerializeField] Image image;
    [SerializeField] Animator animator;
    [SerializeField] Color frameSelected;

    Color frameStart;
    public ItemData item;

    public ItemData Item => item;
    public bool IsFilled => item != null;

    private void Awake()
    {
        //frameStart = frame.color;
        image.sprite = null;
    }

    public void ToggleFrame(bool selected)
    {
        //frame.color = selected ? frameSelected : frameStart;
        animator.enabled = selected;
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
