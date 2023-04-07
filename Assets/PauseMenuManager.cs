using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [SerializeField] ButtonGroup buttons;
    [SerializeField] TextMeshProUGUI textField;
    

    private void Start()
    {
        DisableMenu();
        Controller.OnPause += PauseGame;
        ObjectiveHandler.OnObjectivesUpdated += UpdateObjectiveList;
        textField.text = "";
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
        buttons.DisableButtons();
    }

    public void PauseGame()
    {
        Controller.OnResume += ResumeGame;
        Controller.Instance.SwapToUI();
        canvas.enabled = true;
        buttons.EnableButtons();
    }

    private void OnDestroy()
    {
        buttons.DisableButtons();
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