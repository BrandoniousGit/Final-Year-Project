using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool isSelected, isOn;
    public Camera mainCam, subCam;

    public enum InteractionType
    { 
        Press,
        Hold
    }

    public InteractionType interactionType;

    public abstract void Interact();
    public abstract string GetDescription();

    public float holdTime, maxHoldTime;
    public void resetHoldTime() { holdTime = 0.0f; }

}
