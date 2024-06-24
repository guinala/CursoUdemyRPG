using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Item", menuName = "Items/Weapon item")]
public class WeaponItem : InventoryItem
{
    public Weapon weapon;

    public override bool Equip()
    {
        if(WeaponContainer.Instance.equipedWapon == null)
        {
            return false;
        }

        WeaponContainer.Instance.EquipWeapon(this);
        return true;
    }

    public override bool Remove()
    {
        if(WeaponContainer.Instance.equipedWapon == null)
        {
            return false;
        }

        WeaponContainer.Instance.RemoveWeapon();
        return true;
    }
}
