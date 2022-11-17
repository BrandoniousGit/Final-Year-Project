using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Gun")]
public class GunObject : ScriptableObject
{
    public enum GunType
    {
        Burst,
        FullAuto,
        SemiAuto,
        Shotgun
    }

    public GunType gunType;

    public bool canShoot, reloading;

    public string gunName, gunDescription;
    public int invSlot, ammoInClip, ammoInReserve, clipSize, reserveSize;
    public float damage;
}
