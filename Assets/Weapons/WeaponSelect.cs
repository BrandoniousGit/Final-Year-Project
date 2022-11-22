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
            if (weapon.m_invSlot == 0)
            {
                GameObject clone;
                clone = Instantiate(weaponSelectClone, primWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                Debug.Log(weapon.m_gunName);
            }
            if (weapon.m_invSlot == 1)
            {
                GameObject clone;
                clone = Instantiate(weaponSelectClone, secWeapons.transform);
                clone.GetComponentInChildren<TextMeshProUGUI>().text = weapon.m_gunName;
                Debug.Log(weapon.m_gunName);
            }
        }
    }
}
