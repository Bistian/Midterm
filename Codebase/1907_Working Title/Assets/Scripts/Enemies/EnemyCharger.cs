using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class EnemyCharger : EnemyBase
{
    [SerializeField] float chargerDamage = 2f;
    [SerializeField] float dashSpeed = 15f;
    bool isCharging = false;
    bool isDragging = false;
    bool hit = false;
    bool destinationLeft = true;

    Vector3 playerPos;
    Vector3 enemyPos;

    private State currentState;

    private enum State
    {
        Patrolling,
        Charging
    }

    void Start()
    {
        currentState = State.Patrolling;

        // Movement
        rangeToDestination = 0.1f;
        swapDestination = true;
        isPatrolling = true;

        // Vision
        hasVision = false;

        // Combat
        canTakeDamage = false;
        knockDistace = 10;
        currentHealth = maxHealth;

        // Finde objects on the scene.
        player = GameObject.FindGameObjectWithTag("Player");

        // Set patrolling positions
        startPosition = this.transform.position;
        targetDestination = destination.transform.position;

        if (startPosition.x > targetDestination.x) { destinationLeft = true; }
        else { destinationLeft = false; }

        // Spawn
        originalPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        // Start game inactive to make it easier on the machine and spawn when player hits the trigger.
        if (!startActive)
            this.gameObject.SetActive(false);

        anim = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (auraTimer >= 0)
            auraTimer -= Time.deltaTime;

        playerPos = player.transform.position;
        enemyPos = transform.position;


        if (reloadTimer >= 0)
        {
            reloadTimer -= Time.deltaTime;
        }

        switch (currentState)
        {
            case State.Patrolling:
                {
                    Patrol();
                    FindPlayer();
                    if (hasVision)
                        Charge();

                    break;
                }
            case State.Charging:
                {
                    Move();
                    DragPlayer();
                    StopCharging();
                    break;
                }
            default:
                break;
        }
        Death();
    }

    private void Charge()
    {
        if (reloadTimer <= 0)
        {
            if ((!destinationLeft && (playerPos.x > startPosition.x && playerPos.x < destination.transform.position.x)) ||
                        (destinationLeft && (playerPos.x < startPosition.x && playerPos.x > destination.transform.position.x)))
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.ChargerRunning();
                currentState = State.Charging;
                isPatrolling = false;
                isCharging = true;
                hasVision = false;
            }
        }
    }

    private void Move()
    {
        float moveSpeed = dashSpeed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetDestination, moveSpeed);
    }

    private void DragPlayer()
    {
        if (hit)
        {
            // Disable player movement.
            player.GetComponent<PlayerController>().canMove = false;
            hit = false;
            isDragging = true;
        }
    }

    private void ShovePlayer()
    {
        // Apply force to player.
        player.GetComponent<PlayerController>().TakeDamage(chargerDamage, this.transform.position, knockDistace);
        player.GetComponent<PlayerController>().canMove = true;
        isDragging = false;
        isCharging = false;
        isPatrolling = true;

        StartTimer();
    }

    private void StopCharging()
    {
        float enemyXstart;
        float enemyXdestination;
        if (destinationLeft)
        {
            enemyXstart = startPosition.magnitude - enemyPos.magnitude;
            enemyXdestination = enemyPos.magnitude - destination.transform.position.magnitude;
        }
        else
        {
            enemyXstart = enemyPos.magnitude - startPosition.magnitude;
            enemyXdestination = destination.transform.position.magnitude - enemyPos.magnitude;
        }

        if (enemyXstart < 0.1 || enemyXdestination < 0.1)
        {
            isPatrolling = true;
            isCharging = false;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.ChargerStop();

            if (isDragging)
                ShovePlayer();

            currentState = State.Patrolling;
        }
        StartTimer();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isCharging)
        {
            isDragging = false;
            hit = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (auraTimer <= 0 && other.gameObject == player)
        {
            player.GetComponent<PlayerController>().TakeDamage(.25f, this.transform.position, knockDistace);
            StartAuraTimer();
        }        
    }

}
