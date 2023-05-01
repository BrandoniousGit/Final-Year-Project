using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Gun")]
public class GunObject : ScriptableObject
{
    public GameObject m_model;
    public GameObject m_projectile;
    public TrailRenderer m_bulletTrail;
    public ParticleSystem m_ImpactParticleSystem;

    public enum GunType
    {
        Burst,
        FullAuto,
        SemiAuto,
        Shotgun,
        NonHitscan
    }

    public GunType m_gunType;

    public string m_gunName, m_gunDescription;
    public int m_invSlot, m_ammoInClip, m_clipSize, m_burstCount, m_shotgunPelletCount;
    public float m_damage, m_reloadTime, m_timeBetweenShot, m_timeBetweenBurst, m_shotgunSpread, m_aimSpread, m_explosionSize;
}
