using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbStairs : MonoBehaviour
{
    private GameObject player = null;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        player.GetComponent<CharacterController>().stepOffset = 1;
    }
    private void OnTriggerExit(Collider other)
    {
        player.GetComponent<CharacterController>().stepOffset = 0.3f;
    }
}
