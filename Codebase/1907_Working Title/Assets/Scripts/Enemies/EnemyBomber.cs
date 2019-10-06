using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class EnemyBomber : EnemyBase
{
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private GameObject bulletPrefab = null;
    
    private bool canFire;
    public bool patrol = false;

    //animation
    Animator anim2;


    void Start()
    {
        // Vision
        hasVision = false;

        // Patrol
        if (patrol)
        {
            isPatrolling = patrol;
            startPosition = this.transform.position;
            targetDestination = destination.transform.position;
            rangeToDestination = 0.01f;
        }

        // Combat
        canFire = true;
        bulletPrefab.GetComponent<Bullet>().Damage = damage;
        bulletPrefab.GetComponent<Bullet>().trackPlayer = true;
        knockDistace = 3;
        currentHealth = maxHealth;

        // Finde objects on the scene.
        player = GameObject.FindGameObjectWithTag("Player");

        // Spawn
        originalPosition = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        anim = this.gameObject.GetComponent<Animator>();

        // Start game inactive to make it easier on the machine and spawn when player hits the trigger.
        if (!startActive)
            this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (auraTimer <= 0)
            auraTimer -= Time.deltaTime;
        if (isPatrolling)
            Patrol();
        FindPlayer();
        Death();
        Fire();
    }

    private void Fire()
    {
        reloadTimer -= Time.deltaTime;
        if (reloadTimer <= 0)
            canFire = true;

        if (canFire && hasVision)
        {
            canFire = false;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.EnemyShooting();
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            StartTimer();
            anim2 = GetComponent<Animator>();
            anim2.SetTrigger("Shoot");
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
