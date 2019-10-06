using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class EnemyShooter : EnemyBase
{
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] GameObject animatorHolder = null;

    private bool canFire;

    // Fix so that enemy does not go to patrol while attacking.
    private float backToPatrolTimer = 0.5f;
    private float backToPatrolMaxTime = 0.5f;

    private State currentState;

    private enum State
    {
        Patrolling,
        Attack
    }

    void Start()
    {
        currentState = State.Patrolling;
        bulletPrefab.GetComponent<Bullet>().Damage = damage;

        // Movement
        isPatrolling = true;
        rangeToDestination = 0.01f;
        swapDestination = true;

        // Vision
        hasVision = false;

        // Combat
        canFire = true;
        knockDistace = 3;
        currentHealth = maxHealth;
        auraTimer = 0.5f;

        // Finde objects on the scene.
        player = GameObject.FindGameObjectWithTag("Player");

        // Set patrolling positions
        startPosition = this.transform.position;
        targetDestination = destination.transform.position;

        // Spawn
        originalPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        // Start game inactive to make it easier on the machine and spawn when player hits the trigger.
        if (!startActive)
            this.gameObject.SetActive(false);
        if (animatorHolder != null)
            anim = animatorHolder.GetComponent<Animator>();

    }

    void Update()
    {
        if (auraTimer >= 0)
            auraTimer -= Time.deltaTime;

        switch (currentState)
        {
            case State.Patrolling:
                {
                    Patrol();

                    FindPlayer();
                    if (hasVision)
                    {
                        currentState = State.Attack;
                        isPatrolling = false;
                    }
                    break;
                }
            case State.Attack:
                {
                    FindPlayer();
                    Fire();
                    if (!hasVision)
                    {
                        backToPatrolTimer -= Time.deltaTime;
                        if (backToPatrolTimer <= 0)
                        {
                            currentState = State.Patrolling;
                            isPatrolling = true;
                        }
                       
                    }
                    break;
                }
            default:
                break;
        }
        Death();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (auraTimer <= 0 && other.gameObject == player)
        {
            player.GetComponent<PlayerController>().TakeDamage(knockDistace, this.transform.position, 1);
            StartAuraTimer();
        }
    }
   
    private void Fire()
    {
        if (firePoint != null)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
                canFire = true;

            if (canFire)
            {
                canFire = false;
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.EnemyShooting();
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                StartTimer();
                backToPatrolTimer = backToPatrolMaxTime;
            }
        }
    }
}
