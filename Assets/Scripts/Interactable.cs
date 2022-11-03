using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //Selected and On for various referencing
    public bool isSelected, isOn;

    //Main and Sub camera 
    public Camera mainCam, subCam;

    //Door and Light gameobjects both optional for the Switch Interaction type
    public GameObject m_door, m_light, m_genericObject;

    //Different types of interaction
    public enum InteractionType
    { 
        Press,
        Hold,
        Switch
    }

    public InteractionType interactionType;

    public abstract void Interact();
    public abstract string GetDescription();

    public float holdTime, maxHoldTime;
    public void resetHoldTime() { holdTime = 0.0f; }

}
