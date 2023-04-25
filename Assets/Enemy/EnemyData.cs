using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Enemy")]
public class EnemyData : ScriptableObject
{
    public GameObject m_model;
    public GameObject m_projectile;
    public Vector3 m_attackOffset;

    public uint m_enemyId;

    public float m_maxHP, m_damage, m_moveSpeed;
    public string m_name;
}
