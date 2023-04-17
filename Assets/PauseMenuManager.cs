using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject coverPages;
    [SerializeField] GameObject journalPages;
    [SerializeField] ButtonGroup coverButtons;
    [SerializeField] ButtonGroup journalButtons;
    [SerializeField] TextMeshProUGUI textField;
    

    private void Start()
    {
        DisableMenu();
        Controller.OnPause += PauseGame;
        ObjectiveHandler.OnObjectivesUpdated += UpdateObjectiveList;
        textField.text = "";
    }

    public void SwitchToJournal()
    {
        coverButtons.DisableButtons();
        coverPages.SetActive(false);

        journalButtons.EnableButtons();
        journalPages.SetActive(true);
    }

    public void SwitchToCover()
    {
        journalButtons.DisableButtons();
        journalPages.SetActive(false);

        coverButtons.EnableButtons();
        coverPages.SetActive(true);
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
        coverButtons.DisableButtons();
        journalButtons.DisableButtons();
    }

    public void PauseGame()
    {
        Controller.OnResume += ResumeGame;
        Controller.Instance.SwapToUI();
        canvas.enabled = true;
        SwitchToCover();
    }

    private void OnDestroy()
    {
        coverButtons.DisableButtons();
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