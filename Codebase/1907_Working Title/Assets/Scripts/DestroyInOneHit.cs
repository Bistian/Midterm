using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyInOneHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
