using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

public class ButtonGroup : MonoBehaviour
{
    List<IButton> buttons;
    int currentButtonIndex = -1;

    public void EnableButtons()
    {
        SetButton(0);

        Controller.OnNavigateMenu += SwapButton;
        Controller.OnSelect += ActivateButton;
    }

    public void DisableButtons()
    {
        Controller.OnNavigateMenu -= SwapButton;
        Controller.OnSelect -= ActivateButton;
    }

    private void Awake()
    {
        buttons = GetComponentsInChildren<IButton>().ToList();
        buttons.ForEach(button => button.OnClick += Button_OnClick);
    }

    private void Button_OnClick(IButton obj)
    {
        SetButton(buttons.FindIndex(button => obj == button));
    }

    private void SetButton(int index)
    {
        if (currentButtonIndex == index) return;
        if(currentButtonIndex != -1)
        {
            buttons[currentButtonIndex].ToggleSelected(false);
        }
        currentButtonIndex = index;
        buttons[currentButtonIndex].ToggleSelected(true);
    }

    private void ActivateButton()
    {
        if (!FadeToBlackSystem.FadeOutComplete) return;
        
        buttons[currentButtonIndex].Use();
    }

    private void SwapButton(Vector2 direction)
    {
        if (!FadeToBlackSystem.FadeOutComplete) return;

        int newIndex = currentButtonIndex;
        newIndex -= Mathf.RoundToInt(direction.y);
        newIndex = newIndex >= buttons.Count ? 0 : newIndex;
        newIndex = newIndex < 0 ? buttons.Count - 1 : newIndex;
        SetButton(newIndex);
    }

    private void OnDestroy()
    {
        buttons.ForEach(button => button.OnClick -= Button_OnClick);

        Controller.OnNavigateMenu -= SwapButton;
        Controller.OnSelect -= ActivateButton;
    }
}
