using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CameraCinematic : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    [SerializeField] GameObject secondCamera;
    public PlayableDirector playableDirector;
    public bool hasPlayed = false;
    public float time = 3;

    public void Play()
    {
        if (hasPlayed == false)
        {
            mainCamera.SetActive(false);
            secondCamera.SetActive(true);
            playableDirector.Play();
            hasPlayed = true;
        }
        else
        {

        }
    }
    private void Update()
    {
        if (time > 6.3)
        {
            time -= Time.deltaTime;
        }
        else
        {
            secondCamera.SetActive(false);
            mainCamera.SetActive(true);
        }
    }
}
