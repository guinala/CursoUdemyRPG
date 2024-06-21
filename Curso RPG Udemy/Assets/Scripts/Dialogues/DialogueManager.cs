using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Globalization;

public class DialogueManager : Singleton<DialogueManager>
{
    [SerializeField] private Image NPCIcon;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI NPCName;
    [SerializeField] private TextMeshProUGUI NPCConversation;

    public InteractionNPC availableNPC { get; set; }

    private Queue<string> sentencesSecuence;
    private bool AnimatedText;
    private bool farewellDisplayed;

    private void Start()
    {
        sentencesSecuence = new Queue<string>();
    }

    private void Update()
    {
        if(availableNPC == null)
        { 
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConfigurePanel(availableNPC.NPCDialogue);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(farewellDisplayed)
            {
                SetDialoguePanel(false);
                farewellDisplayed = false;
                return;
            }

            if(availableNPC.NPCDialogue.extraInteractionAvailable)
            {
                Debug.Log("Ultra caoasdas");
                UIManager.Instance.ShowExtraInteraction(availableNPC.NPCDialogue.extraInteraction);
                SetDialoguePanel(false);
                return;
            }

            if(AnimatedText)
            {
                NextDialogue();
            }
        }
    }
    

    public void SetDialoguePanel(bool state)
    {
        dialoguePanel.SetActive(state);
    }

    private void ConfigurePanel(NPCDialogue dialogue)
    {
        SetDialoguePanel(true);
        LoadDialogueSecuence(dialogue);
        NPCIcon.sprite = dialogue.npcSprite;
        NPCName.text = $"{dialogue.npcName}:";
        //NPCConversation.text = dialogue.greetingDialogue;
        DisplayText(dialogue.greetingDialogue);

    }

    private void NextDialogue()
    {
        if(availableNPC == null)
        {
            return;
        }

        if(farewellDisplayed)
        {
            return;
        }

        if (sentencesSecuence.Count == 0)
        {
            string farewell = availableNPC.NPCDialogue.fareWell;
            DisplayText(farewell);
            farewellDisplayed = true;
            return;
        }

        string nextDialogue = sentencesSecuence.Dequeue();
        DisplayText(nextDialogue);
    }

    private void LoadDialogueSecuence(NPCDialogue dialogue)
    {
        if(dialogue == null || dialogue.dialogueTexts.Length <= 0)
        {
            return;
        }

        for(int i = 0; i < dialogue.dialogueTexts.Length; i++)
        {
            sentencesSecuence.Enqueue(dialogue.dialogueTexts[i].sentence);
        }
    }

    private IEnumerator AnimateText (string sentence)
    {
        AnimatedText = false;
        NPCConversation.text = "";
        char[] letras = sentence.ToCharArray();

        for(int i = 0; i < letras.Length; i++)
        {
            NPCConversation.text += letras[i];
            yield return new WaitForSeconds(0.05f);
        }

        AnimatedText = true;
    }


    private void DisplayText(string sentence)
    {
        StartCoroutine(AnimateText(sentence));
    }
}
