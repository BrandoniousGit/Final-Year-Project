using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : Interactable
{
    public GameObject key;
    public GameObject _levelManager;

    public void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager");
    }

    public override string GetDescription()
    {
        return "Press 'E' to pickup Key";
    }

    public override void Interact()
    {
        _levelManager.GetComponent<LevelManager>().SetPlayerHasKey(true);
        gameObject.SetActive(false);
    }
}
