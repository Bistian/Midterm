using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnableText : MonoBehaviour
{
    [SerializeField] GameObject UI = null;
    private int currentIndex;
    private bool timer = false;
    [SerializeField] float amountOfTime = 4f;
    private void Start()
    {
        currentIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UI.SetActive(true);
            timer = true;
        }
    }

    private void Update()
    {
        if (timer)
        {
            amountOfTime -= Time.deltaTime;
            if (amountOfTime <= 0)
            {
                if (currentIndex != 5)
                {
                    GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(currentIndex + 1);
                }
                else
                {
                    GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(0);
                }

                timer = false;
            }
        }
        
    }
}
