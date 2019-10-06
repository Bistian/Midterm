using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class EnemyBoss : MonoBehaviour
{
    // -------------------------- Variables -------------------------- 
    #region Variables
    // Scene Objects
    [HideInInspector] GameObject player = null;
    [HideInInspector] GameObject cameraPrefab = null;
    [SerializeField] GameObject waypointLeft = null;
    [SerializeField] GameObject waypointRight = null;
    [SerializeField] GameObject waypointMiddle = null;
    [SerializeField] GameObject lava = null;
    [SerializeField] GameObject healthBar = null;
    [SerializeField] List<GameObject> lights = null;
    [SerializeField] List<GameObject> hearts = null;

    // Checks
    bool hitPlayer = false;
    bool heartTimerOn = false;

    // Camera Effect
    Quaternion cameraRotation;
    private float shakeMaxTime = 0;
    private float shakeTimer = 0;
    private bool isShaking = false;

    // Movement
    protected Vector3 PositionLeft;
    protected Vector3 PositionRight;
    protected Vector3 PositionMiddle;
    protected Vector3 targetDestination;
    public int runSpeed = 10;
    public int walkSpeed = 4;
    private bool moveLeft = true;

    // Combat
    public int chargeDamage = 1;
    public int auraDamage = 1;
    public int health = 50;

    // Timing
    private float maxIdleTime = 3;
    private float idleTimer = 0;
    public float maxHeartRespawn = 10f;
    private float heartRespawn = 0f;

    Animator anim;
    [SerializeField] GameObject Model = null;

    [HideInInspector] public State currentState;
    #endregion
    // -------------------------- Unity Functions -------------------------- 
    #region Unity Functions

    public enum State
    {
        Idle,
        Charging,
        RaisingLava,
        MoveToMiddle,
        MoveToDestination
    }

    private void Start()
    {
        anim = Model.GetComponent<Animator>();
        currentState = State.Idle;
        player = GameObject.FindGameObjectWithTag("Player");

        // Movement
        PositionLeft = waypointLeft.transform.position;
        PositionRight = waypointRight.transform.position;
        PositionMiddle = waypointMiddle.transform.position;
        targetDestination = PositionLeft;
        
        // Camera
        cameraPrefab = GameObject.FindGameObjectWithTag("MainCamera");
        cameraRotation = cameraPrefab.transform.rotation;
        shakeMaxTime = lava.GetComponent<MovingPlatform>().timer;

        StartTimer(ref idleTimer, ref maxIdleTime);
    }

    private void Update()
    {
#if DEBUG
        TestingButtons();
#endif
        if (heartTimerOn)
            healthBar.GetComponent<Image>().color = Color.red;
        else
            healthBar.GetComponent<Image>().color = Color.yellow;

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;

            case State.Charging:
                Charge();
                break;

            case State.RaisingLava:
                RaiseLava();
                break;

            case State.MoveToMiddle:
                MoveToMiddle();
                break;

            case State.MoveToDestination:
                MoveToDestination();
                break;

            default:
                break;
        }
        HeartRespawnTimer();
        Shield();
    }

    private void LateUpdate()
    {
        CameraShake();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<PlayerController>().TakeDamage(1, this.transform.position, 7);
        }
        if (hitPlayer == false && other.gameObject == player && currentState == State.Charging)
        {
            hitPlayer = true;
            player.GetComponent<PlayerController>().TakeDamage(chargeDamage, this.transform.position, 20);
            ChangeDestination();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerProjectile")
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().GlassChip();
            TakeDamage();
        }
    }

    #endregion
    // -------------------------- State Functions --------------------------
    #region State Functions

    void Idle()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            if (RandomNumber() % 3 == 0)
            {
                currentState = State.MoveToMiddle;
            }
            else
            {
                currentState = State.Charging;
            }
        }
    }

    void Charge()
    {
        // Charge until something was hit.
        if (hitPlayer)
        {
            // Move back to previous wall.
            Move(walkSpeed);
            if (this.transform.position == targetDestination)
            {
                hitPlayer = false;
                ChangeDestination();
                currentState = State.Idle;
                StartTimer(ref idleTimer, ref maxIdleTime);
                return;
            }
        }
        else if (this.transform.position == targetDestination)
        {
            ChangeDestination();
            currentState = State.Idle;
            StartTimer(ref idleTimer, ref maxIdleTime);
            return;
        }
        else
            Move(runSpeed);
    }

    void RaiseLava()
    {
        anim.SetTrigger("Pour");
        isShaking = true;
        lava.GetComponent<MovingPlatform>().move = true;
        currentState = State.MoveToDestination;
        StartTimer(ref shakeTimer, ref shakeMaxTime);
        StartTimer(ref idleTimer, ref maxIdleTime);
    }

    void MoveToMiddle()
    {
        targetDestination = PositionMiddle;
        Move(walkSpeed);
        if (transform.position == targetDestination)
        {
            currentState = State.RaisingLava;
            if (moveLeft)
                targetDestination = PositionLeft;
            else
                targetDestination = PositionRight;
        }
    }

    void MoveToDestination()
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0)
        {
            Move(walkSpeed);

            if (transform.position == targetDestination)
            {
                StartTimer(ref idleTimer, ref maxIdleTime);
                currentState = State.Idle;
            }
        }
    }

    #endregion
    // -------------------------- Help Functions --------------------------
    #region Help Functions

    void Move(int speed)
    {
        float moveSpeed = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetDestination, moveSpeed);
    }

    void ChangeDestination()
    {
        moveLeft = !moveLeft;
        if (moveLeft)
        {
            targetDestination = PositionLeft;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetDestination = PositionRight;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void StartTimer(ref float timer, ref float maxTime)
    {
        timer = maxTime;
    }

    int RandomNumber()
    {
        return Random.Range(0, 100);
    }

    void TestingButtons()
    {
        if (Input.GetKeyDown(KeyCode.T))
            currentState = State.Charging;
        if (Input.GetKeyDown(KeyCode.Y))
            currentState = State.MoveToMiddle;
        if (Input.GetKeyDown(KeyCode.U))
            health = 0;
    }

    void TakeDamage()
    {
        if (heartTimerOn)
        {
            health -= 1;
            if (health <= 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().CoffeeBreak();
                Destroy(this.gameObject);
            }
        }
    }

    void Shield()
    {
        if (heartTimerOn == false)
        {
            for (int i = 0; i < hearts.Count; i++)
            {
                if (hearts[i].GetComponent<MeshRenderer>().enabled == true)
                {
                    heartTimerOn = false;
                    return;
                }
                else
                {
                    heartTimerOn = true;
                }
            }
            if (heartTimerOn)
                heartRespawn = maxHeartRespawn;
        }
    }

    void HeartRespawnTimer()
    {
        if (heartTimerOn)
        {
            if (heartRespawn > 0)
            {
                heartRespawn -= Time.deltaTime;
            }
            if (heartRespawn <= 0)
            {
                for (int i = 0; i < hearts.Count; i++)
                {
                    hearts[i].GetComponent<MeshRenderer>().enabled = true;
                    hearts[i].GetComponent<BoxCollider>().enabled = true;
                    hearts[i].GetComponent<BossHearts>().FillHealth();
                }
                heartTimerOn = false;
            }
        }
    }

    void CameraLerpOut()
    {
        //camera.transform.position = Vector3.Lerp(camera.transform.position, cameraZoomedOut, 0.5f * Time.deltaTime);
    }

    void CameraLerpIn()
    {
        //camera.transform.position = Vector3.Lerp(camera.transform.position, cameraOriginalPosition, 0.5f * Time.deltaTime);
    }

    void ChangeLights(Color color)
    {
        for (int i = 0; i < lights.Count; i++)
            lights[i].GetComponent<Light>().color = color;
    }

    void CameraShake()
    {
        if (isShaking)
        {
            cameraPrefab.transform.rotation = Quaternion.Euler(Random.Range(12, 12.2f), Random.Range(-0.15f, 0.15f), 0);
            shakeTimer -= Time.deltaTime;
        }
        if (isShaking && shakeTimer <= 0)
        {
            cameraPrefab.transform.rotation = cameraRotation;
            isShaking = false;
        }
    }

    #endregion
}
