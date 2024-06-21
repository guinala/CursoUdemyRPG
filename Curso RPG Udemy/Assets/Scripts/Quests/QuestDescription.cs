using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questName;
    [SerializeField] TextMeshProUGUI questDescription;

    public Quests loadedQuest { get; set; }

    public virtual void ConfigureQuest (Quests questForLoad)
    {
        loadedQuest = questForLoad;
        questName.text = questForLoad.Name;
        questDescription.text = questForLoad.Description;
    }
}
