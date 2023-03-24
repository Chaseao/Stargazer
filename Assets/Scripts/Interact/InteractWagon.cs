using UnityEngine;

public class InteractWagon : MonoBehaviour, IInteractable
{
    public bool ExecuteDialogue()
    {
        CraftingSystem.Instance.OpenScreen();
        return true;
    }

    public void OpenDoor()
    {

    }
}
