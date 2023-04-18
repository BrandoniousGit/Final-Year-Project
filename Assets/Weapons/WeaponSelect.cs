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
    public TextMeshProUGUI currentSelPrimDesc, currentSelSecDesc;

    private void Awake()
    {
        bool once1 = false;
        bool once2 = false;
        foreach (GunObject weapon in WeaponList)
        {
            if (weapon.m_invSlot == 0)
            {
                //List of primary weapons
                GameObject clone;
                clone = Instantiate(weaponSelectClone, primWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                clone.GetComponent<Button>().onClick.AddListener(delegate { AddToWeaponManager(weapon, weapon.m_invSlot); });
                if (!once1)
                {
                    currentSelPrim.text = string.Format("{0}", weapon.m_gunName);
                    currentSelPrimDesc.text = string.Format("{0}\n\nDamage: {1} | Clip Size: {2}\nReload Speed: {3}", weapon.m_gunDescription, weapon.m_damage, weapon.m_clipSize, weapon.m_reloadTime);
                    once1 = !once1;
                }
            }
            if (weapon.m_invSlot == 1)
            {
                //List of secondary weapons
                GameObject clone;
                clone = Instantiate(weaponSelectClone, secWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                clone.GetComponent<Button>().onClick.AddListener(delegate { AddToWeaponManager(weapon, weapon.m_invSlot); });
                if (!once2)
                {
                    currentSelSec.text = string.Format("{0}", weapon.m_gunName);
                    currentSelSecDesc.text = string.Format("{0}\n\nDamage: {1} | Clip Size: {2}\nReload Speed: {3}", weapon.m_gunDescription, weapon.m_damage, weapon.m_clipSize, weapon.m_reloadTime);
                    once2 = !once2;
                }
            }
        }
    }

    void AddToWeaponManager(GunObject weapon, int weaponSlot)
    {
        switch(weaponSlot)
        {
            //Text in the menu for weapon stats and information
            case 0:
                weaponsManager.GetComponent<WeaponsManager>().primaryWeapon = weapon;
                currentSelPrim.text = string.Format("{0}", weapon.m_gunName);
                currentSelPrimDesc.text = string.Format("{0}\n\nDamage: {1} | Clip Size: {2}\nReload Speed: {3}", weapon.m_gunDescription, weapon.m_damage, weapon.m_clipSize, weapon.m_reloadTime);

                break;
            case 1:
                weaponsManager.GetComponent<WeaponsManager>().secondaryWeapon = weapon;
                currentSelSec.text = string.Format("{0}", weapon.m_gunName);
                currentSelSecDesc.text = string.Format("{0}\n\nDamage: {1} | Clip Size: {2}\nReload Speed: {3}", weapon.m_gunDescription, weapon.m_damage, weapon.m_clipSize, weapon.m_reloadTime);
                break;
        }
    }
}
