using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.Utilities;
using UnityEditor;
using static DialogueHelperClass;

public static class JsonDialogueConverter
{ 
    public static string ConvertToJson(ConversationData conversation)
    {
        string jsonFile = JsonUtility.ToJson(conversation, true);
        string conversationID = conversation.ID;
        //System.IO.File.WriteAllText(Application.dataPath + $"/Dialogue/{conversationID}.json", jsonFile);
        return jsonFile;
    }

    public static void ConvertToJson(string text)
    {
        foreach (string dialogueScene in text.Split(ID_MARKER, StringSplitOptions.RemoveEmptyEntries)) {
            Debug.Log(dialogueScene);
            SOConversationData conversation = ScriptableObject.CreateInstance<SOConversationData>();
            conversation.SetConversation(ConvertFromJson(ConvertToJson(ConvertToConversation(dialogueScene))));

            string filePath = $"Assets/Resources/Dialogue/{conversation.Data.ID}.asset";
            if (System.IO.File.Exists(filePath))
            {
                var file = AssetDatabase.LoadAssetAtPath(filePath, typeof(SOConversationData)) as SOConversationData;
                file.SetConversation(conversation.Data);
            }
            else
            {
                AssetDatabase.CreateAsset(conversation, filePath);
            }
        }
    }

    public static ConversationData ConvertFromJson(string jsonFile)
    {
        return JsonUtility.FromJson<ConversationData>(jsonFile);
    }

    public static ConversationData ConvertFromJson(TextAsset jsonFile)
    {
        return ConvertFromJson(jsonFile.text);
    }

    private static ConversationData ConvertToConversation(string text)
    {
        var conversation = new ConversationData();
        var lines = text.Split('\n').Where(x => !x.IsNullOrWhitespace()).Select(x => x.Trim()).ToList();

        conversation.ID = lines[0];
        Debug.Log($"Converting {lines[0]}");
        lines.RemoveAt(0);


        AssertMarker(lines[0], CONVERSANT_MARKER);
        conversation.Conversant = lines[0].Substring(CONVERSANT_MARKER.Length);
        lines.RemoveAt(0);

        AssertMarker(lines[0], UNLOCKS_MARKER);
        conversation.Unlocks = lines[0].Substring(UNLOCKS_MARKER.Length);
        lines.RemoveAt(0);

        AssertMarker(lines[0], DIALOGUE_MARKER);
        lines.RemoveAt(0);

        while (!lines[0].StartsWith(CHOICES_MARKER))
        {
            if (!lines[0].StartsWith(PLAYER_MARKER) && !lines[0].StartsWith($"{conversation.Conversant}: ") && !lines[0].StartsWith(VOICE_MARKER))
            {
                conversation.Dialogues[conversation.Dialogues.Count - 1].Dialogue += " " + lines[0];
            }
            else
            {
                string dialogueLine = "";
                if (lines[0].StartsWith(PLAYER_MARKER))
                {
                    dialogueLine = lines[0].Substring(PLAYER_MARKER.Length);
                }
                else if (lines[0].StartsWith(VOICE_MARKER))
                {
                    dialogueLine = lines[0].Substring(VOICE_MARKER.Length);
                }
                else
                {
                    dialogueLine = lines[0].Substring(conversation.Conversant.Length + ": ".Length);
                }
                conversation.Dialogues.Add(new DialogueData() { Dialogue = dialogueLine.Trim(), VoiceSpeaker = lines[0].StartsWith(VOICE_MARKER), PlayerIsSpeaker = lines[0].StartsWith(PLAYER_MARKER) });
            }

            lines.RemoveAt(0);
        }

        lines.RemoveAt(0);

        while (!lines[0].StartsWith(LEADS_TO_MARKER))
        {
            var choiceOption = lines[0].Split('~').Select(x => x.Trim()).ToArray();
            var branchData = new DialogueBranchData();

            branchData.BranchText = choiceOption[0];
            branchData.Requirements = new List<string>();

            for(int i = 1; i < choiceOption.Length; i++)
            {
                branchData.Requirements.Add(choiceOption[i]);
            }

            conversation.Choices.Add(branchData);
            lines.RemoveAt(0);
        }

        lines.RemoveAt(0);

        while (lines.Count > 0)
        {
            var branchLines = lines[0].Split('~').Select(x => x.Trim()).ToArray();
            var branchData = new DialogueBranchData();

            Debug.Log(branchLines[0] + "|" + branchLines[0][0]);
            branchData.isPuzzle = branchLines[0][0] == '*';
            branchLines[0] = branchData.isPuzzle ? branchLines[0].Substring(1) : branchLines[0];

            branchData.BranchText = branchLines[0];
            branchData.Requirements = new List<string>();

            for (int i = 1; i < branchLines.Length; i++)
            {
                branchData.Requirements.Add(branchLines[i]);
            }

            conversation.LeadsTo.Add(branchData);
            lines.RemoveAt(0);
        }

        return conversation;
    }

    private static bool AssertMarker(string text, string marker)
    {
        Debug.Assert(text.StartsWith(marker), $"ERROR: {text} did not start with {marker}");
        return text.StartsWith(marker);
    }
}
