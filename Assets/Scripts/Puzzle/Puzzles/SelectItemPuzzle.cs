using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static PuzzleHelper;

public class SelectItemPuzzle : SerializedMonoBehaviour
{
    [SerializeField] List<SelectPuzzleData> validSelections;
    [SerializeField] GameObject display;
    [SerializeField] SlotHandlerBase slotHandler;

    SelectPuzzleData currentPuzzleData;

    private void Start()
    {
        HideScreen();
    }

    public void StartPuzzle(PuzzleData puzzleData)
    {
        currentPuzzleData = validSelections.Find(x => x.person.ToLowerInvariant().Equals(puzzleData.Item.name.ToLowerInvariant()));
        OpenScreen();
    }

    [Button]
    public void OpenScreen()
    {
        ShowScreen();
        Controller.OnCancel += CancelScreen;
        slotHandler.Open();
    }

    [Button]
    public void CancelScreen()
    {
        CloseScreen();

        PuzzleSystem.Instance.SwapNextDialogueID(currentPuzzleData.dialogueOnCancel);
        PuzzleSystem.Instance.ExitPuzzle();
    }

    public void SelectItem(string item)
    {
        CloseScreen();

        string result = item.ToLowerInvariant() == currentPuzzleData.desiredItem.ToLowerInvariant() ? currentPuzzleData.dialogueOnSuccess : currentPuzzleData.dialogueOnFailure;
        PuzzleSystem.Instance.SwapNextDialogueID(result);
        PuzzleSystem.Instance.ExitPuzzle();
    }

    private void CloseScreen()
    {
        HideScreen();
        Controller.OnCancel -= CancelScreen;
        slotHandler.Close();
    }

    private void ShowScreen()
    {
        display.SetActive(true);
    }

    private void HideScreen()
    {
        display.SetActive(false);
    }

    private void OnDestroy()
    {
        Controller.OnCancel -= CancelScreen;
    }

    [System.Serializable]
    private struct SelectPuzzleData
    {
        public string person;
        public string desiredItem;
        public string dialogueOnSuccess;
        public string dialogueOnFailure;
        public string dialogueOnCancel;
    }
}
