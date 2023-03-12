using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    public Environment levelEnvironment;
    public List<GameObject> rooms;

    public int maxRooms;
    public int minRooms;
    public int roomCount;
    public bool levelReady;

    private GameObject firstSpawn;

    private void Start()
    {
        DontDestroyOnLoad(this);
        firstSpawn = GameObject.FindGameObjectWithTag("FirstSpawnPoint");
        levelReady = false;
        //First check for if the level is big enough/too big after 2.5s
        Invoke(nameof(CheckRoomCount), 2.5f);
    }

    void CheckRoomCount()
    {
        //Checks for if the total amount of rooms are between the given values
        if (rooms.Count < minRooms || rooms.Count > maxRooms)
        {
            for (int i = rooms.Count-1; i >= 0; i--)
            {
                Destroy(rooms[i]);
                rooms.RemoveAt(i);
            }
            Instantiate(levelEnvironment.hTiles[0], firstSpawn.transform.position, firstSpawn.transform.rotation);
            Invoke(nameof(CheckRoomCount), 2.5f);
        }
        else
        {
            levelReady = true;
        }
    }

    private void Update()
    {
        if (!levelReady)
        {
            roomCount = rooms.Count;
        }
    }
}