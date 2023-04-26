using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventoryHelper;
using static PuzzleHelper;

public class SelectItemPuzzle : SerializedMonoBehaviour
{
    [SerializeField] List<SelectPuzzleData> validSelections;
    [SerializeField] GameObject display;
    [SerializeField] SlotHandlerBase slotHandler;
    [SerializeField] UIButton cancelButton;

    SelectPuzzleData currentPuzzleData;
    ItemData itemToConsume;

    public ItemData ItemToConsume { get => itemToConsume; set => itemToConsume = value; }

    private void Start()
    {
        ShowScreen();
        HideScreen();
    }

    public void StartPuzzle(PuzzleData puzzleData)
    {
        currentPuzzleData = validSelections.Find(x => x.person.ToLowerInvariant().Equals(puzzleData.Item.name.ToLowerInvariant()));
        Debug.Assert(currentPuzzleData.dialogueOnCancel != null, "Person that you talked to was not found in Select item puzzle");
        OpenScreen();
    }

    public void OpenScreen()
    {
        ShowScreen();
        Controller.OnCancel += CancelScreen;
        cancelButton.OnSelect += Button_OnClick;
        slotHandler.Open();
    }

    private void Button_OnClick(IButton obj)
    {
        CancelScreen();
    }

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
        DialogueManager.OnDialogueEnded += SelectionConfirmationListnerTrigger;
        PuzzleSystem.Instance.SwapNextDialogueID(result);
        PuzzleSystem.Instance.ExitPuzzle();
    }

    private void SelectionConfirmationListnerTrigger()
    {
        DialogueManager.OnDialogueEnded -= SelectionConfirmationListnerTrigger;
        if (DialogueManager.Instance.DialogueUnlocks.Contains(DialogueHelperClass.CONSUME_CONFIRMED) && itemToConsume != null)
        {
            DialogueManager.Instance.DialogueUnlocks.Remove(DialogueHelperClass.CONSUME_CONFIRMED);
            InventoryManager.Instance.Inventory.Remove(ItemToConsume);
            itemToConsume = null;
        }
    }

    private void CloseScreen()
    {
        HideScreen();
        Controller.OnCancel -= CancelScreen;
        cancelButton.OnSelect -= Button_OnClick;
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
