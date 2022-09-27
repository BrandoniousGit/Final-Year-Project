using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public override string GetDescription()
    {
        string description = "Press 'E' to launch!";

        return description;
    }

    public override void Interact()
    {
        rb.AddForce(0, 350, 0);
    }
}
