using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pencil : MonoBehaviour
{
    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PencilHitWall();
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponents<BoxCollider>()[0].enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponents<BoxCollider>()[0].enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameObject.GetComponents<BoxCollider>()[0].enabled = true;
        }
    }
}
