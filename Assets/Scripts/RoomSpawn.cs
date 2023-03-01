using Unity.VisualScripting;
using UnityEngine;

public class RoomSpawn : MonoBehaviour
{
    private readonly float iChance = 25f, tChance = 15f, hChance = 5f, eChance = 35f, cChance = 20f;
    //Chances for each room to spawn
    //public int needsExit;
    //1 = FORWARD
    //2 = BACK
    //3 = LEFT
    //4 = RIGHT

    private Environment tiles;
    private int rand;
    public bool canSpawnRoom = true;

    private void Start()
    {
        tiles = GameObject.FindGameObjectWithTag("LG_Tiles").GetComponent<RoomList>().levelEnvironment;
        Invoke(nameof(Spawn), 0.1f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Spawn();
        }
    }

    private float ReturnChance(int amount)
    {
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
                clone = Instantiate(tiles.iTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //I TILES
            }
            else if (rand > ReturnChance(0) && rand <= ReturnChance(1))
            {
                clone = Instantiate(tiles.tTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //T TILES
            }
            else if (rand > ReturnChance(1) && rand <= ReturnChance(2))
            {
                clone = Instantiate(tiles.hTiles[0], transform.position, transform.rotation);
                //H TILES
            }
            else if (rand > ReturnChance(2) && rand <= ReturnChance(3))
            {
                clone = Instantiate(tiles.eTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //E TILES
            }
            else if (rand > ReturnChance(3) && rand <= ReturnChance(4))
            {
                clone = Instantiate(tiles.cTiles[0], transform.position, transform.rotation);
                clone.transform.LookAt(transform.parent.transform);
                //C TILES
            }
        }
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