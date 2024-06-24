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
            weaponIcon.sprite = item.weapon.iconWeapon;
            weaponIcon.gameObject.SetActive(true);
            skillIcon.sprite = item.weapon.skillIcon;
            skillIcon.gameObject.SetActive(true);
        }
    }

    public void RemoveWeapon()
    {
        weaponIcon.gameObject.SetActive(false);
        skillIcon.gameObject.SetActive(false);
        equipedWapon = null;
    }
    
}
