using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Objective", menuName = "SO/Objective")]
public class Objective : ScriptableObject
{
    [SerializeField] private string startTrigger;
    [SerializeField] private string endTrigger;
    [SerializeField, ExpandedClass] private ObjectiveData objective;

    public bool IsStarted(List<string> unlocks) => unlocks.Contains(startTrigger.ToLowerInvariant());
    public bool IsEnded(List<string> unlocks) => unlocks.Contains(endTrigger.ToLowerInvariant());
    public ObjectiveData ObjectiveData => objective;
}
