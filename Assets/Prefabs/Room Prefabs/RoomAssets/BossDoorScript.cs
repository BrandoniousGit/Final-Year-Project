using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorScript : Interactable
{
    public GameObject _levelManager;

    public override string GetDescription()
    {
        if (_levelManager.GetComponent<LevelManager>().DoesPlayerHaveKey())
        {
            return "Press 'E' to Unlock";
        }

        return "Find the key on the floor to Unlock";
    }

    public void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    public override void Interact()
    {
        if (_levelManager.GetComponent<LevelManager>().DoesPlayerHaveKey())
        {
            Debug.Log("I am running");
        }
    }
}
