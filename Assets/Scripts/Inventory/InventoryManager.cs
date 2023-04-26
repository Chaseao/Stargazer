using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static InventoryHelper;

public class InventoryManager : SingletonMonoBehavior<InventoryManager>
{
    public static event Action<ItemData> OnItemGained;

    [SerializeField] List<ItemData> inventory = new List<ItemData>();
    public List<ItemData> Inventory => inventory;

    private void Start()
    {
        SceneManager.activeSceneChanged += ClearManager;
    }


    private void OnDestroy()
    {
        SceneManager.activeSceneChanged -= ClearManager;
    }

    private void ClearManager(Scene arg0, Scene arg1)
    {
        inventory.Clear();
    }

    public void GainItem(ItemData item)
    {
        item.name = item.name.ToLowerInvariant();
        inventory.Add(item);
        OnItemGained?.Invoke(item);
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
