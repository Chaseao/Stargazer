using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PuzzleHelper.PuzzleData;

public class ClimbTreePuzzle : MonoBehaviour
{

    [SerializeField] private float delayBeforeLeavingPuzzle = 5f;

    [SerializeField] private GameObject puzzleUI;

    [SerializeField] private GameObject item;

    [SerializeField] private GameObject player;

    [Header("Bar Settings")]
    [SerializeField] private Transform bar;
    [SerializeField] private float barRate;
    [SerializeField] private float barHeight;
    [SerializeField] private float barReachedHeight;
    

    [SerializeField] private float climbRate;

    [SerializeField] private int numOfClimbsNeeded;

    [SerializeField] private AudioSource puzzleMusic;

    [SerializeField] private AudioSource backgroundMusic;

    private PuzzleHelper.PuzzleData currentPuzzle;

    private bool goingUp;

    private int numOfClimbs;

    private bool inClimbPuzzle;


    private void Start()
    {
        bar.localPosition = Vector3.down * barHeight;
        numOfClimbs = 0;
        goingUp= true;
        inClimbPuzzle= false;
    }


    public void ClimbTree(PuzzleHelper.PuzzleData puzzle)
    {
        puzzleUI.SetActive(true);
        puzzleMusic.Play();
        backgroundMusic.Stop();
        inClimbPuzzle= true;
        currentPuzzle= puzzle;
        item.GetComponent<Image>().sprite = puzzle.Item.itemImage;
    }

    private void OnEnable()
    {
        Controller.OnClick += Click;
    }

    private void OnDisable()
    {
        Controller.OnClick -= Click;
    }

    void Click()
    {
        OnClick();
    }

    void FixedUpdate()
    {
        if(inClimbPuzzle)
        {
            if (bar.localPosition.y > 0) goingUp = false;
            else if (bar.localPosition.y < -barHeight) goingUp = true;
            if (goingUp)
            {
                bar.localPosition += Vector3.up * barRate * Time.deltaTime;
            }
            else
            {
                bar.localPosition += Vector3.down * barRate * Time.deltaTime;
            }

            if (numOfClimbs == numOfClimbsNeeded)
            {
                PuzzleSystem.Instance.inPuzzle = false;
                inClimbPuzzle= false;
                StartCoroutine(ResetPuzzle());
            }
        }
    }


    private void OnClick()
    {
        if(bar.localPosition.y >= barReachedHeight)
        {
            MovePlayerUp();
        }
        else if(numOfClimbs !=0)
        {
            MovePlayerDown();
        }
        
        bar.localPosition = Vector3.down * barHeight;
    }

    private void MovePlayerUp()
    {
        player.transform.Translate(new Vector3(0, climbRate, 0));
        numOfClimbs++;
    }

    private void MovePlayerDown()
    {
        player.transform.Translate(new Vector3(0, -climbRate, 0));
        numOfClimbs--;
    }

    private IEnumerator ResetPuzzle()
    {
        numOfClimbs = 0;
        yield return new WaitForSecondsRealtime(delayBeforeLeavingPuzzle);
        puzzleUI.SetActive(false);
        puzzleMusic.Stop();
        backgroundMusic.Play();
        player.transform.Translate(new Vector3(0, -climbRate * numOfClimbsNeeded, 0));
        bar.localPosition = Vector3.down * barHeight;
        goingUp = true;
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}
