using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActivateMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject optionsMenu = null;
    private GameObject player = null;
    [SerializeField] EventSystem Events = null;
    [SerializeField] GameObject restart = null;
    [SerializeField] Slider Master = null;
    [SerializeField] Slider Music = null;
    [SerializeField] Slider Sound = null;
    public static bool GameIsPaused = false;
    int currentSceneIndex;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        if  ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (GameIsPaused)
            {
                optionsMenu.GetComponent<OptionsScript>().Close();
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        if (currentSceneIndex == 2)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.UnpauseLevel1Music();
        }
        else if (currentSceneIndex == 3)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.UnpauseLevel2Music();
        }
        else if (currentSceneIndex == 4)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.UnpauseLevel3Music();
        }
        else if (currentSceneIndex == 5)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.UnpauseLevel4Music();
        }
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.StopPauseMusic();
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
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
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayPauseMusic();
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        if (Events != null)
        {
            Events.SetSelectedGameObject(restart);
        }
    }
    public void SaveAndQuitGame()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        PlayerPrefs.SetInt("SavedScene" + SaveSlots.SlotNumber, currentSceneIndex);
        Resume();
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(0);
    }
    public void Restart()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        Resume();
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(currentSceneIndex);
    }

    public void Options()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        optionsMenu.SetActive(true);
    }
}
