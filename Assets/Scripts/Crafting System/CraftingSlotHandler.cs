using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static InventoryHelper;

public class CraftingSlotHandler : SlotHandlerBase
{
    [SerializeField] ResultsSlot results;
    [SerializeField] CraftButton craftButton;
    [SerializeField] CraftingResultsTable resultsTable;
    [SerializeField] Sprite failedSprite;
    [SerializeField] Image itemSelectedOne;
    [SerializeField] Image itemSelectedTwo;

    private CraftingSlotUIButton firstItem;
    private CraftingSlotUIButton secondItem;

    protected override UIButton ActivateButton { get => craftButton; }

    protected override List<ItemData> Inventory => InventoryManager.Instance.Inventory;

    public override void Close()
    {
        base.Close();
        firstItem = null;
        secondItem = null;
    }

    protected override void ActivateButton_OnClick(IButton obj)
    {
        if(firstItem == null || secondItem == null) return;

        var pairToCraft = new ItemPair(firstItem.Item, secondItem.Item);
        var result = resultsTable.GetResults(pairToCraft);

        if (result == null)
        {
            Debug.Log("Failed to craft");
            NotificationHandler.CreateNotification("Invalid Recipe", failedSprite);
            results.SetSprite(failedSprite);
            return;
        }

        results.SetSprite(result.itemImage);
        InventoryManager.Instance.GainItem(result);
        InventoryManager.Instance.DiscardItem(firstItem.Item);
        InventoryManager.Instance.DiscardItem(secondItem.Item);
        itemSelectedOne.sprite = null;
        itemSelectedTwo.sprite = null;
        DialogueManager.Instance.GainUnlock(DialogueHelperClass.POTION_MADE_UNLOCK);

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
            itemSelectedOne.sprite = null;
        }
        else if (craftingButton.Equals(secondItem))
        {
            craftingButton.ToggleFrame(false);
            secondItem = null;
            itemSelectedTwo.sprite = null;
        }
        else if (firstItem == null)
        {
            craftingButton.ToggleFrame(true);
            firstItem = craftingButton;
            itemSelectedOne.sprite = firstItem.item.itemImage;
        }
        else if (secondItem == null)
        {
            craftingButton.ToggleFrame(true);
            secondItem = craftingButton;
            itemSelectedTwo.sprite = secondItem.item.itemImage;
        }
        
        itemSelectedOne.enabled = itemSelectedOne.sprite != null;
        itemSelectedTwo.enabled = itemSelectedTwo.sprite != null;
    }
}

public class SelectItemHandler : MonoBehaviour
{

}