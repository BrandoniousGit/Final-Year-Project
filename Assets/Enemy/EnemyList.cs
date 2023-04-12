using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public List<GameObject> enemies;

    public List<GameObject> GetEnemyList()
    {
        return enemies;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
