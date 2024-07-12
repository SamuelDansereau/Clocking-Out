using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    //movement
    public float moveSpeed, horizontalInput, verticalInput, groundDrag;
    float sprintAddidtive = 7f;

    public Transform orientation;
    Vector3 moveDirection;
    Rigidbody rb;
    //ground
    public float playerHeight;
    public LayerMask ground;
    bool grounded;
    public int grav = 45;
    //air
    public float jumpForce, jumpCooldown;
    bool readyJump = true;
    KeyCode jumpKey = KeyCode.Space;
    //Health
    public float health = 100f;
    public float maxHealth = 100f;
    bool doDamage = true;
    public string deathScreen;
    //Stamina
    public float stamina = 100f;
    public float maxStamina = 100f;
    bool runCouroutine = true;
    bool usingStamina = false;
    //sprint
    float sprintStaminaLost = 0.1f;
    //dodge
    float dodgeSpace = 7500f;
    //crouch
    bool crouched = false;

    public GameObject playerSafe;
    public GameObject pit;
    public int fallDamage;

    AudioSource sounds;
    public AudioClip walk, run, crouch, dodge;
    bool moving = false;
    bool canPlay = false;

    private void Start()
    {
        sounds = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        maxHealth = 100;
        health = maxHealth;
        maxStamina = 100;
        stamina = maxStamina;
        StartCoroutine(RefreshStamina());
        StartCoroutine(DamageCooldown());
    }
    private void Update()
    {
        //grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, ground);
        PlayerInput();
        SpeedControl();
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        if (health <= 0)
        {
            SceneManager.LoadScene(deathScreen);
        }
    }
    private void FixedUpdate()
    {
        MovePlayer();
        rb.AddForce(Vector3.down * grav * rb.mass);
    }
    void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if(verticalInput != 0 || horizontalInput != 0)
        {
            moving = true;
        }
        if(verticalInput == 0 && horizontalInput == 0)
        {
            moving = false;
        }
        if(!moving)
        {
            sounds.Stop();
        }
    
        if (Input.GetKey(jumpKey) && readyJump && grounded)
        {
            readyJump = false;
            grounded = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl) && stamina >= 15 && !crouched)
        {
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            usingStamina = true;
            if (grounded)
            {
                sounds.PlayOneShot(dodge, 1);
                rb.AddForce(moveDirection.normalized * (10f + dodgeSpace), ForceMode.Force);
                stamina -= 5;
                Debug.Log("DODGE");
            }
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (!crouched)
            {
                crouched = true;
                transform.localScale = new Vector3(0.5f, 0.25f, 1f);
                moveSpeed /= 2;
            }
            else if (crouched)
            {
                crouched = false;
                transform.localScale = new Vector3(1f, 1f, 1f);
                moveSpeed *= 2;
            }

        }
        if(crouched)
        {
            sounds.clip = crouch;
        }
    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        
        
        if (Input.GetKey(KeyCode.LeftShift) && stamina >= 1 && !crouched)
        {
            sounds.clip = run;
            usingStamina = true;
            if (grounded)
            {
                rb.velocity += moveDirection.normalized * moveSpeed * (10f + sprintAddidtive);
                stamina -= sprintStaminaLost;
                if(!sounds.isPlaying && moving)
                {
                    sounds.Play();
                }
            }
            else if (!grounded)
            {
                rb.velocity += moveDirection.normalized * moveSpeed * (10f + sprintAddidtive);
                stamina -= sprintStaminaLost;
            }
        }
        else
        {
            if(!crouched)
                sounds.clip = walk;
            usingStamina = false;
            if (grounded)
                rb.velocity += moveDirection.normalized * moveSpeed * 10f;
                if(!sounds.isPlaying && moving)
                {
                    sounds.Play();
                }
            else if (!grounded)
                rb.velocity += moveDirection.normalized * moveSpeed * 10f;
        }
    }
    void SpeedControl()
    {
        Vector3 velocity = new Vector3(rb.velocity.x, rb.velocity.y, rb.velocity.z);
        if (velocity.magnitude > moveSpeed)
        {
            Vector3 limitVelocity = velocity.normalized * moveSpeed;
            rb.velocity = new Vector3(limitVelocity.x, rb.velocity.y, limitVelocity.z);
        }
    }
    void Jump()
    {
        //rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(rb.velocity + (transform.up * jumpForce), ForceMode.Impulse);
    }
    void ResetJump()
    {
        readyJump = true;
    }
    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == "enemy")
        {
            if (doDamage)
            {
                health -= col.gameObject.GetComponent<BasicEnemy>().outDamage;
                doDamage = false;
            }
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == 6)
            grounded = true;

        if (col.gameObject.tag == "enemyBullet")
        {
            health -= col.gameObject.GetComponent<enemyBullet>().damage;
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == pit)
        {
            transform.position = playerSafe.transform.position;
            health -= fallDamage;
        }
    }

    private IEnumerator RefreshStamina()
    {
        while (runCouroutine)
        {
            if (stamina < maxStamina && !usingStamina)
            {
                stamina++;
                //Debug.Log("Stamina plus 1");
            }
            //Debug.Log("Stamina Coroutine Run");
            yield return new WaitForSeconds(.5f);
        }
    }
    private IEnumerator DamageCooldown()
    {
        while (runCouroutine)
        {
            doDamage = true;
            yield return new WaitForSeconds(2.25f);
        }
    }

}