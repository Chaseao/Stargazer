using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectiveHandler : MonoBehaviour
{
    [SerializeField] NotificationPopup objectivePrefab;

    List<Objective> unstartedObjectives = new List<Objective>();
    List<Objective> currentObjectives = new List<Objective>();
    List<Objective> completedObjectives = new List<Objective>();

    private void Start()
    {
        unstartedObjectives = Resources.LoadAll<Objective>("Objectives").ToList();
        DialogueManager.OnDialogueEnded += CheckObjectives;
    }

    private void CheckObjectives()
    {
        List<string> currentDialogueUnlocks = DialogueManager.Instance.DialogueUnlocks;

        for(int i = unstartedObjectives.Count - 1; i >= 0; i--)
        {
            if (unstartedObjectives[i].IsStarted(currentDialogueUnlocks))
            {
                DisplayNewObjective(unstartedObjectives[i].ObjectiveData);
                currentObjectives.Add(unstartedObjectives[i]);
                unstartedObjectives.RemoveAt(i);
            }
        }

        for(int i = currentObjectives.Count - 1; i >= 0; i--)
        {
            if (currentObjectives[i].IsEnded(currentDialogueUnlocks))
            {
                completedObjectives.Add(currentObjectives[i]);
                currentObjectives.RemoveAt(i);
            }
        }
    }

    private void DisplayNewObjective(ObjectiveData data)
    {
        var popup = Instantiate(objectivePrefab);
        popup.Initialize(new Notification(data));
        popup.transform.SetParent(transform);
        popup.transform.localPosition = Vector3.zero;
    }
}
