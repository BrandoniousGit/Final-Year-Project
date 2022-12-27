using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelect : MonoBehaviour
{
    public GunObject[] WeaponList;
    public GameObject weaponSelectClone, primWeapons, secWeapons, weaponsManager;
    public TextMeshProUGUI currentSelPrim, currentSelSec;

    private void Awake()
    {
        bool once1 = false;
        bool once2 = false;
        foreach (GunObject weapon in WeaponList)
        {
            if (weapon.m_invSlot == 0)
            {
                GameObject clone;
                clone = Instantiate(weaponSelectClone, primWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                clone.GetComponent<Button>().onClick.AddListener(delegate { AddToWeaponManager(weapon, weapon.m_invSlot); });
                if (!once1)
                {
                    currentSelPrim.text = string.Format("Current: {0}", weapon.m_gunName);
                    once1 = !once1;
                }
            }
            if (weapon.m_invSlot == 1)
            {
                GameObject clone;
                clone = Instantiate(weaponSelectClone, secWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                clone.GetComponent<Button>().onClick.AddListener(delegate { AddToWeaponManager(weapon, weapon.m_invSlot); });
                if (!once2)
                {
                    currentSelSec.text = string.Format("Current: {0}", weapon.m_gunName);
                    once2 = !once2;
                }
            }
        }
    }

    void AddToWeaponManager(GunObject weapon, int weaponSlot)
    {
        switch(weaponSlot)
        {
            case 0:
                weaponsManager.GetComponent<WeaponsManager>().primaryWeapon = weapon;
                currentSelPrim.text = string.Format("Current: {0}", weapon.m_gunName);
                break;
            case 1:
                weaponsManager.GetComponent<WeaponsManager>().secondaryWeapon = weapon;
                currentSelSec.text = string.Format("Current: {0}", weapon.m_gunName);
                break;
        }
    }
}
