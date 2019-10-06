using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{
    [SerializeField] GameObject Player = null;
    [SerializeField] GameObject EndPlayer = null;
    [SerializeField] GameObject ElevatorDoor = null;
    public PlayableDirector playableDirector = null;

    public void Play()
    {
        if(Player != null)
            Player.SetActive(false);
        EndPlayer.SetActive(true);
        ElevatorDoor.SetActive(true);
        playableDirector.Play();
        StartCoroutine(EndScene());
    }

    IEnumerator EndScene()
    {
        yield return 3f;
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().TimedEndScene = true;
    }
}
