using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlat : MonoBehaviour
{
    [SerializeField] GameObject Ground = null;
    [SerializeField] CameraCinematic cameraCinematic = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && Ground != null)
        {
            Ground.SetActive(true);
            cameraCinematic.Play();
        }

    }
}
