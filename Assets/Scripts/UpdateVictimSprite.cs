using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Sirenix.OdinInspector;

public class UpdateVictimSprite : SerializedMonoBehaviour
{

    [System.Serializable]
    private class SpriteData
    {
        public SpriteRenderer spriteRenderer;
        public Sprite sprite;
        public bool hasOccured;
    }

    [SerializeField] Dictionary<string, SpriteData> spriteData;

    void Start()
    {
        DialogueManager.OnDialogueEnded += UpdateSprite;
    }

    private void UpdateSprite()
    {
        foreach(var key in spriteData)
        {
            if(DialogueManager.Instance.DialogueUnlocks.Contains(key.Key.ToLower()) && !key.Value.hasOccured)
            {
                key.Value.spriteRenderer.sprite = key.Value.sprite;
                key.Value.hasOccured = true;
            }
        }
    }
}