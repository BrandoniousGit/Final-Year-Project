using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCameraMove : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public Interactable iOScript;
    public float xSens, ySens, interactDistance;
    private float x, y;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void UserInput()
    {
        //Rotational Movement
        x += Input.GetAxis("Mouse X") * xSens;
        y -= Input.GetAxis("Mouse Y") * ySens;

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        transform.eulerAngles = new Vector3(y, x, 0);

        if (Input.GetKey(KeyCode.A)) 
        {
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z + 0.7f);
        }
        if (Input.GetKey(KeyCode.D)) 
        { 
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, transform.localEulerAngles.z - 0.7f);
        }
    }

    void UserInteract()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));

        //Tests for interactable object infront of camera
        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            if (hit.transform.GetComponent<Interactable>() != null)
            {
                iOScript = hit.transform.GetComponent<Interactable>();
                HandleInteraction(iOScript);
                interactionText.text = iOScript.GetDescription();
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

    void HandleInteraction(Interactable interactable)
    {
        KeyCode interactKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), interactable.interactKey);
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Press:
                if (Input.GetKeyDown(interactKeyCode))
                {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKeyDown(interactKeyCode))
                {
                    Debug.Log("Yeah");
                }
                break;
            case Interactable.InteractionType.Switch:
                if (Input.GetKeyDown(interactKeyCode))
                {
                    interactable.Interact();
                }
                break;
            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }

    void Update()
    {
        if (interactionText == null)
        {
            if (GameObject.FindGameObjectWithTag("InteractionText").GetComponent<TextMeshProUGUI>() == null)
            {
                return;
            }
            interactionText = GameObject.FindGameObjectWithTag("InteractionText").GetComponent<TextMeshProUGUI>();
        }

        UserInput();
        UserInteract();
    }
}
