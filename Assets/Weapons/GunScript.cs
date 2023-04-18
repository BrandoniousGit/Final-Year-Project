using System.Collections;
using UnityEngine;
using TMPro;
using static GunObject;

public class GunScript : MonoBehaviour
{
    public GunObject gunObject;
    public GameObject weaponManager, backupWeaponManager, muzzleFlash;
    public WeaponsManager weaponManagerScript;

    public TextMeshProUGUI gunInfo;

    private GameObject PrimaryWeaponClone, SecondaryWeaponClone;
    private Vector3 weaponMuzzlePos;

    private GameObject m_currentWeapon;

    public int clipSize, ammoInClip;
    public float damage;
    public bool reloading, canShoot;

    private Camera cam;

    private void GunInitialise(GunObject currentWeapon)
    {
        clipSize = currentWeapon.m_clipSize;
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
            m_currentWeapon = PrimaryWeaponClone;
            SecondaryWeaponClone.SetActive(false);
            gunObject = weaponManagerScript.primaryWeapon;
            GunInitialise(gunObject);
            gunObject.m_ammoInClip = clipSize;
        }
        else 
        {
            backupWeaponManager.SetActive(true);
            weaponManagerScript = backupWeaponManager.GetComponent<WeaponsManager>();

            PrimaryWeaponClone = Instantiate(weaponManagerScript.primaryWeapon.m_model, transform);
            SecondaryWeaponClone = Instantiate(weaponManagerScript.secondaryWeapon.m_model, transform);
            m_currentWeapon = PrimaryWeaponClone;
            SecondaryWeaponClone.SetActive(false);
            gunObject = weaponManagerScript.primaryWeapon;
            GunInitialise(gunObject);
            gunObject.m_ammoInClip = clipSize;
        }
    }

    private void Update()
    {
        if (cam == null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera") == null)
            {
                return;
            }
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        //Shooting
        if (!reloading && CheckAmmo() && canShoot)
        {
            WeaponShoot(gunObject.m_gunType);
        }

        //Reloading
        if (Input.GetKeyDown(KeyCode.R) && !reloading && canShoot && gunObject.m_ammoInClip != clipSize)
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
                m_currentWeapon = PrimaryWeaponClone;
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
                m_currentWeapon = SecondaryWeaponClone;
                gunObject = weaponManagerScript.secondaryWeapon;
                GunInitialise(gunObject);
            }
        }
        gunInfo.text = string.Format("{0}: {1} | {2}", gunObject.m_gunName, gunObject.m_ammoInClip, gunObject.m_clipSize);
    }

    //Shoot function
    void WeaponShoot(GunType gunType)
    {
        switch (gunType)
        {
            //Semi Automatic weapons
            case GunType.SemiAuto:
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    CheckRay(gunObject.m_gunType);
                    gunObject.m_ammoInClip -= 1;
                    m_currentWeapon.gameObject.GetComponent<Animator>().SetTrigger("Shoot");
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
                    m_currentWeapon.gameObject.GetComponent<Animator>().SetTrigger("Shoot");
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
        m_currentWeapon.gameObject.GetComponent<Animator>().SetTrigger("Reload");
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
        weaponMuzzlePos = transform.TransformPoint(FindGameObjectInChildWithTag(gunObject.m_model, "GunFront").transform.position);
        //Checking for if shotgun impacts walls/enemies
        if (gunType == GunType.Shotgun)
        {
            float randomX, randomY, randomZ;
            for (int i = 0; i < gunObject.m_shotgunPelletCount; i++)
            {
                //Random Spread
                randomX = Random.Range(-gunObject.m_shotgunSpread, gunObject.m_shotgunSpread);
                randomY = Random.Range(-gunObject.m_shotgunSpread, gunObject.m_shotgunSpread);
                randomZ = Random.Range(-gunObject.m_shotgunSpread, gunObject.m_shotgunSpread);

                Vector3 RandomVec = new Vector3(randomX, randomY, randomZ);

                if (Physics.Raycast(cam.transform.position, cam.transform.forward + RandomVec, out RaycastHit hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
                {
                    WhatDidIHit(hit);

                    SpawnMuzzleFlash();

                    TrailRenderer trail = Instantiate(gunObject.m_bulletTrail, weaponMuzzlePos, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, hit.point));
                }
                else //Bullet miss
                {
                    SpawnMuzzleFlash();

                    TrailRenderer trail = Instantiate(gunObject.m_bulletTrail, weaponMuzzlePos, Quaternion.identity);
                    StartCoroutine(SpawnTrail(trail, cam.transform.position + cam.transform.forward * 30));
                }
            }
        }
        //Checking for any other hitscan weapons
        else
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, Mathf.Infinity, 1, QueryTriggerInteraction.Ignore))
            {
                WhatDidIHit(hit);

                SpawnMuzzleFlash();

                TrailRenderer trail = Instantiate(gunObject.m_bulletTrail, weaponMuzzlePos, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, hit.point));
            }
            else //Bullet miss
            {
                SpawnMuzzleFlash();

                TrailRenderer trail = Instantiate(gunObject.m_bulletTrail, weaponMuzzlePos, Quaternion.identity);
                StartCoroutine(SpawnTrail(trail, cam.transform.position + cam.transform.forward * 30));
            }
        }
    }

    void SpawnMuzzleFlash()
    {
        GameObject muzzleflashClone = Instantiate(muzzleFlash, weaponMuzzlePos, Quaternion.identity, transform);
        StartCoroutine(DestroyAfterX(muzzleflashClone, 0.3f));
    }

    void WhatDidIHit(RaycastHit _hit)
    {
        EnemyAgent hitEnemyScript;

        switch (_hit.transform.gameObject.tag)
        {
            case "Enemy":
                hitEnemyScript = _hit.transform.GetComponent<EnemyAgent>();
                hitEnemyScript.TakeDamage(damage);
                Instantiate(gunObject.m_ImpactParticleSystem, _hit.point, Quaternion.LookRotation(_hit.normal), _hit.transform);
                break;
            default:
                Instantiate(gunObject.m_ImpactParticleSystem, _hit.point, Quaternion.LookRotation(_hit.normal));
                break;
        }
    }

    public static GameObject FindGameObjectInChildWithTag(GameObject parent, string tag)
    {
        Transform t = parent.transform;

        for (int i = 0; i < t.childCount; i++)
        {
            if (t.GetChild(i).gameObject.tag == tag)
            {
                return t.GetChild(i).gameObject;
            }

        }

        return null;
    }

    // ===========================================================ENUMERATORS===========================================================

    IEnumerator SpawnTrail(TrailRenderer _trail, Vector3 _hit)
    {
        float distance = Vector3.Distance(_trail.transform.position, _hit);
        float startDistance = distance;

        //Sets speed of trail to be the same for any distance
        while (distance > 0)
        {
            _trail.transform.position = Vector3.Lerp(_trail.transform.position, _hit, 1 - (distance / startDistance));
            distance -= Time.deltaTime * 300;

            yield return null;
        }
        _trail.transform.position = _hit;

        Destroy(_trail.gameObject, _trail.time);
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
        gunObject.m_ammoInClip = clipSize;

        reloading = false;
        Debug.Log("Reloaded");
    }

    IEnumerator DestroyAfterX(GameObject objectDestroy, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(objectDestroy);
    }
}