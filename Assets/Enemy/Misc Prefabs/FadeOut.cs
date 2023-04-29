using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    private Vector4 color;

    void Awake()
    {
        color = gameObject.GetComponent<Renderer>().material.color;
    }

    private void Update()
    {
        gameObject.GetComponent<Renderer>().material.color = new Vector4(color.x, color.y, color.z, gameObject.GetComponent<Renderer>().material.color.a - 1f * Time.deltaTime);
        if (gameObject.GetComponent<Renderer>().material.color.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}
