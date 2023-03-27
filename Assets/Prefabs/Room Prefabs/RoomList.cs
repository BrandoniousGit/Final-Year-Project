using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    public Environment levelEnvironment;
    public List<GameObject> rooms;
    public List<GameObject> endRooms;

    public GameObject _player, _canvas, bossKey, chest, bossRoomObject;

    private int maxRooms;
    private int minRooms;
    public int roomCount, endRoomCount;
    public bool levelReady;

    private GameObject firstSpawn;

    private void Start()
    {
        maxRooms = levelEnvironment.m_maxTiles;
        minRooms = levelEnvironment.m_minTiles;

        DontDestroyOnLoad(this);
        firstSpawn = GameObject.FindGameObjectWithTag("FirstSpawnPoint");
        levelReady = false;
        //First check for if the level is big enough/too big after 2.0s
        Invoke(nameof(CheckRoomCount), 1.5f);
    }

    void CheckRoomCount()
    {
        //Checks for if the total amount of rooms are between the given values
        if (rooms.Count < minRooms || rooms.Count > maxRooms)
        {
            ResetStage();
        }

        else if (endRoomCount < 1)
        {
            ResetStage();
        }

        else
        {
            int bossRoom = Random.Range(0, endRoomCount - 1);

            int keyRoom = Random.Range(1, roomCount - 1);

            while (keyRoom == bossRoom)
            {
                keyRoom = Random.Range(1, roomCount - 1);
            }

            int chestRoom = Random.Range(1, roomCount - 1);

            while (chestRoom == bossRoom || chestRoom == keyRoom)
            {
                chestRoom = Random.Range(1, roomCount - 1);
            }

            Debug.Log(bossRoom);

            Instantiate(bossKey, rooms[keyRoom].transform.position + new Vector3(0, 1, 0), rooms[keyRoom].transform.rotation);
            Instantiate(chest, rooms[chestRoom].transform.position + new Vector3(0, 1, 0), rooms[keyRoom].transform.rotation);

            endRooms[bossRoom].GetComponent<LG_Add>().SetBossRoom();

            levelReady = true;
        }
    }

    private void ResetStage()
    {
        for (int i = rooms.Count - 1; i >= 0; i--)
        {
            Destroy(rooms[i]);
            rooms.RemoveAt(i);
        }

        for (int i = endRooms.Count - 1; i >= 0; i--)
        {
            Destroy(endRooms[i]);
            endRooms.RemoveAt(i);
        }

        Instantiate(levelEnvironment.hTiles[0], firstSpawn.transform.position, firstSpawn.transform.rotation);
        Invoke(nameof(CheckRoomCount), 1.5f);
    }

    private void Update()
    {
        if (!levelReady)
        {
            roomCount = rooms.Count;
            endRoomCount = endRooms.Count;
        }
    }
}