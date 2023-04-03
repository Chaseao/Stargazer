using Sirenix.OdinInspector;
using UnityEngine;

public class CraftingSystem : SingletonMonoBehavior<CraftingSystem>
{
    [SerializeField] Canvas display;
    [SerializeField] CraftingSlotHandler slotHandler;
    [SerializeField] UIButton closeButton;

    private void Start()
    {
        HideScreen();
    }

    [Button]
    public void OpenScreen()
    {
        ShowScreen();
        Controller.Instance.SwapToUI();
        Controller.OnResume += CloseScreen;
        closeButton.OnClick += CloseScreen;
        slotHandler.Open();
    }

    public void CloseScreen(IButton _)
    {
        CloseScreen();
    }

    [Button]
    public void CloseScreen()
    {
        HideScreen();
        Controller.Instance.SwapToGameplay();
        Controller.OnResume -= CloseScreen;
        closeButton.OnClick -= CloseScreen;
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

    private void OnDestroy()
    {
        Controller.OnResume -= CloseScreen;
    }
}