using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ExtraInteraction
{
    Shop,
    Craft,
    Quest
}

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "NPC/Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [Header("Info")]
    public string npcName;
    public Sprite npcSprite;
    public ExtraInteraction extraInteraction;
    public bool extraInteractionAvailable;

    [Header("Greeting")]
    [TextArea] public string greetingDialogue;

    [Header("Chat")]
    public DialogueText[] dialogueTexts;

    [Header("Farewell")]
    [TextArea] public string fareWell;
}


[Serializable]
public class DialogueText
{
    [TextArea] public string sentence;
}
