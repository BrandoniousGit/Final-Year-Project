using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainCameraMove : MonoBehaviour
{
    public TextMeshProUGUI interactionText, magazineSize;
    public Interactable iOScript;
    public float xSens, ySens, throwPower, interactDistance;
    private float x, y;

    public Transform primWeaponPos, secWeaponPos;
    private GunObject primWeapon, secWeapon;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (primWeaponPos.GetComponentInChildren<GunScript>() != null)
        {
            primWeapon = primWeaponPos.GetComponentInChildren<GunObject>();
        }
        else
        {
            Debug.LogWarning("The Primary weapon is null!");
        }

        if (secWeaponPos.GetComponentInChildren<GunScript>() != null)
        {
            secWeapon = secWeaponPos.GetComponentInChildren<GunObject>();
        }
        else
        {
            Debug.LogWarning("The Secondary weapon is null!");
        }
    }

    void UserInput()
    {
        //Rotational Movement
        x += Input.GetAxis("Mouse X") * xSens;
        y -= Input.GetAxis("Mouse Y") * ySens;

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        transform.eulerAngles = new Vector3(y, x, 0);

        //Adjusting Throw Power
        if (Input.mouseScrollDelta.y >= 0) { throwPower += 1.0f; }
        if (Input.mouseScrollDelta.y <= 0) { throwPower -= 1.0f; }

        //Capped throw power
        if (throwPower >= 25) { throwPower = 25; }
        if (throwPower <= 0) { throwPower = 0; }

        //Setting GUI Text
        magazineSize.text = ("{0}: {1} | {2}", primWeapon.gunName, primWeapon.ammoInClip.ToString(), primWeapon.ammoInReserve.ToString());

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
            else { interactionText.text = ""; }
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
        UserInput();
        UserInteract();
    }
}
