using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomAnim : MonoBehaviour
{
    private GameObject player = null;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void PlayerCantMove()
    {
        player.GetComponent<PlayerController>().canMove = false;
    }

    void PlayerCanMove()
    {
        player.GetComponent<PlayerController>().canMove = true;
    }


}
