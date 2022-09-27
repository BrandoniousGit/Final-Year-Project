using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class Interaction : MonoBehaviour
{
    Ray ray;

    public TextMeshProUGUI interactionText;
    Interactable interactableObject;
    //public float intDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) == true)
        {
            if (hit.transform.GetComponent<Interactable>() != null)
            {
                interactableObject = hit.transform.GetComponent<Interactable>();
                interactionText.text = interactableObject.GetDescription();
                if(Input.GetKeyDown(KeyCode.E))
                {
                    interactableObject.Interact();
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
}
