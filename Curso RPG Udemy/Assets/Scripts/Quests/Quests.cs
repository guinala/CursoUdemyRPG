using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quests", menuName = "Quests/Quest")]
public class Quests : ScriptableObject
{
    public static Action<Quests> OnQuestCompleted;
    [Header("Info")]
    public string Name;
    public string ID;
    public int QuantityTarget;

    [Header("Description")]
    [TextArea] public string Description;

    [Header("Rewards")]
    public float ExpReward;
    public int GoldReward;

    public QuestItemRewards itemRewards;

    [HideInInspector] public int actualQuantity;
    [HideInInspector] public bool isCompleted;


    public void UpdateQuest(int quantity)
    {
        actualQuantity += quantity;
        VerifyProgress();
    }


    private void VerifyProgress()
    {
        if (actualQuantity >= QuantityTarget)
        {
            actualQuantity = QuantityTarget;
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        if (isCompleted)
        {
            return;
        }

        isCompleted = true;
        OnQuestCompleted?.Invoke(this);
    }

    private void OnEnable()
    {
        isCompleted = false;
        actualQuantity = 0;
    }
}

[Serializable]
public class QuestItemRewards
{
    public InventoryItem item;
    public int quantity;
}