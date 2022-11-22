using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Gun")]
public class GunObject : ScriptableObject
{
    public GameObject m_model;

    public enum GunType
    {
        Burst,
        FullAuto,
        SemiAuto,
        Shotgun,
        NonHitscan
    }

    public GunType m_gunType;

    public bool m_canShoot, m_reloading;

    public string m_gunName, m_gunDescription;
    public int m_invSlot, m_ammoInClip, m_ammoInReserve, m_clipSize, m_reserveSize;
    public float m_damage;
}
