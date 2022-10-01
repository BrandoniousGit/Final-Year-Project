using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVGame : Interactable
{

    private void Start()
    {
        mainCam = Camera.main;
    }

    public override string GetDescription()
    {
        string description = "Press 'RMB' to interact.";

        return description;
    }

    public override void Interact()
    {
        mainCam.gameObject.SetActive(false);
        subCam.gameObject.SetActive(true);
        isSelected = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Escape) && isSelected == true)
        {
            mainCam.gameObject.SetActive(true);
            subCam.gameObject.SetActive(false);
            isSelected = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
