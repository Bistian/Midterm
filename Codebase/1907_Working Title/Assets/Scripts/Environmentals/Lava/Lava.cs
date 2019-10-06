using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    private GameObject Player = null;
    private float timer = 0.5f;
    bool inLava = false;
    private int numOfJumps = 0;
    private float originalGravity = 0f;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        numOfJumps = Player.GetComponent<PlayerController>().GetJumps();
        originalGravity = Player.GetComponent<PlayerController>().GetGravity();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            inLava = true;
            numOfJumps = Player.GetComponent<PlayerController>().GetJumps();
            Player.GetComponent<PlayerController>().SetSpeed(Player.GetComponent<PlayerController>().GetSpeed() / 2);
            Player.GetComponent<PlayerController>().SetGravity(Player.GetComponent<PlayerController>().GetGravity() / 50);
            Player.GetComponent<PlayerController>().TakeDamage(1f);
            Player.GetComponent<PlayerController>().SetJumps(10000);
        }
        else
        {
            //Destroy(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.GetComponent<PlayerController>().SetSpeed(6.2f);
            Player.GetComponent<PlayerController>().SetGravity(originalGravity);
            Player.GetComponent<PlayerController>().SetJumps(numOfJumps);
            inLava = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //numOfJumps = Player.GetComponent<PlayerController>().GetJumps();
        if (inLava)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.GetComponent<PlayerController>().moveDirection.y = 0;
                Player.GetComponent<PlayerController>().moveDirection.y = Player.GetComponent<PlayerController>().jumpForce;
            }

            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if (timer <= 0)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().Sizzle();
                timer = 1.0f;
                if (Player.gameObject.tag == "Player")
                {
                    Player.GetComponent<PlayerController>().TakeDamage(.25f);
                }
            }
        }


    }

}
