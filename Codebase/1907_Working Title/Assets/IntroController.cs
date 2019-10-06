using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public float time = 15;
    void play()
    {
        playableDirector.Play();
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Intro"))
        {
            play();
        }
    }

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            SceneManager.LoadScene("Level 1");
        }
    }
}
