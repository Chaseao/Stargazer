using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleHelper
{
    [System.Serializable]
    public class PuzzleData
    {

        public enum Puzzle
        {
            //Add more puzzles
            CollectHerb
        }

        public Puzzle Type;
        public InventoryHelper.ItemData Item;
        public SOConversationData Dialogue;
    }
}
