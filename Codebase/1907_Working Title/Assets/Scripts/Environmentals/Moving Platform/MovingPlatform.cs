using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected GameObject destination;
    protected Vector3 startPosition;
    protected Vector3 targetDestination;
    protected float rangeToDestination;
    protected bool swapDestination;
    [SerializeField] public float timer = 0;
    float tempTimer = 0;
    public bool move = true;
    bool tempMove;
    float tempSpeed = 0;
    GameObject player = null;
    [SerializeField] bool restartOnDeath = false;


    // Start is called before the first frame update
    void Start()
    {
        tempSpeed = speed;
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayJumpSparkle();
        tempMove = move;
        tempTimer = timer;
        startPosition = this.transform.position;
        targetDestination = destination.transform.position;
        rangeToDestination = 0.01f;
        float moveSpeed = speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(this.transform.position, targetDestination, moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().Died == true && restartOnDeath == true)
        {
            this.transform.position = startPosition;
            move = false;

        }
            if (Vector3.Distance(this.transform.position, targetDestination) < rangeToDestination)
        {
                move = false;
            
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                if (timer <= 0)
                {
                    move = true;
                    timer = tempTimer;
                    swapDestination = !swapDestination;
                    if (swapDestination)
                    {
                    move = tempMove;
                        targetDestination = destination.transform.position;
                   
                    }
                    else
                    {
                        targetDestination = startPosition;
                    }
                }
            
        }
        if (move)
        {
            float moveSpeed = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, targetDestination, moveSpeed);
        }

    }
}
