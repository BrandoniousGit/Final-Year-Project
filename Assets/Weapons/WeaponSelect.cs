using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponSelect : MonoBehaviour
{
    public GunObject[] WeaponList;
    public GameObject weaponSelectClone, primWeapons, secWeapons;

    private void Start()
    {
        foreach(GunObject weapon in WeaponList)
        {
            switch(weapon.m_invSlot)
            {
                case 0:
                    Instantiate(weaponSelectClone, primWeapons.transform);
                    weaponSelectClone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                    Debug.Log(weapon.m_gunName);
                    break;
                case 1:
                    Instantiate(weaponSelectClone, secWeapons.transform);
                    weaponSelectClone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                    Debug.Log(weapon.m_gunName);
                    break;
                default:
                    throw new System.Exception("Unsupported inv slot.");
            }
        }
    }
}
