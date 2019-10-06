using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenController : MonoBehaviour
{
    [SerializeField] GameObject Main = null;
    public GameObject loadingScreenObject = null;
    public Slider slider = null;

    private void Start()
    {
        if (Main == null)
        {
            Main = GameObject.FindGameObjectWithTag("MenuOption");
        }
    }
    public void LoadScreen(int levelNumber)
    {
        StartCoroutine(LoadingScreen(levelNumber));
    }

    IEnumerator LoadingScreen(int level)
    {
        Main?.SetActive(false);
        loadingScreenObject.SetActive(true);
        AsyncOperation async = SceneManager.LoadSceneAsync(level);
        async.allowSceneActivation = false;

        if (async.isDone)
        {
            slider.value = 1f;
        }
        while (!async.isDone)
        {
            slider.value = async.progress;
            if (async.progress == 0.75f)
            {
                GameObject.FindGameObjectWithTag("Main HUD").GetComponent<HudScript>().Fade();
            }
            if (async.progress == 0.9f)
            {
                slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
