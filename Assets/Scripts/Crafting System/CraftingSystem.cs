using Sirenix.OdinInspector;
using UnityEngine;

public class CraftingSystem : SingletonMonoBehavior<CraftingSystem>
{
    [SerializeField] Canvas display;
    [SerializeField] ButtonGroup inventoryButtons;
    [SerializeField] CraftingSlotHandler slotHandler;

    private void Start()
    {
        HideScreen();
    }

    [Button]
    public void OpenScreen()
    {
        ShowScreen();
        Controller.Instance.SwapToUI();
        slotHandler.Open();
    }

    [Button]
    public void CloseScreen()
    {
        HideScreen();
        Controller.Instance.SwapToGameplay();
        slotHandler.Close();
    }

    private void ShowScreen()
    {
        display.enabled = true;
        //inventoryButtons.EnableButtons();
    }

    private void HideScreen()
    {
        display.enabled = false;
        //inventoryButtons.DisableButtons();
    }
}