using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryHelper;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    [SerializeField] List<ItemData> inventory = new List<ItemData>();
    public List<ItemData> Inventory => inventory;

    public void GainItem(ItemData item)
    {
        inventory.Add(item);
    }

    public void DiscardItem(ItemData item)
    {
        Debug.Assert(inventory.Contains(item));
        inventory.Remove(item);
    }
}
