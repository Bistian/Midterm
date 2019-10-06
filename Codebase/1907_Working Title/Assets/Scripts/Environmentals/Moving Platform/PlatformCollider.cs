using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    [SerializeField] protected GameObject Player;
    public bool PencilOnly = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !PencilOnly)
        {
            Player.transform.parent = this.transform;
        }
        if (other.gameObject.tag == "PlayerProjectile")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.transform.parent = null;
        }
    }

}
