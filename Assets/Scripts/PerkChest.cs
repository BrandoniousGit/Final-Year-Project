using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkChest : Interactable
{
    public override string GetDescription()
    {
        return "Press 'E' to open chest";
    }

    public override void Interact()
    {
        Instantiate(m_genericObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
    }
}
