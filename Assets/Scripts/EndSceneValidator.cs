using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneValidator : MonoBehaviour
{

    [SerializeField] private float timeBeforeExit;

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
            StartCoroutine(WaitForEnding());
        }
    }

    private IEnumerator WaitForEnding()
    {
        yield return new WaitForSecondsRealtime(timeBeforeExit);
        StartCoroutine(SceneTools.TransitionToScene(SceneTools.NextSceneIndex));
    }

    private void OnDestroy()
    {
        DialogueManager.OnDialogueEnded -= CheckForEnding;
    }
}
