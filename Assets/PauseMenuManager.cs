using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] List<Page> pages;
    [SerializeField] TextMeshProUGUI textField;

    int currentPageIndex;

    [Serializable, ExpandedClass]
    public class Page
    {
        [SerializeField] GameObject page;
        [SerializeField] ButtonGroup buttons;

        public void ToggleDisplay(bool isEnabled)
        {
            page.SetActive(isEnabled);
            if (isEnabled) buttons.EnableButtons();
            else buttons.DisableButtons();
        }
    }

    private void Start()
    {
        DisableMenu();
        Controller.OnPause += PauseGame;
        ObjectiveHandler.OnObjectivesUpdated += UpdateObjectiveList;
        textField.text = "";
        currentPageIndex = 0;
    }

    public void SwitchToNext()
    {
        SwitchPage(1);
    }

    public void SwitchToPrevious()
    {
        SwitchPage(-1);
    }

    private void SwitchPage(int amount)
    {
        if (currentPageIndex + amount >= pages.Count || currentPageIndex + amount < 0) return;
        pages[currentPageIndex].ToggleDisplay(false);
        currentPageIndex += amount;
        pages[currentPageIndex].ToggleDisplay(true);
    }

    private void UpdateObjectiveList(List<Objective> obj)
    {
        textField.text = "";
        foreach(var objective in obj)
        {
            textField.text += " - " + objective.ObjectiveData.Title + "\n";
        }
    }

    public void ResumeGame()
    {
        Controller.Instance.SwapToGameplay();
        DisableMenu();
        Controller.OnResume -= ResumeGame;
    }

    private void DisableMenu()
    {
        canvas.enabled = false;
        pages.ForEach(page => page.ToggleDisplay(false));
    }

    public void PauseGame()
    {
        Controller.OnResume += ResumeGame;
        Controller.Instance.SwapToUI();
        canvas.enabled = true;
        currentPageIndex = 0;
        pages[currentPageIndex].ToggleDisplay(true);
    }

    private void OnDestroy()
    {
        pages.ForEach(page => page.ToggleDisplay(false));
        Controller.OnPause -= PauseGame;
        Controller.OnResume -= ResumeGame;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GoToMenu()
    {
        Controller.Instance.SwapToGameplay();
        StartCoroutine(SceneTools.TransitionToScene(0));
    }

}