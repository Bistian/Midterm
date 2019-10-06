using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBreak : MonoBehaviour
{
    [SerializeField] GameObject Kill = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(Kill);
        }

    }
}
