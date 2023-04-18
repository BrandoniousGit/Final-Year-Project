using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerCam : MonoBehaviour
{
    private PlayerMove playerScript;
    public Image sidestepBar, healthBar;

    void Update()
    {
        //Finds the player and it's script to get access to variables
        if (playerScript == null)
        {
            if (GameObject.FindGameObjectWithTag("Player") == null)
            {
                return;
            }
            playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        }

        sidestepBar.fillAmount = playerScript.returnStepCounter();

        //Finds the player's camera and to get access to it
        if (gameObject.GetComponent<Canvas>().worldCamera == null)
        {
            if (GameObject.FindGameObjectWithTag("MainCamera") == null)
            {
                return;
            }
            gameObject.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }
    }
}
