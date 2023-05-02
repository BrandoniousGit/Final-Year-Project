using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorScript : Interactable
{
    private LevelManager _levelManager;

    public override string GetDescription()
    {
        if (_levelManager.DoesPlayerHaveKey())
        {
            return "Press 'E' to Unlock";
        }

        return "Find the key on the floor to Unlock";
    }

    public void Awake()
    {
        _levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    public override void Interact()
    {
        if (_levelManager.DoesPlayerHaveKey())
        {
            _levelManager.NextStage();
        }
    }
}
