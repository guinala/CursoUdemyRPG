using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootButton : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;

    public DropItem ItemToDrop { get; set; }

    public void ConfigureLootItem(DropItem dropItem)
    {
        ItemToDrop = dropItem;
        itemIcon.sprite = dropItem.Item.icon;
        itemName.text = $"{dropItem.Item.name} x {dropItem.amount}";
    }

    public void TakeLoot()
    {
        if(ItemToDrop == null)
        {
            return;
        }

        Inventory.Instance.AddItem(ItemToDrop.Item, ItemToDrop.amount);
        ItemToDrop.Looted = true;
        Destroy(gameObject);
    }
}
