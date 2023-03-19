using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    public float moveSpeed, jumpForce, slowdownMulti, sideStepForce, sideStepCooldown, walkSpeed, crouchSpeed;
    public bool grounded, crouching, sideStepped;

    private void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void UserInput()
    {
        float xMove = Input.GetAxisRaw("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(xMove, 0, zMove).normalized;

        if (direction.magnitude >= 0.1f)
        {
            Vector3 _moveDir = CalcUserUnput(direction);
            rb.velocity = new Vector3(_moveDir.x * moveSpeed, rb.velocity.y, _moveDir.z * moveSpeed);
        }

        else
        {
            rb.velocity = new Vector3(rb.velocity.x / slowdownMulti, rb.velocity.y, rb.velocity.z / slowdownMulti);
        }

        if (Input.GetKey(KeyCode.LeftShift) && grounded && !sideStepped && !crouching)
        {
            Vector3 _moveDir = CalcUserUnput(direction);
            Sidestep(_moveDir);
        }
    }

    Vector3 CalcUserUnput(Vector3 _direction)
    {
        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        Vector3 moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized;

        return moveDir;
    }

    void CheckGrounded()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.72f))
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    void Jumping()
    {

        if (!grounded)
        {
            slowdownMulti = 1.04f;
        }
        else
        {
            slowdownMulti = 1.18f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(0, jumpForce, 0);
        }
    }

    void Sidestep(Vector3 direction)
    {
        Vector3 sideStepForceVec = new Vector3(sideStepForce * direction.x, 0, sideStepForce * direction.z);

        if (Input.GetKey(KeyCode.A))
        {
            sideStepped = true;
            StartCoroutine("SidestepCooldown", sideStepCooldown);
            rb.AddForce(sideStepForceVec);
        }
        if (Input.GetKey(KeyCode.D))
        {
            sideStepped = true;
            StartCoroutine("SidestepCooldown", sideStepCooldown);
            rb.AddForce(sideStepForceVec);
        }
    }

    void Crouching()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            crouching = true;
            moveSpeed = crouchSpeed;
            transform.localScale = new Vector3(transform.localScale.x, 0.9f, transform.localScale.z);
            transform.localPosition -= new Vector3(0, 0.3f, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            crouching = false;
            moveSpeed = walkSpeed;
            transform.localScale = new Vector3(transform.localScale.x, 1.2f, transform.localScale.z);
            transform.localPosition += new Vector3(0, 0.3f, 0);
        }
    }

    void Update()
    {
        Jumping();
        Crouching();
        CheckGrounded();
    }

    void LateUpdate()
    {
        UserInput();
    }

    IEnumerator SidestepCooldown(float m_sidestepCooldown)
    {
        yield return new WaitForSeconds(m_sidestepCooldown);
        sideStepped = false;
    }
}
