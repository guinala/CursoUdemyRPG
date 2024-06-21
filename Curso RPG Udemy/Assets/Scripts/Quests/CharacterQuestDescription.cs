using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI questTask;
    [SerializeField] private TextMeshProUGUI questExperience;
    [SerializeField] private TextMeshProUGUI questGold;

    [Header("Items")]
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemQuantity;

    private void Update()
    {
        if (loadedQuest.isCompleted) return;
       
        questTask.text = $"{loadedQuest.actualQuantity}/{loadedQuest.QuantityTarget}";
    }
    


    public override void ConfigureQuest(Quests questForLoad)
    {
        base.ConfigureQuest(questForLoad);
        questTask.text = $"{loadedQuest.actualQuantity}/{loadedQuest.QuantityTarget}";
        questExperience.text = questForLoad.ExpReward.ToString();
        questGold.text = questForLoad.GoldReward.ToString();

        itemIcon.sprite = questForLoad.itemRewards.item.icon;
        itemQuantity.text = questForLoad.itemRewards.quantity.ToString();
    }


    private void OnEnable()
    {
        if(loadedQuest.isCompleted)
        {
            gameObject.SetActive(false);
        }

        Quests.OnQuestCompleted += QuestCompleted;
    }

    private void OnDisable()
    {
        Quests.OnQuestCompleted -= QuestCompleted;
    }

    private void QuestCompleted(Quests quest)
    {
        if(loadedQuest.ID == quest.ID)
        {
            questTask.text = $"{loadedQuest.QuantityTarget}/{loadedQuest.QuantityTarget}";
            gameObject.SetActive(false);
        }
    }
}
