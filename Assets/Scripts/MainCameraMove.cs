using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class MainCameraMove : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    Interactable iOScript;
    public float xSens, ySens;
    private float x, y;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UserInput()
    {
        x += Input.GetAxis("Mouse X") * xSens;
        y -= Input.GetAxis("Mouse Y") * ySens;

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        transform.eulerAngles = new Vector3(y, x, 0);
    }

    void UserInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) == true)
        {
            if (hit.transform.GetComponent<Interactable>() != null)
            {
                iOScript = hit.transform.GetComponent<Interactable>();
                interactionText.text = iOScript.GetDescription();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    iOScript.Interact();
                }
            }
            else
            {
                interactionText.text = "";
            }
        }
        else
        {
            interactionText.text = "";
        }
    }

    void Update()
    {
        UserInput();
        UserInteract();
    }
}
