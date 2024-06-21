using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InspectorQuestDescription : QuestDescription
{
    [SerializeField] private TextMeshProUGUI questRewards;

    public override void ConfigureQuest(Quests questForLoad)
    {
        base.ConfigureQuest(questForLoad);
        questRewards.text = $"Rewards: {questForLoad.GoldReward}"
                            + $"\n Exp: {questForLoad.ExpReward}"
                            + $"\n Items:{questForLoad.itemRewards.item.name} x {questForLoad.itemRewards.quantity}";

    }

    public void AcceptQuest()
    {
       if(loadedQuest == null) return;

       QuestManager.Instance.AddQuest(loadedQuest);
        gameObject.SetActive(false);
    }
}
