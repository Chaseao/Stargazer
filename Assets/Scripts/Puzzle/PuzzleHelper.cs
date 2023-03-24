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
            CollectHerb,
            DigDirt
        }

        public Puzzle Type;
        public InventoryHelper.ItemData Item;
        //public SOConversationData Dialogue;

        public PuzzleData(string item, string puzzleType, Sprite itemImage/*, SOConversationData dialogue*/)
        {
            Item = new InventoryHelper.ItemData(item, itemImage);
            //Dialogue = dialogue;
            switch(puzzleType)
            {
                case "CollectHerb":
                    Type = Puzzle.CollectHerb;
                    break;
                case "DigDirt":
                    Type = Puzzle.DigDirt;
                    break;
                default:
                    Debug.Log("ERROR: Could not find Puzzle Type");
                    break;
            }
        }
    }
}
