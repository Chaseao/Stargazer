using System.Collections.Generic;
using UnityEngine;

public static class DialogueHelperClass
{
    [System.Serializable]
    public class DialogueData
    {
        public bool WickIsSpeaker;
        public bool VoiceSpeaker;
        [SerializeField, TextArea()] public string Dialogue;
    }

    [System.Serializable]
    public class ConversationData
    {
        public string ID;
        public string Conversant;
        public string Unlocks;
        public List<DialogueData> Dialogues = new List<DialogueData>();
        public List<DialogueBranchData> Choices = new List<DialogueBranchData>();
        public List<DialogueBranchData> LeadsTo = new List<DialogueBranchData>();
    }

    [System.Serializable]
    public class DialogueBranchData
    {
        public string BranchText;
        public bool isPuzzle;
        public List<string> Requirements;
    }

}