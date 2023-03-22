using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PuzzleHelper;

public class PuzzleSystem : SingletonMonoBehavior<PuzzleSystem>
{

    private bool inPuzzle;
    public static Action OnPuzzleExit;

    public void StartPuzzle(string puzzleID)
    {
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

        switch (puzzle.Type)
        {
            case PuzzleHelper.PuzzleData.Puzzle.CollectHerb:
                print("Do Collect Herb Puzzle");
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
        //To Do - make a puzzledata object from item and puzzle type
        return new PuzzleData();
    }

    private void ExitPuzzle()
    {
        inPuzzle = false;
        Controller.Instance.SwapToUI();
    }
}
