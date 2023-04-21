using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static PuzzleHelper.PuzzleData;

public class StarPuzzle : MonoBehaviour
{

    [SerializeField] private float delayBeforeLeavingPuzzle = 5f;

    [SerializeField] private GameObject puzzleUI;

    [SerializeField] private AudioSource puzzleMusic;

    [SerializeField] private AudioSource backgroundMusic;

    public int StarFinished;

    private PuzzleHelper.PuzzleData currentPuzzle;

    private bool inStarPuzzle;

    public Vector2 inputPositionVector;


    private void Start()
    {
        inStarPuzzle = false;
    }

    private void OnEnable()
    {
        Controller.OnPosition += Position;
    }

    private void OnDisable()
    {
        Controller.OnPosition -= Position;
    }

    void Position(Vector2 input)
    {
        inputPositionVector = input;
    }


    public void StartPuzzle(PuzzleHelper.PuzzleData puzzle)
    {
        puzzleUI.SetActive(true);
        puzzleMusic.Play();
        backgroundMusic.Stop();
        inStarPuzzle = true;
        currentPuzzle = puzzle;
    }




    private IEnumerator ResetPuzzle()
    {
        yield return new WaitForSecondsRealtime(delayBeforeLeavingPuzzle);
        puzzleUI.SetActive(false);
        puzzleMusic.Stop();
        backgroundMusic.Play();
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}
