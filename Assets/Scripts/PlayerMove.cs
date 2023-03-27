using System.Collections;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    public float moveSpeed, jumpForce, slowdownMulti, sideStepForce, sideStepCooldown, airMulti, slamSpeed;
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
            Vector3 mag = new Vector3(rb.velocity.x, 0, rb.velocity.z);

            if (!grounded)
            {
                rb.AddForce(new Vector3(_moveDir.x * moveSpeed * airMulti * Time.deltaTime, 0, _moveDir.z * moveSpeed * airMulti * Time.deltaTime));

                if (mag.magnitude > moveSpeed)
                {
                    rb.velocity = new Vector3(rb.velocity.normalized.x * moveSpeed, rb.velocity.y, rb.velocity.normalized.z * moveSpeed);
                }
            }

            else
            {
                rb.velocity = new Vector3(_moveDir.x * moveSpeed, rb.velocity.y, _moveDir.z * moveSpeed);
            }
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

        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.71f))
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
            slowdownMulti = 1.01f;
        }
        else
        {
            slowdownMulti = 1.18f;
        }

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
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

        if (Input.GetKey(KeyCode.W))
        {
            sideStepped = true;
            StartCoroutine("SidestepCooldown", sideStepCooldown);
            rb.AddForce(sideStepForceVec);
        }

        if (Input.GetKey(KeyCode.S))
        {
            sideStepped = true;
            StartCoroutine("SidestepCooldown", sideStepCooldown);
            rb.AddForce(sideStepForceVec);
        }
    }

    void Crouching()
    {
        if (!grounded)
        {
            if (crouching)
            {
                crouching = false;
                transform.localScale = new Vector3(transform.localScale.x, 1.2f, transform.localScale.z);
                transform.localPosition += new Vector3(0, 0.3f, 0);
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                rb.velocity = new Vector3(0, slamSpeed, 0);
            }

            return;
        }

        if (Input.GetKey(KeyCode.LeftControl) && !crouching)
        {
            crouching = true;
            transform.localScale = new Vector3(transform.localScale.x, 0.9f, transform.localScale.z);
            transform.localPosition -= new Vector3(0, 0.3f, 0);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && crouching)
        {
            crouching = false;
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
