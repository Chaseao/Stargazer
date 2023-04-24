using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SlotHandlerBase : MonoBehaviour
{
    protected List<CraftingSlotUIButton> slots;
    protected abstract UIButton ActivateButton { get; }
    protected abstract List<InventoryHelper.ItemData> Inventory { get; }

    public virtual void Close()
    {
        ClearSlots();
        ActivateButton.OnSelect -= ActivateButton_OnClick;
    }

    public void Open()
    {
        SetUpSlots();
        ActivateButton.OnSelect += ActivateButton_OnClick;
    }

    private void Awake()
    {
        slots = transform.GetComponentsInChildren<CraftingSlotUIButton>().ToList();
        Close();
    }


    private void ClearSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].OnSelect -= Handle_Click;
            slots[i].SetItem(null);
            slots[i].ToggleFrame(false);
        }
    }

    protected abstract void ActivateButton_OnClick(IButton obj);
    protected abstract void Handle_Click(IButton button);

    private void SetUpSlots()
    {
        Debug.Assert(Inventory.Count <= slots.Count);

        for (int i = 0; i < Inventory.Count; i++)
        {
            slots[i].OnSelect += Handle_Click;
            slots[i].SetItem(Inventory[i]);
        }
    }
}