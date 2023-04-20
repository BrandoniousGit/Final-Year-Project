using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class LG_Add : MonoBehaviour
{
    private RoomList _roomList;
    public GameObject bossDoor, enemyChecker;
    public bool bossRoom, once;
    public List<GameObject> doorList;

    void Awake()
    {
        //Finding the LG_Tiles to get access to the Room List
        _roomList = GameObject.FindGameObjectWithTag("LG_Tiles").GetComponent<RoomList>();
        _roomList.rooms.Add(gameObject);

        if (tag == "EndTile")
        {
            _roomList.endRooms.Add(gameObject);
        }

        if (Random.Range(0,100) > 50)
        {
            enemyChecker.GetComponent<EnemyCheckerScript>().isEnemyRoom = true;
        }

        once = false;
    }

    public void SetBossRoom()
    {
        //Sets the room to the boss room
        bossRoom = true;
    }

    void Update()
    {
        if (bossRoom)
        {
            bossDoor.SetActive(true);
        }

        if (gameObject.tag == "SpawnRoom")
        {
            enemyChecker.GetComponent<EnemyCheckerScript>().isEnemyRoom = false;
        }

        if (_roomList.levelReady)
        {
            //If the level is started and the player enters an enemy room, the walls go up and a fight starts
            if (enemyChecker.GetComponent<EnemyCheckerScript>().isEnemyRoom)
            {
                if (enemyChecker.GetComponent<EnemyCheckerScript>().playerEnteredRoom && !enemyChecker.GetComponent<EnemyCheckerScript>().fightFinished)
                {
                    for (int i = 0; i < doorList.Count; i++)
                    {
                        doorList[i].transform.position = Vector3.Lerp(doorList[i].transform.position, new Vector3(doorList[i].transform.position.x, 4, doorList[i].transform.position.z), 0.03f);
                    }
                }

                if (enemyChecker.GetComponent<EnemyCheckerScript>().fightFinished)
                {
                    for (int i = 0; i < doorList.Count; i++)
                    {
                        doorList[i].transform.position = Vector3.Lerp(doorList[i].transform.position, new Vector3(doorList[i].transform.position.x, -3, doorList[i].transform.position.z), 0.03f);
                    }
                }
            }
        }
    }
}