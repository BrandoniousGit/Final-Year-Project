using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerMove : MonoBehaviour
{
    private Camera cam;
    private Rigidbody rb;
    public float moveSpeed, jumpForce, slowdownMulti, sideStepForce, sideStepCooldown, airMulti, slamSpeed, stepCounter;
    private float extraJumpHeight = 0;
    public bool grounded, crouching, sideStepped, hasControl, isAlive;
    public float currentHealth, maxHealth;

    private void Start()
    {
        cam = Camera.main;
        stepCounter = 1;
        rb = GetComponent<Rigidbody>();
        hasControl = true;
        currentHealth = 100;
        maxHealth = currentHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Checks for colliding with the "EnemyChecker" to see if a fight needs to be executed
        if (other.gameObject.tag == "EnemyChecker")
        {
            EnemyCheckerScript _enemyCheckerScript = other.gameObject.GetComponent<EnemyCheckerScript>();

            if (_enemyCheckerScript.fightFinished)
            {
                return;
            }

            _enemyCheckerScript.playerEnteredRoom = true;
        }
    }

    public bool ReturnIsAlive()
    {
        return isAlive;
    }

    void UserInput()
    {
        //Allows for player movement relative to where the camera is looking
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
                    rb.velocity = new Vector3(rb.velocity.x * slowdownMulti, rb.velocity.y, rb.velocity.z * slowdownMulti);
                }
            }

            else
            {
                rb.velocity = new Vector3(_moveDir.x * moveSpeed, rb.velocity.y, _moveDir.z * moveSpeed);
            }
        }

        else
        {
            rb.velocity = new Vector3(rb.velocity.x * slowdownMulti, rb.velocity.y, rb.velocity.z * slowdownMulti);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !sideStepped && !crouching && !grounded)
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

        //Checks for if the player is on the ground so they can jump etc.
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
            slowdownMulti = Mathf.Pow(0.97f, Time.deltaTime / 0.0069f);
        }

        else if (grounded)
        {
            slowdownMulti = Mathf.Pow(Mathf.Lerp(slowdownMulti, 0.82f, 0.99f), Time.deltaTime / 0.0069f);
        }

        //Allows for the player to jump (v:jumpForce, v:grounded)
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            grounded = false;
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(0, jumpForce + extraJumpHeight, 0);
        }
    }

    void Sidestep(Vector3 direction)
    {
        //A quick movement (v:sideStepForce) in the direction the player is moving with a short cooldown (v:sideStepCooldown)
        Vector3 sideStepForceVec = new Vector3(sideStepForce * direction.x, 0, sideStepForce * direction.z);
        sideStepped = true;
        StartCoroutine("SidestepCooldown", sideStepCooldown);
        rb.AddForce(sideStepForceVec);
    }

    void Crouching()
    {
        if (!grounded)
        {
            //Uncrouches the player while they are airborne (v:crouching)
            if (crouching)
            {
                crouching = false;
                transform.localScale = new Vector3(transform.localScale.x, 1.2f, transform.localScale.z);
                transform.localPosition += new Vector3(0, 0.3f, 0);
            }

            //Allows for the user to slam if airborne
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                rb.velocity = new Vector3(0, slamSpeed, 0);
            }

            return;
        }

        //Crouched
        if (Input.GetKey(KeyCode.LeftControl) && !crouching)
        {
            crouching = true;
            transform.localScale = new Vector3(transform.localScale.x, 0.9f, transform.localScale.z);
            transform.localPosition -= new Vector3(0, 0.3f, 0);
        }

        //Uncrouched
        if (Input.GetKeyUp(KeyCode.LeftControl) && crouching)
        {
            crouching = false;
            transform.localScale = new Vector3(transform.localScale.x, 1.2f, transform.localScale.z);
            transform.localPosition += new Vector3(0, 0.3f, 0);
        }
    }

    public void TakeDamage(float incomingDamage)
    {
        currentHealth -= incomingDamage;
    }

    public float ReturnMaxHealth()
    {
        return maxHealth;
    }

    public float ReturnHealth()
    {
        return currentHealth;
    }

    private void Awake()
    {
        isAlive = true;
    }

    void Update()
    {
        if (!hasControl)
        {
            return;
        }

        if (sideStepped && stepCounter <= 1)
        {
            stepCounter += (1 / sideStepCooldown) * Time.deltaTime;
        }

        if (currentHealth <= 0)
        {
            isAlive = false;
            hasControl = false;
        }

        Jumping();
        Crouching();
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(Time.deltaTime);
        }
    }

    public float ReturnStepCounter()
    {
        return stepCounter;
    }

    void LateUpdate()
    {
        if (!hasControl)
        {
            return;
        }
        UserInput();
    }

    IEnumerator SidestepCooldown(float m_sidestepCooldown)
    {
        stepCounter = 0;
        yield return new WaitForSeconds(m_sidestepCooldown);
        sideStepped = false;
    }
}