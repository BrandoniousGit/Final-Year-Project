using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerLoader : MonoBehaviour
{
    public GameObject levelManager, weaponManager;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("WeaponManager") == null)
        {
            Instantiate(weaponManager);
        }

        if (GameObject.FindGameObjectWithTag("LevelManager") == null)
        {
            Instantiate(levelManager);
        }
    }
}
