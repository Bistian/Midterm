using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int currentBuildIndex = 0;
    public bool EndScene = false;
    public bool ReloadThisScene = false;
    public bool TimedEndScene = false;
    private float endTime = 6f;

    // Update is called once per frame
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        currentBuildIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode amode)
    {
        Time.timeScale = 1f;
    }

    void Update()
    {

        if (EndScene)
        {
            GameObject.FindGameObjectWithTag("Main HUD").GetComponent<HudScript>().Fade();
            LoadMain();
        }

        if (TimedEndScene)
        {
            endTime -= Time.deltaTime;
            if (endTime <= 0)
            {
                GameObject.FindGameObjectWithTag("Main HUD").GetComponent<HudScript>().Fade();
                NextScene();
            }
        }

        if (ReloadThisScene)
        {
            GameObject.FindGameObjectWithTag("Main HUD").GetComponent<HudScript>().Fade();
            ReloadScene();
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene(currentBuildIndex + 1);
    }

    private void LoadMain()
    {
        SceneManager.LoadScene(0);
    }

    private void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
