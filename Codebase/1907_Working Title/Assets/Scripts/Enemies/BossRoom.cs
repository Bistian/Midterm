using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossRoom : MonoBehaviour
{
    [SerializeField] GameObject victoryText = null;
    [SerializeField] GameObject boss = null;

    // Timing
    private float maxIdleTime = 3;
    private float idleTimer = 3;

    bool activateVictory = false;

    private void Update()
    {
        VictoryText();
    }

    void VictoryText()
    {
        if (boss == null && !activateVictory)
        {
            victoryText.SetActive(true);
            activateVictory = true;
            StartTimer();
        }
        if (activateVictory)
            idleTimer -= Time.deltaTime;
        if (idleTimer < 0)
        {
            GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(0);
        }
    }

    void StartTimer()
    {
        idleTimer = maxIdleTime;
    }

}
