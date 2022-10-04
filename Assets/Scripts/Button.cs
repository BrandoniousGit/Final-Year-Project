using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    Rigidbody rb;
    private Vector3 velocity = Vector3.zero;
    private MainCameraMove mainCamScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamScript = Camera.main.GetComponent<MainCameraMove>();
    }
    public override string GetDescription()
    {
        string description;
        if (isSelected == false)
        {
            description = "Press 'RMB' to pick up!";
            return description;
        }
        else if (isSelected == true)
        {
            description = "Press 'LMB' to throw!";
            return description;
        }
        else
        {
            description = "";
            return description;
        }
    }

    public override void Interact()
    {
        isSelected = true;
    }

    private void Update()
    {
        if (isSelected == true)
        {
            rb.transform.position = mainCam.transform.position + mainCam.transform.TransformDirection(Vector3.forward) * mainCamScript.interactDistance;
            //rb.transform.position = Vector3.SmoothDamp(rb.transform.position, mainCam.transform.position + mainCam.transform.TransformDirection(Vector3.forward) * mainCamScript.interactDistance, ref velocity, 0.01f);
            rb.transform.rotation = new Quaternion(0, mainCam.transform.rotation.y, 0, 0);
            rb.velocity = Vector3.zero;
            if (Input.GetMouseButtonUp(1))
            {
                isSelected = false;
            }
            else if (Input.GetMouseButtonDown(0))
            {
                rb.velocity = mainCam.transform.TransformDirection(Vector3.forward) * mainCamScript.throwPower;
                isSelected = false;
            }
        }
    }
}
