using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
   
    GameObject target;
    public float yOffset;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 desiredPosition = new Vector3(target.transform.position.x, target.transform.position.y + yOffset, transform.position.z);
        transform.position = desiredPosition;
    }
}
