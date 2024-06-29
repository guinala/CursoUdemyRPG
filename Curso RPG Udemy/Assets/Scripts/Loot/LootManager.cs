using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : Singleton<LootManager>
{
    [SerializeField] private GameObject lootPanelPrefab;
    [SerializeField] private Transform lootContainer;
    [SerializeField] private LootButton lootButtonPrefab;

    public void DisplayLoot(EnemyLoot loot)
    {
        lootPanelPrefab.SetActive(true);
        if(BusyContainer())
        {
           foreach(Transform child in lootContainer.transform)
            {
               Destroy(child.gameObject);
           }
        }

        for(int i = 0; i < loot.SelectedItems.Count; i++)
        {
            LoadLootPanel(loot.SelectedItems[i]);
        }
    }

    public void ClosePanel()
    {
        lootButtonPrefab.gameObject.SetActive(false);
    }

    private void LoadLootPanel(DropItem dropItem)
    {
        if (dropItem.Looted)
        {
            return;
        }

        LootButton button = Instantiate(lootButtonPrefab, lootContainer);
        button.ConfigureLootItem(dropItem);
        button.transform.SetParent(lootContainer);
    }

    private bool BusyContainer()
    {
        LootButton[] children = lootContainer.GetComponentsInChildren<LootButton>();

        if(children.Length > 0)
        {
            return true;
        }
        return false;
    }
}
