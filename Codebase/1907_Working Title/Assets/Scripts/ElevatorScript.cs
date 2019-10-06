using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] GameObject elevatorLight = null;
    // Start is called before the first frame update
    void Start()
    {
        elevatorLight.SetActive(false);
    }

    public void ChangeLight()
    {
        elevatorLight.SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && gameObject.tag == "End Level")
        {
            gameObject.GetComponent<TimelineController>().Play();
        }
    }
}
