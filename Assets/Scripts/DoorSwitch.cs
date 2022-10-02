using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSwitch : Interactable
{
    public Vector3 doorOffset;
    private Vector3 velocity = Vector3.zero;
    public float maxH, minH;

    private void Start()
    {
        isOn = true;
    }

    public override string GetDescription()
    {
        string description = "Press 'RMB' to open door";
        return description;
    }

    public override void Interact()
    {
        isOn = !isOn;
    }

    private void Update()
    {
        if (isOn == false)
        {
            m_door.transform.position = Vector3.SmoothDamp(m_door.transform.position, m_door.transform.position + doorOffset, ref velocity, 1.0f);
        }
        else if (isOn == true)
        {
            m_door.transform.position = Vector3.SmoothDamp(m_door.transform.position, m_door.transform.position - doorOffset, ref velocity, 1.0f);
        }

        if (m_door.transform.position.y >= maxH)
        {
            m_door.transform.position = new Vector3(m_door.transform.position.x, maxH, m_door.transform.position.z);
        }
        if (m_door.transform.position.y <= minH)
        {
            m_door.transform.position = new Vector3(m_door.transform.position.x, minH, m_door.transform.position.z);
        }
    }
}
