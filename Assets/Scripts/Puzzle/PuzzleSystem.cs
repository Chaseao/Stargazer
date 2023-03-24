using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PuzzleHelper;
using static CollectHerbPuzzle;

public class PuzzleSystem : SingletonMonoBehavior<PuzzleSystem>
{

    public bool inPuzzle;
    private String dialogueID;
    public static Action OnPuzzleExit;
    [SerializeField] private Sprite carrotImage;
    [SerializeField] private GameObject puzzleUI;
    [SerializeField] private CollectHerbPuzzle collectHerbPuzzle;

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
        DisplayPuzzle(puzzle);

        switch (puzzle.Type)
        {
            case PuzzleHelper.PuzzleData.Puzzle.CollectHerb:
                collectHerbPuzzle.CollectHerb();
                break;
            case PuzzleHelper.PuzzleData.Puzzle.DigDirt:
                print("Do Dig Dirt Puzzle");
                break;
            default:
                print("Could not find puzzle type");
                break;
        }
    }

    private PuzzleData CreatePuzzle(string puzzleID)
    {
        String[] puzzleIDBrokenUp = puzzleID.Split(' ');
        String item = puzzleIDBrokenUp[1];
        String puzzleType = puzzleIDBrokenUp[0];
        return new PuzzleData(item, puzzleType, carrotImage);
    }

    private void DisplayPuzzle(PuzzleData puzzle)
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
