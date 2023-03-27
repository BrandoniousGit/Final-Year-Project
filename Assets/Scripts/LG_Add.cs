using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LG_Add : MonoBehaviour
{
    private RoomList _roomList;
    public GameObject bossDoor;
    public bool bossRoom;
    void Awake()
    {
        _roomList = GameObject.FindGameObjectWithTag("LG_Tiles").GetComponent<RoomList>();
        _roomList.rooms.Add(gameObject);

        if (tag == "EndTile")
        {
            _roomList.endRooms.Add(gameObject);
        }
    }

    public void SetBossRoom()
    {
        bossRoom = true;
    }

    void Update()
    {
        if (bossRoom)
        {
            bossDoor.SetActive(true);
        }
    }
}