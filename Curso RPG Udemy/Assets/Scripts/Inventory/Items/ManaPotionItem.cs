using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mana Potion", menuName = "Items/Mana Potion")]
public class ManaPotionItem : InventoryItem
{
    [Header("Effects")]
    public float manaAmount;

    public override bool Use()
    {
        if(Inventory.Instance.Character.CharacterMana.CanRestore)
        {
            Inventory.Instance.Character.CharacterMana.RestoreManaItem(manaAmount);
            return true;
        }

        return false;
    }
}
