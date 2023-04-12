using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PuzzleHelper.PuzzleData;

public class DigPuzzle : MonoBehaviour
{

    [SerializeField] private float delayBeforeLeavingPuzzle = 5f;

    [SerializeField] private GameObject puzzleUI;

    [SerializeField] private GameObject item;

    [SerializeField] private Image bar;

    [SerializeField] private int numOfClicksNeeded;

    [SerializeField] private int numOfClicksToRemoveDirt;

    [SerializeField] private AudioSource puzzleMusic;

    [SerializeField] private AudioSource backgroundMusic;

    [SerializeField] private GameObject dirtParticles;

    [SerializeField] private Camera UICam;

    private Vector2 inputPositionVector;

    public GameObject[] Dirt;

    private PuzzleHelper.PuzzleData currentPuzzle;

    private int numOfClicks;

    private bool inDigPuzzle;


    private void Start()
    {
        inDigPuzzle = false;
    }


    public void StartPuzzle(PuzzleHelper.PuzzleData puzzle)
    {
        puzzleUI.SetActive(true);
        puzzleMusic.Play();
        backgroundMusic.Stop();
        inDigPuzzle = true;
        currentPuzzle = puzzle;
        item.GetComponent<Image>().sprite = puzzle.Item.itemImage;
    }

    private void OnEnable()
    {
        Controller.OnPosition += Position;
        Controller.OnClick += Click;
    }

    private void OnDisable()
    {
        Controller.OnPosition -= Position;
        Controller.OnClick -= Click;
    }

    void Position(Vector2 input)
    {
        inputPositionVector = input;
    }

    void Click()
    {
        OnClick();
    }

    void FixedUpdate()
    {
        if (inDigPuzzle)
        {
            if (numOfClicks == numOfClicksNeeded)
            {
                PuzzleSystem.Instance.inPuzzle = false;
                inDigPuzzle = false;
                StartCoroutine(ResetPuzzle());
            }
        }
    }


    private void OnClick()
    {
        if(inDigPuzzle)
        {
            numOfClicks++;
            bar.fillAmount += ((float)1 / numOfClicksNeeded);
            if (numOfClicks % numOfClicksToRemoveDirt == 0 && Dirt.Length > (numOfClicks / numOfClicksToRemoveDirt) - 1)
            {
                Dirt[(numOfClicks / numOfClicksToRemoveDirt) - 1].SetActive(false);
            }
            CreateParticle();
        }
    }

    private void CreateParticle()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(inputPositionVector);
        GameObject dirt = Instantiate(dirtParticles, new Vector3(mousePos.x, mousePos.y, 0f), Quaternion.identity) as GameObject;
        dirt.transform.SetParent(puzzleUI.transform);
    }

    private IEnumerator ResetPuzzle()
    {
        numOfClicks = 0;
        yield return new WaitForSecondsRealtime(delayBeforeLeavingPuzzle);
        puzzleUI.SetActive(false);
        puzzleMusic.Stop();
        backgroundMusic.Play();
        bar.fillAmount = 0;
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}

