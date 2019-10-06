using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public abstract class EnemyBase: MonoBehaviour
{
    #region Variables

    public GameObject deathEffect = null;

    [HideInInspector] public bool isDead = false;

    // Combat stats.
    public float maxHealth = 0f;
    [HideInInspector] public float currentHealth = 0;
    public int damage;
    protected bool canTakeDamage = true;
    protected float knockDistace = 10;

    // Movement
    [SerializeField] protected float speed = 0f;
    [SerializeField] protected GameObject destination = null;
    protected Vector3 startPosition;
    protected Vector3 targetDestination;
    protected float rangeToDestination = 0f;
    protected bool swapDestination = false;
    protected bool isPatrolling;


    // Aiming
    protected bool hasVision;
    [SerializeField] protected GameObject alertIcon = null;
    [SerializeField] protected int range = 0;
    [SerializeField] protected float coneOfView;
    protected Quaternion targetDirection;

    // Timing
    [SerializeField] float maxReloadTime = 3;
    protected float reloadTimer = 0;
    protected float auraTimer = 0;
    protected float auraMaxTime = 1;

    //Animation
    [HideInInspector] public Animator anim = null;

    // Objects on the scene.
    protected GameObject player = null;

    // Spawn
    [HideInInspector] public Vector3 originalPosition;
    public bool startActive = false;

    #endregion

    // Keeps moving from left to right while the player is not detected.
    protected void Patrol()
    {
        if (isPatrolling)
        {
            // Move towards the target.
            float moveSpeed = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetDestination, moveSpeed);

            // Check if the destination was reached and change destination.
            if (Vector3.Distance(this.transform.position, targetDestination) < rangeToDestination)
                ChangeDestination();
        }
    }

    // Raycast to player's position and check if the ray hits the player or something else.
    protected void FindPlayer()
    {
        // ray is locating the player and raycastHit is telling the first thing that was hit by the ray.
        Ray ray = new Ray(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 1, player.transform.position.z) - transform.position);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.tag == "Player" && CheckRange() && CheckConeOfVision())
            {
                hasVision = true;
                if (alertIcon != null)
                    alertIcon.GetComponent<MeshRenderer>().enabled = true;
            }
            else
            {
                hasVision = false;
                if (alertIcon != null)
                    alertIcon.GetComponent<MeshRenderer>().enabled = false;
            }
        }

        Vector3 offset;
        offset.x = player.transform.position.x;
        offset.y = player.transform.position.y + 1;
        offset.z = player.transform.position.z;

        Debug.DrawLine(transform.position, offset);
    }

    void ChangeDestination()
    {
        swapDestination = !swapDestination;
        if (swapDestination)
        {
            targetDestination = destination.transform.position;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            targetDestination = startPosition;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        anim.SetTrigger("Turn");
    }

    // Check distance between player and enemy to see if player is at range.
    bool CheckRange()
    {
        Vector3 sum = player.transform.position - transform.position;
        float magnitude = sum.magnitude;
       
        if (magnitude < range)
            return true;
        
        return false;
    }

    // Check if player is inside of the field of vision.
    bool CheckConeOfVision()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(directionToPlayer, transform.right);

        if (angle < coneOfView)
        {
            return true;
        }
        return false;
    }

    // Enemy detects if a player bullet hits it.
    public void TakeDamage(float damage)
    {
        if (canTakeDamage)
            currentHealth -= damage;
    }

    // Enemy dies.
    protected void Death()
    {
        if (currentHealth < 1)
        {
            isDead = true;
        }
        
        if (isDead)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Explosion();
            Instantiate(deathEffect, transform.position, deathEffect.transform.rotation);
            ResetPosition();
            currentHealth = maxHealth;
            isDead = false;
            gameObject.SetActive(false);
        }
    }

    protected void StartTimer()
    {
        reloadTimer = maxReloadTime;
    }

    protected void StartAuraTimer()
    {
        auraTimer = auraMaxTime;
    }

    public void ResetPosition()
    {
        this.transform.position = originalPosition;
    }

   
}
