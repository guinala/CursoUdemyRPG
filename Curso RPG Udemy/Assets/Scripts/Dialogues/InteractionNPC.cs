using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionNPC : MonoBehaviour
{
    [SerializeField] private GameObject buttonInteractive;
    [SerializeField] private NPCDialogue npcDialogue;

    public NPCDialogue NPCDialogue => npcDialogue;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.availableNPC = this;
            buttonInteractive.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            DialogueManager.Instance.availableNPC = null;
            buttonInteractive.SetActive(false);
        }
    }
}
