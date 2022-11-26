using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunScript : MonoBehaviour
{
    public GunObject gunObject;
    public GameObject weaponManager, backupWeaponManager;
    public WeaponsManager weaponManagerScript;

    public TextMeshProUGUI gunInfo;

    private GameObject PrimaryWeaponClone, SecondaryWeaponClone;

    public int clipSize, reserveSize;
    public float damage;
    public bool reloading;

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
            GunInitialise(gunObject);
        }
        else 
        {
            backupWeaponManager.SetActive(true);
            weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
            weaponManagerScript = weaponManager.GetComponent<WeaponsManager>();

            PrimaryWeaponClone = Instantiate(weaponManagerScript.primaryWeapon.m_model, transform);
            SecondaryWeaponClone = Instantiate(weaponManagerScript.secondaryWeapon.m_model, transform);
            SecondaryWeaponClone.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloading == false && gunObject.m_ammoInClip > 0)
        {
            WeaponShoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && reloading == false)
        {
            WeaponReload();
        }


        //Activating primary weapon and deactivating secondary
        if (Input.GetKeyDown(KeyCode.Alpha1) && reloading == false)
        {
            if (PrimaryWeaponClone.activeSelf == false)
            {
                SecondaryWeaponClone.SetActive(false);
                PrimaryWeaponClone.SetActive(true);
                gunObject = weaponManagerScript.primaryWeapon;
                GunInitialise(gunObject);
            }
        }

        //Activating secondary weapon and deactivating primary
        if (Input.GetKeyDown(KeyCode.Alpha2) && reloading == false)
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

    //Shoot function
    void WeaponShoot()
    {
        gunObject.m_ammoInClip -= 1;
        // Shooting goes here //
    }

    //Reload function
    void WeaponReload()
    {
        reloading = true;
        Debug.Log("Reloading!");
        StartCoroutine("Reloading", gunObject.m_reloadTime);
    }

    IEnumerator Reloading(float reloadTime)
    {
        yield return new WaitForSeconds(reloadTime);
        if (gunObject.m_ammoInReserve + gunObject.m_ammoInClip < clipSize)
        {
            gunObject.m_ammoInClip += gunObject.m_ammoInReserve;
            gunObject.m_ammoInReserve = 0;
        }
        else
        {
            int diff = clipSize - gunObject.m_ammoInClip;
            gunObject.m_ammoInClip = clipSize;
            gunObject.m_ammoInReserve -= diff;
        }
        reloading = false;
        Debug.Log("Reloaded");
    }
}