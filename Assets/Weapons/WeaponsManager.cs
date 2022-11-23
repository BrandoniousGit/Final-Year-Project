using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public GunObject primaryWeapon, secondaryWeapon;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}