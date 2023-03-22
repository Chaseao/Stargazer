using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryHelper
{
    [System.Serializable]
    public class ItemData
    {
        public string name;
        public Sprite itemImage;
    }

    [System.Serializable]
    public class ItemPair
    {
        public string itemOne;
        public string itemTwo;

        public ItemPair(ItemData itemOne, ItemData itemTwo)
        {
            this.itemOne = itemOne.name;
            this.itemTwo = itemTwo.name;
        }

        public bool IsMatch(ItemPair item)
        {
            bool perfectMatch = item.itemOne == itemOne && item.itemTwo == itemTwo;
            bool oppositeMatch = item.itemTwo == itemOne && item.itemOne == itemTwo;

            return perfectMatch || oppositeMatch;
        }
    }
}
