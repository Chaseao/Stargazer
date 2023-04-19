using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static PuzzleHelper;

public class CollectHerbPuzzle : MonoBehaviour
{

    [SerializeField] private List<GameObject> herbs;
    private List<GameObject> herbsList;
    [SerializeField] private List<GameObject> herbsInBasket;
    public Dictionary<GameObject, bool> touchedHerbs;
    private Vector2 inputPositionVector;

    [SerializeField] private Transform newSelectionTransform;
    private Transform currentSelectionTransform;

    [SerializeField] private float distanceThreshold;
    [SerializeField] private float delayBeforeLeavingPuzzle = 2f;

    [SerializeField] private Camera UICamera;

    [SerializeField] private GameObject puzzleUI;

    [SerializeField] private AudioSource puzzleMusic;

    [SerializeField] private AudioSource backgroundMusic;

    private bool inHerbPuzzle;


    [SerializeField] private Texture2D interactiveCursorTexture;

    private PuzzleData currentPuzzle;
    private Cursor cursor;

    private bool cursorIsInteractive = false;


    private void Start()
    {
        touchedHerbs = new Dictionary<GameObject, bool>();
        herbsList = new List<GameObject>();
        herbsList.AddRange(herbs);
        inHerbPuzzle = false;
    }


    public void CollectHerb(PuzzleData puzzle)
    {
        currentPuzzle = puzzle;
        puzzleUI.SetActive(true);
        puzzleMusic.Play();
        backgroundMusic.Stop();
        inHerbPuzzle = true;
        foreach(var herb in herbsInBasket) { herb.GetComponent<Image>().sprite = puzzle.Item.itemImage; }
        foreach (var herb in herbs)
        {
            herb.GetComponent<Image>().sprite = puzzle.Item.itemImage;
            herb.SetActive(true);
            touchedHerbs.Add(herb, false);
        }
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
        OnClickInteractable();
    }

    void Update()
    {
        if (inHerbPuzzle)
        {
            FindInteractableWithinDistanceThreshold();
            if (allHerbsTouched(touchedHerbs))
            {
                PuzzleSystem.Instance.inPuzzle = false;
                inHerbPuzzle = false;
                StartCoroutine(ResetPuzzle());
            }
        }
    }

    private void FindInteractableWithinDistanceThreshold()
    {
        newSelectionTransform = null;

        for (int itemIndex = 0; itemIndex < herbs.Count; itemIndex++)
        {
            Vector2 fromMouseToInteractableOffset = UICamera.WorldToScreenPoint(herbs[itemIndex].transform.position) - new Vector3(inputPositionVector.x, inputPositionVector.y, 0f);
            if (fromMouseToInteractableOffset.sqrMagnitude < distanceThreshold * distanceThreshold)
            {
                newSelectionTransform = herbs[itemIndex].transform;
                if (!cursorIsInteractive) InteractiveCursorTexture();
                break;
            }
        }

        if (currentSelectionTransform != newSelectionTransform)
        {
            currentSelectionTransform = newSelectionTransform;
            DefaultCursorTexture();
        }
    }

    private void InteractiveCursorTexture()
    {
        cursorIsInteractive = true;
        Vector2 hotspot = new Vector2(interactiveCursorTexture.width / 2, 0);
        Cursor.SetCursor(interactiveCursorTexture, hotspot, CursorMode.Auto);
    }

    private void DefaultCursorTexture()
    {
        cursorIsInteractive = false;
        Cursor.SetCursor(default, default, default);
    }

    private void OnClickInteractable()
    {
        if (newSelectionTransform != null)
        {
            touchedHerbs[newSelectionTransform.gameObject] = true;
            newSelectionTransform.gameObject.SetActive(false);
            herbsInBasket[herbs.Count-1].SetActive(true);
            herbs.Remove(newSelectionTransform.gameObject);
            newSelectionTransform = null;
        }
    }

    private bool allHerbsTouched(Dictionary<GameObject, bool> touchedHerbs)
    {
        foreach(bool touched in touchedHerbs.Values)
        {
            if (!touched) return false;
        }
        return true;
    }

    private IEnumerator ResetPuzzle()
    {
        herbs.AddRange(herbsList);
        touchedHerbs.Clear();
        yield return new WaitForSecondsRealtime(delayBeforeLeavingPuzzle);
        foreach (var herb in herbsInBasket)
        {
            herb.SetActive(false);
        }
        puzzleMusic.Stop();
        backgroundMusic.Play();
        puzzleUI.SetActive(false);
        InventoryManager.Instance.GainItem(currentPuzzle.Item);
        PuzzleSystem.Instance.ExitPuzzle();
    }
}
