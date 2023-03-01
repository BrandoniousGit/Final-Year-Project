using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPickup : Interactable
{
    private int random;

    public override string GetDescription()
    {
        if (isSelected == false)
        {
            return "Press 'E' to pickup " + random;
        }
        else
        {
            return "";
        }
    }

    private void Awake()
    {
        random = Random.Range(0, 10);
    }

    public override void Interact()
    {

    }
}
