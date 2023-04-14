using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public static class DialogueHelperClass
{
    public static readonly string ID_MARKER = "ID: ";
    public static readonly string CONVERSANT_MARKER = "Conversant: ";
    public static readonly string UNLOCKS_MARKER = "Unlocks: ";
    public static readonly string DIALOGUE_MARKER = "Dialogue:";
    public static readonly string PLAYER_MARKER = "L0-1D: ";
    public static readonly string VOICE_MARKER = "Voice: ";
    public static readonly string CHOICES_MARKER = "Choices:";
    public static readonly string LEADS_TO_MARKER = "Leads to:";
    public static readonly string SOUND_MARKER = "Music:";
    public static readonly string EMPTY_MARKER = "N/A";
    public static readonly string POTION_MADE_UNLOCK = "ITEMCRAFTED";

    [System.Serializable]
    public class DialogueData
    {
        public bool PlayerIsSpeaker;
        public bool VoiceSpeaker;
        [SerializeField, TextArea()] public string Dialogue;
    }

    [System.Serializable]
    public class ConversationData
    {
        public string ID;
        public string Conversant;
        public string Unlocks;
        public string Sound;
        public List<DialogueData> Dialogues = new List<DialogueData>();
        public List<string> Choices = new List<string>();
        public List<DialogueBranchData> LeadsTo = new List<DialogueBranchData>();
    }

    [System.Serializable]
    public class DialogueBranchData
    {
        public string BranchText;
        public bool isPuzzle;
        public List<RequirementData> Requirements;
    }

    [System.Serializable]
    public class RequirementData
    {
        public string label;
        public bool isItemID;
        [ShowIf("isItemID", true)] public bool consumesItem;
    }
}