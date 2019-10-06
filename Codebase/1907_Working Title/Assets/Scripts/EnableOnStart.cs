using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnStart : MonoBehaviour
{
    public GameObject objectToEnable = null;
    // Start is called before the first frame update
    void OnEnable()
    {
        objectToEnable.SetActive(true); 
    }

}
