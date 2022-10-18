using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    public float moveSpeed, maxSpeed, jumpForce, slowdownMulti;
    private bool jumping;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void UserInput()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float yMove = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xMove, 0, yMove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
            Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;

            rb.AddForce(moveDir * moveSpeed * Time.deltaTime);

            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = new Vector3(maxSpeed * moveDir.x, rb.velocity.y, maxSpeed * moveDir.z);
            }
        }

        else
        {
            rb.velocity = new Vector3(rb.velocity.x / slowdownMulti, rb.velocity.y, rb.velocity.z / slowdownMulti);
        }
    }

    private void Update()
    {
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Space) && jumping == false)
        {
            rb.AddForce(0, jumpForce, 0);
            jumping = true;
            slowdownMulti = 1.04f;
        }

        if (Physics.SphereCast(transform.position, transform.localScale.x / 2, Vector3.down, out hit, (transform.localScale.y / 2) + 0.05f) && jumping == true)
        {
            jumping = false;
            slowdownMulti = 1.18f;
        }
    }

    void LateUpdate()
    {
        UserInput();
    }
}
