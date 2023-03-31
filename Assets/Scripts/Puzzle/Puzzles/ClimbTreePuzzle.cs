using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ClimbTreePuzzle : MonoBehaviour
{

 
    [SerializeField] private float delayBeforeLeavingPuzzle = 2f;

    [SerializeField] private GameObject puzzleUI;

    [SerializeField] private GameObject item;


    private void Start()
    {

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
    
    }


    private void OnClick()
    {
        StartCoroutine(ResetPuzzle());
    }

    private IEnumerator ResetPuzzle()
    {
        yield return new WaitForSecondsRealtime(delayBeforeLeavingPuzzle);
        puzzleUI.SetActive(false);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}
