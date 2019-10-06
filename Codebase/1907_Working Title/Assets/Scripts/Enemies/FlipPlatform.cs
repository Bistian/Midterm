using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class FlipPlatform : MonoBehaviour
{
    [SerializeField] string tagToCheck = "";

    // Timer
    [SerializeField] float flipBackTime = 3;
    Vector3 originalTransform;
    Vector3 displacedTransform;
    float flipTimer = 0;

    private void Start()
    {
        originalTransform = transform.position;
        displacedTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z + 50f);
    }

    private void Update()
    {
        if (flipTimer > -1)
            flipTimer -= Time.deltaTime;

        if (flipTimer <= 0)
            Unflip();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagToCheck)
        {
            Flip();
            StartTimer();
        }
    }

    void Flip()
    {
        this.transform.position = displacedTransform;
    }
    
    void Unflip()
    {
        this.transform.position = originalTransform;
    }

    void StartTimer()
    {
        flipTimer = flipBackTime;
    }
}
