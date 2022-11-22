using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GunObject gunObject;

    public int clipSize, reserveSize, upgrade;
    public float damage;

    private void GunInitialise(GunObject currentWeapon)
    {
        clipSize = currentWeapon.m_clipSize;
        reserveSize = currentWeapon.m_reserveSize;
        damage = currentWeapon.m_damage;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
        }
    }
}
