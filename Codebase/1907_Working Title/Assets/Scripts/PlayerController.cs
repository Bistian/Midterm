using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    public float health = 5f;
    float healthMax;
    [HideInInspector] public bool gotToElevator = false;

    [SerializeField] private float speed = 5.3f;
    [SerializeField] public float jumpForce = 1.8f;
    [SerializeField] private float gravity = 4.4f;
    [SerializeField] private int jumps = 1;
    public Vector3 respawnPoint;
    public GameObject bloodEffect = null;
    int timesJumped = 0;
    bool Fell = false;
    public bool Died = false;
    bool calledDeath = false;
    float timer = 3f;
    public bool canMove = true;
    public bool canDamage = true;
    private int timesDied = 0;
    public bool canShoot = true;
    private float fellTimer = 0.2f;

    Animator anim;

    GameObject gameManager;

    private Vector3 pushBack = Vector3.zero;
    public Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        healthMax = health;
        controller = GetComponent<CharacterController>();
        respawnPoint = transform.position;
        anim = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        canShoot = true;
    }

    //Getter and Setters
    public float GetSpeed()
    {
        return speed;
    }

    public void SetSpeed(float _speed)
    {
        speed = _speed;
    }

    public int GetJumps()
    {
        return jumps;
    }

    public void SetJumps(int _Jumps)
    {
        jumps = _Jumps;
    }

    public float GetGravity()
    {
        return gravity;
    }

    public void SetGravity(float _Gravity)
    {
        gravity = _Gravity;
    }

    // Update is called once per frame
    void Update()
    {
        //Z Axis Not on Zero
        controller.transform.position = new Vector3(transform.position.x, transform.position.y, 0);

        //On Ground
        if (controller.isGrounded && canMove)
        {
            if (Fell)
            {
                moveDirection.y = 0f;
            }
            timesJumped = 0;
            fellTimer = 0.2f;

            float Speed = controller.velocity.normalized.x;
            Speed = Mathf.Abs(Speed);
            anim.SetFloat("speed", Speed);
        }
        else
            anim.SetFloat("speed", 0);
        if (canMove)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), moveDirection.y, 0f);
        }

        if (!canMove)
        {
            moveDirection = new Vector3(0f, moveDirection.y, 0f);
            //moveDirection.x = 0f;
        }

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) && timesJumped < jumps)
        {
            moveDirection.y = 0f;
            moveDirection.y = jumpForce;


            anim.SetTrigger("jump");

            if (timesJumped == 1)
            {
                gameManager.GetComponent<AudioManager>()?.PlayJumpSparkle();
                anim.SetTrigger("DoubleJump");
            }
            timesJumped++;
        }


        //Rotation
        if (controller.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (controller.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }

        //In Air
        if (!controller.isGrounded && canMove)
        {
            //Fell off ledge without jumping
            if (timesJumped == 0)
            {
                Fell = true;
                if (fellTimer > 0)
                {
                    fellTimer -= Time.deltaTime;
                }
                if (fellTimer <= 0)
                {
                    timesJumped++;
                }
            }
            //Hit his head to stop movement
            if (controller.velocity.y == 0)
            {
                moveDirection.y = 0f;
            }
        }
        //Knockback
        pushBack.x = Mathf.Lerp(pushBack.x, 0f, 0.2f);
        if (pushBack.x != 0)
        {
            moveDirection.x += pushBack.x;
        }

        //Gravity
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);

        if (Died)
        {
            canShoot = false;
            if (!calledDeath)
            {
                timesDied = GameObject.FindGameObjectWithTag("Main HUD").GetComponent<HudScript>().deathCounter++;
                calledDeath = true;
            }
            canMove = false;
            jumpForce = 0;
            anim.SetBool("dead", true);
            if (timesDied != 4)
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    Respawn();
                    timer = 3.0f;
                    jumpForce = 2.1f;
                    Died = false;
                    calledDeath = false;
                    canMove = true;
                }
            }
        }
    }
    public void canShootFunc()
    {
        canShoot = true;
    }
    public void Respawn()
    {
        transform.position = respawnPoint;
        health = 5;
        anim.SetBool("dead", false);
        anim.SetTrigger("Wake");
        Invoke("ChangeDamagable", .5f);
        Invoke("canShootFunc", .5f);
    }

    public void ChangeDamagable()
    {
        canDamage = !canDamage;
    }

    private void ChangeColor(Color name)
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = name;
    }

    private void Revert()
    {
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.color = Color.white;
    }

    public void TakeDamage(float damage)
    {
        if (canDamage)
        {
            health -= damage;
            ChangeColor(Color.red);
            Vector3 bloodPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0);
            Instantiate(bloodEffect, bloodPosition, bloodEffect.transform.rotation);
            Invoke("Revert", 0.25f);

            anim.SetTrigger("damage");
            if (health > 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayPlayerDamage();
            }
            if (health <= 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().DeathSound();
                Died = true;
                ChangeDamagable();
            }
        }


    }

    public void TakeDamage(float damage, Vector3 position, float knockDistance)
    {
        if (canDamage)
        {
            health -= damage;
            ChangeColor(Color.red);
            Vector3 bloodPosition = new Vector3(transform.position.x, transform.position.y + 1f, 0);
            Instantiate(bloodEffect, bloodPosition, bloodEffect.transform.rotation);
            Invoke("Revert", 0.25f);

            if (health > 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayPlayerDamage();
            }
            if (health <= 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().DeathSound();
                Died = true;
                ChangeDamagable();
            }

            if (transform.position.x > position.x && Died != true)
            {
                pushBack.x = (transform.position.x - position.x) + knockDistance;
                //moveDirection.x = 0;
            }
            else if (transform.position.x < position.x && Died != true)
            {
                pushBack.x = (transform.position.x - position.x) - knockDistance;
                //moveDirection.x = 0;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Killbox")
        {
            TakeDamage(5);
        }

        if (other.gameObject.tag == "Elevator" || other.gameObject.tag == "Begin Level")
        {
            health = 5;
            gotToElevator = true;
            if (other.gameObject.tag != "Begin Level")
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Celebrate();
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayElevatorRing();
            other.gameObject.GetComponent<ElevatorScript>().ChangeLight();
            respawnPoint = new Vector3(other.transform.position.x, other.transform.position.y + 1f, 0f);
            anim.SetTrigger("CheckPoint");
        }

        if (other.gameObject.tag == "End Level")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayElevatorRing();
        }

        if (other.gameObject.tag == "DoubleJump")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Pickup();
            jumps++;
            Destroy(other.gameObject);
        }


        if (other.gameObject.tag == "Health")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Pickup();
            if (health != healthMax)
            {
                health++;
            }
            Destroy(other.gameObject);
        }
    }
}
