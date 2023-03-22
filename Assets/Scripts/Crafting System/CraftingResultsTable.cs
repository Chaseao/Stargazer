using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using static InventoryHelper;

public class CraftingResultsTable : SerializedMonoBehaviour
{
    [SerializeField] readonly Dictionary<ItemPair, ItemData> craftResults = new Dictionary<ItemPair, ItemData>();

    public ItemData GetResults(ItemPair pair)
    {
        ItemData result = null;

        foreach (var possiblity in craftResults)
        {
            if (possiblity.Key.IsMatch(pair))
            {
                result = possiblity.Value;
            }
        }

        return result;
    }
}
