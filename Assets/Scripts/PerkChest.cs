using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkChest : Interactable
{
    GameObject perk1, perk2;

    public override string GetDescription()
    {
        if (isSelected == false)
        {
            return "Press 'E' to open chest";
        }
        else
        {
            return "";
        }
    }

    public override void Interact()
    {
        if (isSelected == false)
        {
            perk1 = Instantiate(m_genericObject, transform.position + new Vector3(0, 1, 0.6f), new Quaternion(0, 0, 0, 1));
            perk2 = Instantiate(m_genericObject, transform.position + new Vector3(0, 1, -0.6f), new Quaternion(0, 0, 0, 1));
            isSelected = true;
        }
        else
        {

        }
    }
}
