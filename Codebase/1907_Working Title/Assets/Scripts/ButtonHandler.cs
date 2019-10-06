using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ButtonHandler : MonoBehaviour
{

    [SerializeField] GameObject Main = null;
    [SerializeField] GameObject OptionsMenu = null;
    [SerializeField] GameObject CreditsMenu = null;
    [SerializeField] GameObject SaveMenu = null;
    [SerializeField] GameObject LoadMenu = null;
    [SerializeField] GameObject NameMenu = null;
    [SerializeField] EventSystem Events = null;
    [SerializeField] GameObject newGame = null;
    [SerializeField] GameObject Master = null;
    [SerializeField] GameObject CreditsBack = null;
    [SerializeField] GameObject SaveOne = null;
    [SerializeField] GameObject LoadOne = null;
    [SerializeField] GameObject Confirm = null;
    [SerializeField] GameObject player= null;
    Animator anim;



    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
    }

    public void Animator()
    {
        anim.SetTrigger("Click");
    }


    public void Animator2()
    {
        anim.SetTrigger("Click2");
    }

    public void ClickToMenu()
    {
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        Animator();
        Main.SetActive(false);
        SaveMenu.SetActive(true);
        Events.SetSelectedGameObject(SaveOne);
    }

    public void Continue()
    {
        Animator();
        Main.SetActive(false);
        LoadMenu.SetActive(true);
        Events.SetSelectedGameObject(LoadOne);
    }

    public void Options()
    {
        anim.SetTrigger("Click5");
        OptionsMenu.SetActive(true);
        Main.SetActive(false);
        Events.SetSelectedGameObject(Master);
    }

    public void Credits()
    {
        anim.SetTrigger("Click7");
        CreditsMenu.SetActive(true);
        Main.SetActive(false);
        Events.SetSelectedGameObject(CreditsBack);
    }

    public void Back()
    {
        if (Confirm.activeSelf)
        {
            anim.SetTrigger("Click4");
        }

        if (OptionsMenu.activeSelf)
        {
            anim.SetTrigger("Click6");
        }

        if (CreditsMenu.activeSelf)
        {
            anim.SetTrigger("Click8");
        }
        NameMenu.SetActive(false);
        LoadMenu.SetActive(false);
        SaveMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Main.SetActive(true);
        Confirm.SetActive(false);
        Events.SetSelectedGameObject(newGame);
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void Exit1()
    {
        NameMenu.SetActive(false);
        LoadMenu.SetActive(false);
        SaveMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        Confirm.SetActive(true);
        Main.SetActive(false);
        anim.SetTrigger("Click3");


    }

    public void PlaySound()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
    }

    public void Level1()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(2);
        GameObject.FindGameObjectWithTag("SaveScreen").SetActive(false);
    }

    public void Level2()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(3);
        GameObject.FindGameObjectWithTag("SaveScreen").SetActive(false);
    }

    public void Level3()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(4);
        GameObject.FindGameObjectWithTag("SaveScreen").SetActive(false);
    }

    public void Level4()
    {
        GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(5);
        GameObject.FindGameObjectWithTag("SaveScreen").SetActive(false);
    }


}