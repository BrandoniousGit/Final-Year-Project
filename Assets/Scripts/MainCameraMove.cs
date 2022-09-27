using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraMove : MonoBehaviour
{

    public float xSens, ySens;
    private float x, y;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        x += Input.GetAxis("Mouse X") * xSens;
        y -= Input.GetAxis("Mouse Y") * ySens;

        y = Mathf.Clamp(y, -80.0f, 80.0f);

        transform.eulerAngles = new Vector3(y, x, 0);
    }

}
