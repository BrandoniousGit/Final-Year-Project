using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Enemy")]
public class EnemyData : ScriptableObject
{
    public float m_maxHP, m_damage, m_moveSpeed;
    public string m_name;
}
