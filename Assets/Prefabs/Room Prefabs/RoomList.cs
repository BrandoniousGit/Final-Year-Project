using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomList : MonoBehaviour
{
    public Environment levelEnvironment;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
}