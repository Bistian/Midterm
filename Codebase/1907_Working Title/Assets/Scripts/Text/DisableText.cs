using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisableText : MonoBehaviour
{
    [SerializeField] GameObject Ui = null;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Ui.gameObject.SetActive(false);

        }
    }


}
