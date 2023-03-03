using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    private float iChance, tChance, hChance, eChance, cChance;
    //Chances for each room to spawn
    //public int needsExit;
    //1 = FORWARD
    //2 = BACK
    //3 = LEFT
    //4 = RIGHT

    private Environment _tiles;
    public RoomList _roomList;
    private int rand;
    public bool canSpawnRoom = true;

    private void Start()
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
            Instantiate(_tiles.hTiles[0], transform.position, transform.rotation);
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
            Spawn();
        }
    }

    private float ReturnChance(int amount)
    {
        //Randomly picks a tile to spawn, make sure tile chances add to 100
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

    /*Vector3 returnRot(int roomType)
    {
        switch(roomType)
        {
            case 1:
                //I Tiles
                return needsExit switch
                {
                    1 => new Vector3(0, 90, 0),
                    2 => new Vector3(0, -90, 0),
                    _ => new Vector3(0, 0, 0),
                };
            case 2:
                //T Tiles
                return needsExit switch
                {
                    1 => new Vector3(0, 90, 0),
                    _ => new Vector3(0, 0, 0),
                };
            case 3:
                //Hub Tiles
                return new Vector3(0, 0, 0);
            case 4:
                //End Tiles
                return needsExit switch
                {
                    1 => new Vector3(0, 90, 0),
                    2 => new Vector3(0, -90, 0),
                    4 => new Vector3(0, 180, 0),
                    _ => new Vector3(0, 0, 0),
                };
            case 5:
                //Corner Tiles
                return needsExit switch
                {
                    1 => new Vector3(0, 90, 0),
                    4 => new Vector3(0, 180, 0),
                    _ => new Vector3(0, 0, 0),
                };
            default:
                return new Vector3(0, 0, 0);
        }
    }*/

    private void Spawn()
    {
        if (canSpawnRoom)
        {
            rand = Random.Range(0, 101);
            GameObject clone;
            if (rand <= ReturnChance(0))
            {
                clone = Instantiate(_tiles.iTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                _roomList.rooms.Append(clone);
                //I TILES
            }
            else if (rand > ReturnChance(0) && rand <= ReturnChance(1))
            {
                clone = Instantiate(_tiles.tTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                _roomList.rooms.Append(clone);
                //T TILES
            }
            else if (rand > ReturnChance(1) && rand <= ReturnChance(2))
            {
                clone = Instantiate(_tiles.hTiles[0], transform.position, transform.rotation);
                _roomList.rooms.Append(clone);
                //H TILES
            }
            else if (rand > ReturnChance(2) && rand <= ReturnChance(3))
            {
                clone = Instantiate(_tiles.eTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                _roomList.rooms.Append(clone);
                //E TILES
            }
            else if (rand > ReturnChance(3) && rand <= ReturnChance(4))
            {
                clone = Instantiate(_tiles.cTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                _roomList.rooms.Append(clone);
                //C TILES
            }
            //Tiles are rotated to make sure they face somewhere with an entrance
        }
        //Stops the spawner from creating more rooms after it's initial spawn
        canSpawnRoom = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroy the trigger if it hits another
        if(other.CompareTag("Spawnpoint"))
        {
            Destroy(gameObject);
        }
    }
}