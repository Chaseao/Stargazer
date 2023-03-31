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
        item.name = item.name.ToLowerInvariant();
        inventory.Add(item);
    }

    public void DiscardItem(string item)
    {
        DiscardItem(inventory.Find(x => x.name == item));
    }

    public void DiscardItem(ItemData item)
    {
        item.name = item.name.ToLower();
        Debug.Assert(inventory.Contains(item));
        inventory.Remove(item);
    }

    public bool CheckForItem(string itemName)
    {
        return inventory.Exists(x => x.name == itemName);
    }
}
