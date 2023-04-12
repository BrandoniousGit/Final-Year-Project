using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    //Chance for each tile to spawn
    private float iChance, tChance, hChance, eChance, cChance;

    private Environment _tiles;
    private RoomList _roomList;
    private float rand;
    public bool canSpawnRoom = true;

    private void Awake()
    {
        _roomList = GameObject.FindGameObjectWithTag("LG_Tiles").GetComponent<RoomList>();
        _tiles = _roomList.levelEnvironment;

        iChance = _tiles.m_iChance;
        tChance = _tiles.m_tChance;
        hChance = _tiles.m_hChance;
        eChance = _tiles.m_eChance;
        cChance = _tiles.m_cChance;

        if (tag == "FirstSpawnPoint")
        {
            GameObject _spawnRoom = Instantiate(_tiles.hTiles[0], transform.position, transform.rotation);
            _spawnRoom.tag = "SpawnRoom";
        }
        else
        {
            Invoke(nameof(Spawn), 0.1f);
        }
    }

    private void Update()
    {
        //Debugging
        if (Input.GetKeyDown(KeyCode.G))
        {
            //Spawn();
        }
    }

    private float ReturnChance(int amount)
    {
        //Randomly picks a tile to spawn
        return amount switch
        {
            0 => iChance,
            1 => iChance + tChance,
            2 => iChance + tChance + hChance,
            3 => iChance + tChance + hChance + eChance,
            4 => iChance + tChance + hChance + eChance + cChance,
            _ => 0,
        };
    }

    private void Spawn()
    {
        if (canSpawnRoom)
        {
            rand = Random.Range(0, ReturnChance(4));
            GameObject clone;
            if (rand <= ReturnChance(0))
            {
                clone = Instantiate(_tiles.iTiles[Random.Range(0, _tiles.iTiles.Length)], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //I TILES
            }
            else if (rand > ReturnChance(0) && rand <= ReturnChance(1))
            {
                clone = Instantiate(_tiles.tTiles[Random.Range(0, _tiles.tTiles.Length)], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //T TILES
            }
            else if (rand > ReturnChance(1) && rand <= ReturnChance(2))
            {
                clone = Instantiate(_tiles.hTiles[Random.Range(0, _tiles.hTiles.Length)], transform.position, transform.rotation);
                //H TILES
            }
            else if (rand > ReturnChance(2) && rand <= ReturnChance(3))
            {
                clone = Instantiate(_tiles.eTiles[Random.Range(0, _tiles.eTiles.Length)], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //E TILES
            }
            else if (rand > ReturnChance(3) && rand <= ReturnChance(4))
            {
                clone = Instantiate(_tiles.cTiles[Random.Range(0, _tiles.cTiles.Length)], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //C TILES
            }
            //Tiles are rotated to make sure they face somewhere with an entrance
        }
        //Stops the spawner from creating more rooms after it's initial spawn
        canSpawnRoom = false;
    }

    private void ConflictSpawn(Vector3 pos, Quaternion rot)
    {
        Debug.LogWarning("Running conflict spawn!");
        rand = Random.Range(0, ReturnChance(4));
        GameObject clone;
        if (rand <= ReturnChance(0))
        {
            clone = Instantiate(_tiles.iTiles[Random.Range(0, _tiles.iTiles.Length)], pos, rot);
            clone.transform.LookAt(transform.parent.transform);
            //I TILES
        }
        else if (rand > ReturnChance(0) && rand <= ReturnChance(1))
        {
            clone = Instantiate(_tiles.tTiles[Random.Range(0, _tiles.tTiles.Length)], pos, rot);
            clone.transform.LookAt(transform.parent.transform);
            //T TILES
        }
        else if (rand > ReturnChance(1) && rand <= ReturnChance(2))
        {
            clone = Instantiate(_tiles.hTiles[Random.Range(0, _tiles.hTiles.Length)], pos, rot);
            //H TILES
        }
        else if (rand > ReturnChance(2) && rand <= ReturnChance(3))
        {
            clone = Instantiate(_tiles.eTiles[Random.Range(0, _tiles.eTiles.Length)], pos, rot);
            clone.transform.LookAt(transform.parent.transform);
            //E TILES
        }
        else if (rand > ReturnChance(3) && rand <= ReturnChance(4))
        {
            clone = Instantiate(_tiles.cTiles[Random.Range(0, _tiles.cTiles.Length)], pos, rot);
            clone.transform.LookAt(transform.parent.transform);
            //C TILES
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("FirstSpawnPoint"))
        {
            canSpawnRoom = false;
            return;
        }

        //Destroy the trigger if it hits another
        if(other.CompareTag("Spawnpoint"))
        {
            if (!other.gameObject.GetComponent<RoomSpawn>().canSpawnRoom)
            {
                canSpawnRoom = false;
            }
            if (other.gameObject.GetComponent<RoomSpawn>().canSpawnRoom && canSpawnRoom)
            {
                canSpawnRoom = false;
                ConflictSpawn(transform.position, transform.rotation);
                //Debug.Log("Conflict at\nX: " + transform.position.x / 32 + "\nZ: " + transform.position.z / 32);
            }
        }
    }
}