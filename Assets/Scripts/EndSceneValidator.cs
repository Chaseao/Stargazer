using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneValidator : MonoBehaviour
{
    private void Start()
    {
        DialogueManager.OnDialogueEnded += CheckForEnding;
    }

    private void CheckForEnding()
    {
        List<string> keysToCheck = DialogueHelperClass.POTION_GIVEN_MARKERS;
        int totalFound = 0;
        foreach(var key in keysToCheck)
        {
            if (DialogueManager.Instance.DialogueUnlocks.Contains(key))
            {
                totalFound++;
            }
        }

        if (totalFound >= keysToCheck.Count / 2)
        {
            StartCoroutine(SceneTools.TransitionToScene(SceneTools.NextSceneIndex));
        }
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= CheckForEnding;
    }
}
