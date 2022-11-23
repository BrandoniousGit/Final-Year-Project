using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelect : MonoBehaviour
{
    public GunObject[] WeaponList;
    public GameObject weaponSelectClone, primWeapons, secWeapons, weaponsManager;

    private void Start()
    {
        foreach(GunObject weapon in WeaponList)
        {
            if (weapon.m_invSlot == 0)
            {
                GameObject clone;
                clone = Instantiate(weaponSelectClone, primWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                clone.GetComponent<Button>().onClick.AddListener(delegate { AddToWeaponManager(weapon, weapon.m_invSlot); });
            }
            if (weapon.m_invSlot == 1)
            {
                GameObject clone;
                clone = Instantiate(weaponSelectClone, secWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                clone.GetComponent<Button>().onClick.AddListener(delegate { AddToWeaponManager(weapon, weapon.m_invSlot); });
            }
        }
    }

    void AddToWeaponManager(GunObject weapon, int weaponSlot)
    {
        switch(weaponSlot)
        {
            case 0:
                weaponsManager.GetComponent<WeaponsManager>().primaryWeapon = weapon;
                break;
            case 1:
                weaponsManager.GetComponent<WeaponsManager>().secondaryWeapon = weapon;
                break;
        }
    }
}
