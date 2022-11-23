using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour
{
    public GunObject gunObject;
    public GameObject weaponManager;
    public WeaponsManager weaponManagerScript;

    public TextMeshProUGUI gunInfo;

    private GameObject PrimaryWeaponClone, SecondaryWeaponClone;

    public int clipSize, reserveSize, upgrade;
    public float damage;

    private void GunInitialise(GunObject currentWeapon)
    {
        clipSize = currentWeapon.m_clipSize;
        reserveSize = currentWeapon.m_reserveSize;
        damage = currentWeapon.m_damage;
    }

    private void Awake()
    {
        if (GameObject.FindGameObjectWithTag("WeaponManager") != null)
        {
            weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
            weaponManagerScript = weaponManager.GetComponent<WeaponsManager>();

            PrimaryWeaponClone = Instantiate(weaponManagerScript.primaryWeapon.m_model, transform);
            SecondaryWeaponClone = Instantiate(weaponManagerScript.secondaryWeapon.m_model, transform);
            SecondaryWeaponClone.SetActive(false);
        }
        else { Debug.LogError("No Weapon Manager found!!");  }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (PrimaryWeaponClone.activeSelf == false)
            {
                SecondaryWeaponClone.SetActive(false);
                PrimaryWeaponClone.SetActive(true);
                gunObject = weaponManagerScript.primaryWeapon;
                GunInitialise(gunObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (SecondaryWeaponClone.activeSelf == false)
            {
                SecondaryWeaponClone.SetActive(true);
                PrimaryWeaponClone.SetActive(false);
                gunObject = weaponManagerScript.secondaryWeapon;
                GunInitialise(gunObject);
            }
        }
        gunInfo.text = string.Format("{0}: {1} | {2}", gunObject.m_gunName, gunObject.m_ammoInClip, gunObject.m_ammoInReserve);
    }
}
