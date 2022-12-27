using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using static GunObject;

public class GunScript : MonoBehaviour
{
    public GunObject gunObject;
    public GameObject weaponManager, backupWeaponManager;
    public WeaponsManager weaponManagerScript;

    public TextMeshProUGUI gunInfo;

    private GameObject PrimaryWeaponClone, SecondaryWeaponClone;

    public int clipSize, reserveSize;
    public float damage;
    public bool reloading, canShoot;

    Camera cam;

    private void GunInitialise(GunObject currentWeapon)
    {
        clipSize = currentWeapon.m_clipSize;
        reserveSize = currentWeapon.m_reserveSize;
        damage = currentWeapon.m_damage;
    }

    private void Awake()
    {
        cam = Camera.main;

        if (GameObject.FindGameObjectWithTag("WeaponManager") != null)
        {
            weaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
            weaponManagerScript = weaponManager.GetComponent<WeaponsManager>();

            PrimaryWeaponClone = Instantiate(weaponManagerScript.primaryWeapon.m_model, transform);
            SecondaryWeaponClone = Instantiate(weaponManagerScript.secondaryWeapon.m_model, transform);
            SecondaryWeaponClone.SetActive(false);
            gunObject = weaponManagerScript.primaryWeapon;
            GunInitialise(gunObject);
        }
        else 
        {
            backupWeaponManager.SetActive(true);
            weaponManagerScript = backupWeaponManager.GetComponent<WeaponsManager>();

            PrimaryWeaponClone = Instantiate(weaponManagerScript.primaryWeapon.m_model, transform);
            SecondaryWeaponClone = Instantiate(weaponManagerScript.secondaryWeapon.m_model, transform);
            SecondaryWeaponClone.SetActive(false);
            gunObject = weaponManagerScript.primaryWeapon;
            GunInitialise(gunObject);
        }
    }

    private void Update()
    {
        //Shooting
        if (!reloading && CheckAmmo() && canShoot)
        {
            WeaponShoot(gunObject.m_gunType);
        }

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && !reloading && canShoot)
        {
            WeaponReload();
        }

        //Activating primary weapon and deactivating secondary
        if (Input.GetKeyDown(KeyCode.Alpha1) && !reloading)
        {
            if (!PrimaryWeaponClone.activeSelf)
            {
                SecondaryWeaponClone.SetActive(false);
                PrimaryWeaponClone.SetActive(true);
                gunObject = weaponManagerScript.primaryWeapon;
                GunInitialise(gunObject);
            }
        }

        //Activating secondary weapon and deactivating primary
        if (Input.GetKeyDown(KeyCode.Alpha2) && !reloading)
        {
            if (!SecondaryWeaponClone.activeSelf)
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
    void WeaponShoot(GunType gunType)
    {
        switch (gunObject.m_gunType)
        {
            //Semi Automatic weapons
            case GunType.SemiAuto:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CheckRay(gunObject.m_gunType);
                    gunObject.m_ammoInClip -= 1;
                    StartCoroutine("ShotDelay", gunObject.m_timeBetweenShot);
                }
                break;

            //Shotguns
            case GunType.Shotgun:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CheckRay(gunObject.m_gunType);
                    gunObject.m_ammoInClip -= 1;
                    StartCoroutine("ShotDelay", gunObject.m_timeBetweenShot);
                }
                break;

            //Full Automatic weapons
            case GunType.FullAuto:
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    CheckRay(gunObject.m_gunType);
                    gunObject.m_ammoInClip -= 1;
                    StartCoroutine("ShotDelay", gunObject.m_timeBetweenShot);
                }
                break;

            //Burst fire weapons
            case GunType.Burst:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    StartCoroutine(BurstDelay(gunObject.m_timeBetweenBurst, gunObject.m_burstCount));
                }
                break;

            //Lob shot weapons
            case GunType.NonHitscan:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    gunObject.m_ammoInClip -= 1;
                    StartCoroutine("CreateProjectile");
                    StartCoroutine("ShotDelay", gunObject.m_timeBetweenShot);
                }
                break;
        }
    }

    //Reload function
    void WeaponReload()
    {
        reloading = true;
        Debug.Log("Reloading!");
        StartCoroutine("Reloading", gunObject.m_reloadTime);
    }

    bool CheckAmmo()
    {
        if (gunObject.m_ammoInClip > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckRay(GunType gunType)
    {
        RaycastHit hit;
        if (gunType == GunType.Shotgun)
        {
            float randomX, randomY, randomZ;
            for (int i = 0; i < gunObject.m_shotgunPelletCount; i++)
            {
                randomX = Random.Range(-gunObject.m_shotgunSpread, gunObject.m_shotgunSpread);
                randomY = Random.Range(-gunObject.m_shotgunSpread, gunObject.m_shotgunSpread);
                randomZ = Random.Range(-gunObject.m_shotgunSpread, gunObject.m_shotgunSpread);

                if (Physics.Raycast(cam.transform.position, cam.transform.forward + new Vector3(randomX, randomY, randomZ), out hit, 100.0f))
                {
                    TrailRenderer trail = Instantiate(gunObject.m_bulletTrail, transform.position, Quaternion.identity);

                    StartCoroutine(SpawnTrail(trail, hit));
                    Debug.DrawRay(cam.transform.position, cam.transform.forward + new Vector3(randomX, randomY, randomZ), Color.red, 3.0f);
                    Debug.Log(hit.transform.name);
                }
            }
        }

        else
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100.0f))
            {
                TrailRenderer trail = Instantiate(gunObject.m_bulletTrail, transform.position, Quaternion.identity);

                StartCoroutine(SpawnTrail(trail, hit));
                Debug.DrawRay(cam.transform.position, cam.transform.forward * 5, Color.red, 3.0f);
                Debug.Log(hit.transform.name);
            }
        }
    }

    IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(trail.transform.position, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        trail.transform.position = hit.point;
        Instantiate(gunObject.m_ImpactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }

    IEnumerator CreateProjectile()
    {
        GameObject clone = Instantiate(gunObject.m_projectile, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        yield return new WaitForSeconds(1);
        Destroy(clone);
    }

    IEnumerator ShotDelay(float time)
    {
        canShoot = false;
        yield return new WaitForSeconds(time);
        canShoot = true;
    }

    IEnumerator BurstDelay(float time, int count)
    {
        canShoot = false;
        for (int i = 0; i < count; i++)
        {
            if (CheckAmmo())
            {
                CheckRay(gunObject.m_gunType);
                gunObject.m_ammoInClip -= 1;
                yield return new WaitForSeconds(time);
            }
        }
        StartCoroutine("ShotDelay", gunObject.m_timeBetweenShot - (time * count));
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