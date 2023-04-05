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

    [SerializeField] private Image bar;

    [SerializeField] private float barRate;

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
        bar.fillAmount = 0;
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
            if (bar.fillAmount == 1) goingUp = false;
            else if (bar.fillAmount == 0) goingUp = true;
            if (goingUp)
            {
                bar.fillAmount += barRate;
            }
            else
            {
                bar.fillAmount -= barRate;
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
        if(bar.fillAmount >= 0.85f)
        {
            MovePlayerUp();
            bar.fillAmount = 0;
        }
        else if(numOfClimbs !=0)
        {
            MovePlayerDown();
            bar.fillAmount = 0;
        }
        else
        {
            bar.fillAmount = 0;
        }
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
        player.transform.Translate(new Vector3(0, -50, 0));
        bar.fillAmount = 0;
        goingUp = true;
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}
