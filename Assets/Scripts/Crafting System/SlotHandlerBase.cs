using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class SlotHandlerBase : MonoBehaviour
{
    protected List<CraftingSlotUIButton> slots;
    protected abstract CraftButton CraftButton { get; }
    protected List<InventoryHelper.ItemData> Inventory { get; }

    public virtual void Close()
    {
        ClearSlots();
        CraftButton.OnClick -= CraftButton_OnClick;
    }

    public void Open()
    {
        SetUpSlots();
        CraftButton.OnClick += CraftButton_OnClick;
    }

    private void Awake()
    {
        slots = transform.GetComponentsInChildren<CraftingSlotUIButton>().ToList();
    }

    private void Start()
    {
        Close();
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

    protected abstract void CraftButton_OnClick(IButton obj);
    protected abstract void Handle_Click(IButton button);

    private void SetUpSlots()
    {
        Debug.Assert(Inventory.Count <= slots.Count);

        for (int i = 0; i < Inventory.Count; i++)
        {
            slots[i].OnClick += Handle_Click;
            slots[i].SetItem(Inventory[i]);
        }
    }
}