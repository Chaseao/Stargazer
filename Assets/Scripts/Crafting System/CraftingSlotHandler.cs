using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static InventoryHelper;

public class CraftingSlotHandler : SlotHandlerBase
{
    [SerializeField] ResultsSlot results;
    [SerializeField] CraftButton craftButton;
    [SerializeField] CraftingResultsTable resultsTable;

    private CraftingSlotUIButton firstItem;
    private CraftingSlotUIButton secondItem;

    protected override CraftButton CraftButton { get => craftButton; }

    public override void Close()
    {
        base.Close();
        firstItem = null;
        secondItem = null;
    }

    protected override void CraftButton_OnClick(IButton obj)
    {
        if(firstItem == null || secondItem == null) return;

        var pairToCraft = new ItemPair(firstItem.Item, secondItem.Item);
        var result = resultsTable.GetResults(pairToCraft);

        if (result == null)
        {
            Debug.Log("Failed to craft");
            return;
        }

        results.SetSprite(result.itemImage);
        InventoryManager.Instance.GainItem(result);
        InventoryManager.Instance.DiscardItem(firstItem.Item);
        InventoryManager.Instance.DiscardItem(secondItem.Item);

        Close();
        Open();
    }

    protected override void Handle_Click(IButton button)
    {
        var craftingButton = (CraftingSlotUIButton) button;
        if (!craftingButton.IsFilled) return;
        results.SetSprite(null);

        if(craftingButton.Equals(firstItem))
        {
            craftingButton.ToggleFrame(false);
            firstItem = null;
        }
        else if (craftingButton.Equals(secondItem))
        {
            craftingButton.ToggleFrame(false);
            secondItem = null;
        }
        else if (firstItem == null)
        {
            craftingButton.ToggleFrame(true);
            firstItem = craftingButton;
        }
        else if (secondItem == null)
        {
            craftingButton.ToggleFrame(true);
            secondItem = craftingButton;
        }
    }
}

public class SelectItemHandler : MonoBehaviour
{

}