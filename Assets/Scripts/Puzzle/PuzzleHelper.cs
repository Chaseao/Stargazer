using System.Collections;
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
            ClimbTree,
            Gain,
            Select,
            Water
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
                case "ClimbTree":
                    Type = Puzzle.ClimbTree;
                    break;
                case "Gain":
                    Type = Puzzle.Gain;
                    break;
                case "Select":
                    Type = Puzzle.Select;
                    break;
                case "Water":
                    Type = Puzzle.Water;
                    break;
                default:
                    Debug.Log("ERROR: Could not find Puzzle Type");
                    break;
            }
        }
    }
}
