using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PuzzleHelper.PuzzleData;

public class ClimbTreePuzzle : MonoBehaviour
{

    [SerializeField] private float delayBeforeLeavingPuzzle = 2f;

    [SerializeField] private GameObject puzzleUI;

    [SerializeField] private GameObject item;

    [SerializeField] private Image bar;

    [SerializeField] private float barRate;

    private PuzzleHelper.PuzzleData currentPuzzle;

    private bool goingUp;



    private void Start()
    {
        bar.fillAmount = 0;
        goingUp= true;
    }


    public void ClimbTree(PuzzleHelper.PuzzleData puzzle)
    {
        puzzleUI.SetActive(true);
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

    void Update()
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
    }


    private void OnClick()
    {
        StartCoroutine(ResetPuzzle());
    }

    private IEnumerator ResetPuzzle()
    {
        yield return new WaitForSecondsRealtime(delayBeforeLeavingPuzzle);
        puzzleUI.SetActive(false);
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}
