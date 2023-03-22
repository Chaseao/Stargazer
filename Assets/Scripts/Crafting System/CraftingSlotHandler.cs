using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static InventoryHelper;

public class CraftingSlotHandler : MonoBehaviour
{
    [SerializeField] ResultsSlot results;
    [SerializeField] CraftButton craftButton;
    [SerializeField] CraftingResultsTable resultsTable;

    private List<CraftingSlotUIButton> slots;

    private CraftingSlotUIButton firstItem;
    private CraftingSlotUIButton secondItem;

    private void Awake()
    {
        slots = transform.GetComponentsInChildren<CraftingSlotUIButton>().ToList();
    }

    private void Start()
    {
        Close();
    }

    public void Close()
    {
        ClearSlots();
        craftButton.OnClick -= CraftButton_OnClick;
        firstItem = null;
        secondItem = null;
    }

    public void Open()
    {     
        SetUpSlots();
        craftButton.OnClick += CraftButton_OnClick;
    }

    private void CraftButton_OnClick(IButton obj)
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

    private void ClearSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].OnClick -= Handle_Click;
            slots[i].SetItem(null);
            slots[i].ToggleFrame(false);
        }
    }

    private void Handle_Click(IButton button)
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

    private void SetUpSlots()
    {
        InventoryManager inventory = InventoryManager.Instance;
        Debug.Assert(inventory.Inventory.Count <= slots.Count);

        for(int i = 0; i < inventory.Inventory.Count; i++)
        {
            slots[i].OnClick += Handle_Click;
            slots[i].SetItem(inventory.Inventory[i]);
        }
    }
}