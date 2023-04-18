using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCheckerScript : MonoBehaviour
{
    public List<GameObject> agents;
    public List<GameObject> aliveEnemies;

    public EnemyList enemyManager;

    private LevelManager levelManager;

    public bool playerEnteredRoom, fightFinished, once, isEnemyRoom;

    public void Awake()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyList>();
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        agents = enemyManager.GetEnemyList();
        once = false;
        fightFinished = false;
        playerEnteredRoom = false;
    }

    public void Update()
    {
        if (!once && playerEnteredRoom && levelManager.IsLevelReady() && isEnemyRoom)
        {
            /*for (int i = 0; i < Random.Range(2,4); i++)
            {
                Instantiate(agents[0]);
            }*/
            aliveEnemies.Add(Instantiate(agents[Random.Range(0, 2)], transform.position, transform.rotation));
            once = true;
        }

        for (int i = 0; i < aliveEnemies.Count; i++)
        {
            if (!aliveEnemies[i].GetComponent<EnemyAgent>().IsAlive())
            {
                aliveEnemies.RemoveAt(i);
            }
        }

        if (!fightFinished && playerEnteredRoom && aliveEnemies.Count <= 0)
        {
            fightFinished = true;
        }
    }

    public bool PlayerEnteredRoom()
    {
        return playerEnteredRoom;
    }

    public bool FightHasFinished()
    {
        return fightFinished;
    }
}