using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PuzzleHelper;

public class PuzzleSystem : SingletonMonoBehavior<PuzzleSystem>
{

    public bool inPuzzle;
    private string dialogueID;
    public static Action OnPuzzleExit;
    [SerializeField] private GameObject puzzleUI;
    [SerializeField] private CollectHerbPuzzle collectHerbPuzzle;
    [SerializeField] private ClimbTreePuzzle climbTreePuzzle;

    [Serializable]
    public class itemImage
    {
        public string itemName;
        public Sprite itemSprite;
    }

    public List<itemImage> itemList = new List<itemImage>();
    Dictionary<string, Sprite> Images = new Dictionary<string, Sprite>();

    protected override void Awake()
    {
        base.Awake();

        foreach (var item in itemList)
        {
            Images[item.itemName] = item.itemSprite;
        }
    }

    public void SwapNextDialogueID(string nextDialogue)
    {
        dialogueID = nextDialogue;
    }


    public void StartPuzzle(string puzzleID)
    {
        dialogueID = puzzleID;
        if (puzzleID == null || puzzleID.Equals("Exit"))
        {
            ExitPuzzle();
            return;
        }
        else if (!inPuzzle)
        {
            inPuzzle = true;
            Controller.Instance.SwapToUIPuzzle();
        }

        PuzzleData puzzle = CreatePuzzle(puzzleID);
        DisplayPuzzle();

        switch (puzzle.Type)
        {
            case PuzzleData.Puzzle.CollectHerb:
                collectHerbPuzzle.CollectHerb(puzzle);
                break;
            case PuzzleData.Puzzle.ClimbTree:
                climbTreePuzzle.ClimbTree(puzzle);
                break;
            case PuzzleData.Puzzle.Gain:
                InventoryManager.Instance.GainItem(puzzle.Item);
                ExitPuzzle();
                break;
            case PuzzleData.Puzzle.Select:
                print("Bring something up");
                break;
            default:
                print("Could not find puzzle type");
                break;
        }
    }

    private PuzzleData CreatePuzzle(string puzzleID)
    {
        string[] puzzleIDBrokenUp = puzzleID.Split(' ');
        string item = puzzleIDBrokenUp[1];
        string puzzleType = puzzleIDBrokenUp[0];
        return new PuzzleData(item, puzzleType, Images[item]);
    }

    private void DisplayPuzzle()
    {
        puzzleUI.SetActive(true);
    }

    public void ExitPuzzle()
    {
        inPuzzle = false;
        puzzleUI.SetActive(false);
        Controller.Instance.SwapToUI();
        DialogueManager.Instance.StartDialogue(dialogueID);
    }
}
