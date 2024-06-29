using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponContainer : Singleton<WeaponContainer>
{
    [SerializeField] private Image weaponIcon;
    [SerializeField] private Image skillIcon;

    public WeaponItem equipedWapon { get; set; }

    public void EquipWeapon(WeaponItem item)
    {
        if (weaponIcon != null)
        {
            equipedWapon = item;
            weaponIcon.sprite = item.weapon.iconWeapon;
            weaponIcon.gameObject.SetActive(true);

            if(equipedWapon.weapon.weaponType == WeaponType.Magic)
            {
                skillIcon.sprite = item.weapon.skillIcon;
                skillIcon.gameObject.SetActive(true);
            }

            Inventory.Instance.Character.CharacterAttack.EquipWeapon(item);
        }
    }

    public void RemoveWeapon()
    {
        weaponIcon.gameObject.SetActive(false);
        skillIcon.gameObject.SetActive(false);
        equipedWapon = null;
        Inventory.Instance.Character.CharacterAttack.RemoveWeapon();
    }
    
}
