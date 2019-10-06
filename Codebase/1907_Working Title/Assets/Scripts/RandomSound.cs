using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{ 
    float timerNumber = 16f;
    float originalTime = 0f;
    int number;
    int recentlyUsed;
    // Start is called before the first frame update
    private void Start()
    {
        originalTime = timerNumber;
    }
    // Update is called once per frame
    void Update()
    {
        timerNumber -= Time.deltaTime;
        if (timerNumber <= 0)
        {
            number = Random.Range(1, 4);
            if (number == 1 && recentlyUsed != number)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlayPhoneRing();
                recentlyUsed = 1;
                timerNumber = originalTime;
            }
            else if (number == 2 && recentlyUsed != number)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlayFilingCabinet();
                recentlyUsed = 2;
                timerNumber = originalTime;
            }
            else if (number == 3 && recentlyUsed != number)
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().PlayPrinter();
                recentlyUsed = 3;
                timerNumber = originalTime;
            }
        }
    }
}
