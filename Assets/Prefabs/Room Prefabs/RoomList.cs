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

    private GameObject firstSpawn;

    private void Start()
    {
        DontDestroyOnLoad(this);
        firstSpawn = GameObject.FindGameObjectWithTag("FirstSpawnPoint");
    }

    void CheckRoomCount()
    {
        if (rooms.Count < minRooms || rooms.Count > maxRooms)
        {
            for (int i = rooms.Count-1; i >= 0; i--)
            {
                Destroy(rooms[i]);
                rooms.RemoveAt(i);
            }
            Instantiate(levelEnvironment.hTiles[0], firstSpawn.transform.position, firstSpawn.transform.rotation);
        }
    }

    private void Update()
    {
        roomCount = rooms.Count;
        //Debugging
        if (Input.GetKeyDown(KeyCode.G))
        {
            CheckRoomCount();
        }
    }
}