using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkChest : Interactable
{
    public GameObject perk1Spawn, perk2Spawn;
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
            gameObject.GetComponent<Animator>().SetTrigger("ChestOpened");
            perk1 = Instantiate(m_genericObject, perk1Spawn.transform.position, new Quaternion(0, 0, 0, 1));
            perk2 = Instantiate(m_genericObject, perk2Spawn.transform.position, new Quaternion(0, 0, 0, 1));
            isSelected = true;
        }

        else
        {

        }
    }
}
