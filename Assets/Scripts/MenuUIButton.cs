using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIButton : UIButton
{
    [SerializeField] ButtonFunctions function;
    [SerializeField] int sceneIndex;
    [SerializeField] PauseMenuManager pauseMenuManager;


    enum ButtonFunctions
    {
        transtionToMenu,
        quitGame,
        resumeGame
    }

    public override void Use()
    {
        switch (function)
        {
            case ButtonFunctions.transtionToMenu:
                Controller.Instance.SwapToGameplay();
                StartCoroutine(SceneTools.TransitionToScene(sceneIndex));
                break;
            case ButtonFunctions.quitGame:
                Application.Quit();
                break;
            case ButtonFunctions.resumeGame:
                pauseMenuManager.ResumeGame();
                break;
        }
    }
}
