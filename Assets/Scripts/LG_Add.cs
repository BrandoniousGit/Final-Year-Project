using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LG_Add : MonoBehaviour
{
    private RoomList _roomList;
    void Awake()
    {
        _roomList = GameObject.FindGameObjectWithTag("LG_Tiles").GetComponent<RoomList>();
        _roomList.rooms.Add(gameObject);
    }
}