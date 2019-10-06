using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovePlat : MonoBehaviour
{
    [SerializeField] List<GameObject> StuffToRemove = new List<GameObject>();
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            for (int i = 0; i < StuffToRemove.Count; i++)
            {
                Destroy(StuffToRemove[i]);
            }
        }
    }
}
