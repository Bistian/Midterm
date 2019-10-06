using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    int currentSceneIndex = 0;

    private void OnEnable()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 2)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PauseLevel1Music();
        }
        else if (currentSceneIndex == 3)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PauseLevel2Music();
        }
        else if (currentSceneIndex == 4)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PauseLevel3Music();
        }
        else if (currentSceneIndex == 5)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PauseLevel4Music();
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().GameOver();
    }
    public void Restart()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = true;
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(currentSceneIndex);
    }

    public void Quit()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = true;
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(0);
    }
}
