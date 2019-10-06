using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoyoteTime : MonoBehaviour
{
    GameObject[] coyotes = null;
    // Start is called before the first frame update
    void Start()
    {
        coyotes = GameObject.FindGameObjectsWithTag("CoyoteTime");
        foreach (GameObject block in coyotes)
        {
            block.GetComponent<BoxCollider>().size = new Vector3(block.GetComponent<BoxCollider>().size.x * 1.2f, block.GetComponent<BoxCollider>().size.y, block.GetComponent<BoxCollider>().size.z);
        }
    }
}
