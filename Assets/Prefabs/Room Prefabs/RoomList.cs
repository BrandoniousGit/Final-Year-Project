using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomList : MonoBehaviour
{
    public Environment levelEnvironment;
    public List<GameObject> rooms;
    public List<GameObject> endRooms;

    public GameObject _player, _canvas, bossKey, chest, bossRoomObject, playerSpawnPoint;
    private int keyRoom, chestRoom;

    private bool keySpawned, chestSpawned;

    private int maxRooms;
    private int minRooms;
    public int roomCount, endRoomCount;
    public bool levelReady, once;

    private GameObject firstSpawn;

    private GameObject _levelManager;

    private NavMeshSurface navMesh;

    private void Start()
    {
        DontDestroyOnLoad(this);
        navMesh = GetComponent<NavMeshSurface>();
        firstSpawn = GameObject.FindGameObjectWithTag("FirstSpawnPoint");
        levelReady = false;
        //First check for if the level is big enough/too big after 2.0s
        Invoke(nameof(CheckRoomCount), 2.0f);
    }

    public void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager");

        levelEnvironment = _levelManager.GetComponent<LevelManager>().GetCurrentEnvironment();

        maxRooms = levelEnvironment.m_maxTiles;
        minRooms = levelEnvironment.m_minTiles;
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
            //Selecting boss room and collectable rooms
            int bossRoom = Random.Range(0, endRoomCount - 1);

            endRooms[bossRoom].GetComponent<LG_Add>().SetBossRoom();

            keyRoom = Random.Range(1, roomCount - 1);

            chestRoom = Random.Range(1, roomCount - 1);

            while (rooms[keyRoom].GetComponent<LG_Add>().bossDoor)
            {
                keyRoom = Random.Range(1, roomCount - 1);
            }

            while (rooms[chestRoom].GetComponent<LG_Add>().bossDoor)
            {
                chestRoom = Random.Range(1, roomCount - 1);
            }

            levelReady = true;
            _levelManager.GetComponent<LevelManager>().SetLevelIsReady(true);
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

        GameObject _spawnRoom = Instantiate(levelEnvironment.hTiles[0], firstSpawn.transform.position, firstSpawn.transform.rotation);
        _spawnRoom.tag = "SpawnRoom";
        Invoke(nameof(CheckRoomCount), 1.5f);
    }

    public NavMeshSurface GetNavMesh()
    {
        return navMesh;
    }

    private void Update()
    {
        if (!levelReady)
        {
            //Updates the room counts
            roomCount = rooms.Count;
            endRoomCount = endRooms.Count;
            return;
        }

        if (levelReady && !once)
        {
            //Once the level is ready, the player and canvas are instantiated
            Instantiate(_canvas, transform.position, transform.rotation);
            Instantiate(_player, new Vector3(0.25f, 2.3f, 8.0f), transform.rotation);
            navMesh.BuildNavMesh();
            once = true;
        }

        //Adds the key to the level, invisible if the room it's in has enemies
        if (rooms[keyRoom].GetComponent<LG_Add>().enemyChecker.GetComponent<EnemyCheckerScript>().fightFinished && !keySpawned)
        {
            Instantiate(bossKey, rooms[keyRoom].transform.position + new Vector3(0, 3, 0), rooms[keyRoom].transform.rotation);
            keySpawned = true;
        }
        else if (!rooms[keyRoom].GetComponent<LG_Add>().enemyChecker.GetComponent<EnemyCheckerScript>().isEnemyRoom && !keySpawned)
        {
            Instantiate(bossKey, rooms[keyRoom].transform.position + new Vector3(0, 3, 0), rooms[keyRoom].transform.rotation);
            keySpawned = true;
        }

        //Adds the chest to the level, invisible if the room it's in has enemies
        if (rooms[chestRoom].GetComponent<LG_Add>().enemyChecker.GetComponent<EnemyCheckerScript>().fightFinished && !chestSpawned)
        {
            Instantiate(chest, rooms[chestRoom].transform.position + new Vector3(0, 1, 0), rooms[chestRoom].transform.rotation);
            chestSpawned = true;
        }
        else if (!rooms[chestRoom].GetComponent<LG_Add>().enemyChecker.GetComponent<EnemyCheckerScript>().isEnemyRoom && !chestSpawned)
        {
            Instantiate(chest, rooms[chestRoom].transform.position + new Vector3(0, 1, 0), rooms[chestRoom].transform.rotation);
            chestSpawned = true;
        }
    }
}