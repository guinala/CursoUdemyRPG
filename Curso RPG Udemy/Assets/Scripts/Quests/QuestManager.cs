using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : Singleton<QuestManager>
{
    [Header("Character")] 
    [SerializeField] private Character character;

    [Header("Quests")]
    [SerializeField] private Quests[] availableQuests;

    [Header("Sheriff Quests")]
    [SerializeField] private InspectorQuestDescription questPrefab;
    [SerializeField] private Transform questContainer;

    [Header("Character Quests")]
    [SerializeField] private CharacterQuestDescription questCharacterPrefab;
    [SerializeField] private Transform questCharacterContainer;


    [Header("Quest Completed Panel")]
    [SerializeField] private GameObject questCompletedPanel;
    [SerializeField] private TextMeshProUGUI questCompletedText;
    [SerializeField] private TextMeshProUGUI questCompletedExperience;
    [SerializeField] private TextMeshProUGUI questCompletedGold;
    [SerializeField] private TextMeshProUGUI questCompletedItemQuantity;
    [SerializeField] private Image questCompletedItemIcon;

    public Quests questToClaim { get; private set; }

    private void Start()
    {
        LoadQuestInInspector();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            AddProgress("RookieHunter", 2);
            AddProgress("ProfessionalHunter", 2);
            AddProgress("MasterHunter", 2);
        }
    }

    private void LoadQuestInInspector()
    {

        for (int i = 0; i < availableQuests.Length; i++)
        {
           InspectorQuestDescription newQuest = Instantiate(questPrefab, questContainer);
            newQuest.ConfigureQuest(availableQuests[i]);
        }
        
    }

    private void AddQuestToLoad(Quests quest)
    {
        CharacterQuestDescription newQuest = Instantiate(questCharacterPrefab, questCharacterContainer);
        newQuest.ConfigureQuest(quest);
    }

    public void AddQuest(Quests quest)
    {
        AddQuestToLoad(quest);
    }

    private void OnEnable()
    {
        Quests.OnQuestCompleted += QuestCompleted;
    }

    private void OnDisable()
    {
        Quests.OnQuestCompleted -= QuestCompleted;
    }

    private void QuestCompleted(Quests quest)
    {
       questToClaim = QuestExists(quest.ID);

        if (questToClaim != null)
        {
            DisplayCompletedPanel(questToClaim);
        }
    }

    public void ClaimRewards()
    {
        if(questToClaim == null) return;

        character.CharacterExp.AddExp(questToClaim.ExpReward);
        CoinManager.Instance.AddCoins(questToClaim.GoldReward);
        Inventory.Instance.AddItem(questToClaim.itemRewards.item, questToClaim.itemRewards.quantity);
        questCompletedPanel.SetActive(false);
        questToClaim = null;
    }

    private void DisplayCompletedPanel(Quests completedQuest)
    {
        questCompletedPanel.SetActive(true);
        questCompletedText.text = completedQuest.Name;
        questCompletedExperience.text = completedQuest.ExpReward.ToString();
        questCompletedGold.text = completedQuest.GoldReward.ToString();
        questCompletedItemQuantity.text = completedQuest.itemRewards.quantity.ToString();
        questCompletedItemIcon.sprite = completedQuest.itemRewards.item.icon;
    }


    public void AddProgress(string QuestID, int quantity)
    {
        Quests updateQuest = QuestExists(QuestID);
        if (updateQuest != null)
        {
            updateQuest.UpdateQuest(quantity);
        }
    }

    private Quests QuestExists(string questID)
    {

        for (int i = 0; i < availableQuests.Length; i++)
        {
            if (availableQuests[i].ID == questID)
            {
                return availableQuests[i];
            }
        }
        return null;
    }
   
}
