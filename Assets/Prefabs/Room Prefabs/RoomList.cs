using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    public Environment levelEnvironment;
    public GameObject[] rooms;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}