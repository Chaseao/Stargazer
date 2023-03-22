using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractItem : MonoBehaviour, IInteractable
{
    [SerializeField] private SOConversationData dialogue;
    public bool ExecuteDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        print("Item Description");
        return true;
    }

    public void OpenDoor()
    {
        //Should never activate this method
    }
}
