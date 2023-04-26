using System.Collections.Generic;
using UnityEngine;
using static InventoryHelper;

public class SelectionPuzzleSlotHandler : SlotHandlerBase
{
    [SerializeField] UIButton confirmButton;
    [SerializeField] SelectItemPuzzle itemPuzzle;

    private CraftingSlotUIButton selectedButton;

    protected override UIButton ActivateButton { get => confirmButton; }

    protected override List<ItemData> Inventory => InventoryManager.Instance.Inventory;

    public override void Close()
    {
        base.Close();
        selectedButton = null;
    }

    protected override void ActivateButton_OnClick(IButton obj)
    {
        if (selectedButton == null) return;

        var itemSelected = selectedButton.Item;

        if (itemSelected == null)
        {
            Debug.Log("Failed to select");
            return;
        }

        itemPuzzle.ItemToConsume = itemSelected;
        itemPuzzle.SelectItem(itemSelected.name);
        Close();
    }

    protected override void Handle_Click(IButton button)
    {
        var craftingButton = (CraftingSlotUIButton)button;
        if (!craftingButton.IsFilled) return;

        if (craftingButton.Equals(selectedButton))
        {
            craftingButton.ToggleFrame(false);
            selectedButton = null;
        }
        else if (selectedButton == null)
        {
            craftingButton.ToggleFrame(true);
            selectedButton = craftingButton;
        }
        else
        {
            selectedButton.ToggleFrame(false);
            selectedButton = craftingButton;
            craftingButton.ToggleFrame(true);
        }
    }
}
