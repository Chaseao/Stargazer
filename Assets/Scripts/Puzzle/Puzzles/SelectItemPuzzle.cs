using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemPuzzle : SerializedMonoBehaviour
{
    [SerializeField] List<SelectPuzzleData> validSelections;
    [SerializeField] GameObject display;
    [SerializeField] CraftingSlotHandler slotHandler;

    SelectPuzzleData currentPuzzleData;

    private void Start()
    {
        HideScreen();
    }

    [Button]
    public void OpenScreen()
    {
        ShowScreen();
        Controller.OnResume += CancelScreen;
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
        Controller.OnResume -= CancelScreen;
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
        Controller.OnResume -= CancelScreen;
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
