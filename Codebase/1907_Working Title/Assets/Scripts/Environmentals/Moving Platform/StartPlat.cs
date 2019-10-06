using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPlat : MonoBehaviour
{
    [SerializeField] protected GameObject Plat;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Plat != null)
        {
            Plat.GetComponent<MovingPlatform>().move = true;
        }
    }

}
