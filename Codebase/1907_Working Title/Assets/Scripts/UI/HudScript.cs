using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HudScript : MonoBehaviour
{
    GameObject player = null;
    public Slider healthBar;
    public GameObject LiveCounter;
    public Image fadeObject = null;
    public Animator fadeAnim = null;
    public GameObject gameOver = null;
    public int deathCounter = 0;
    GameObject[] lives = null;
    public bool inMainMenu = false;
    
    // Ammunition
    public Text ammunitionText;
    [HideInInspector] public int maxAmmunition = 0;
    [HideInInspector] public int currentAmmunition = 0;

    // Timer
    public Text timerText;
    private float gameTimer = 0;
    private string minutes;
    private string seconds;

    void Start()
    {
        lives = new GameObject[3];
        player = GameObject.FindGameObjectWithTag("Player");
        GameObject[] arrangeLives = GameObject.FindGameObjectsWithTag("Live");
        for (int i = 0; i < arrangeLives.Length; i++)
        {
            if (arrangeLives[i].name == "First Life")
                lives[2] = arrangeLives[i];
            else if (arrangeLives[i].name == "Second Life")
                lives[1] = arrangeLives[i];
            else
                lives[0] = arrangeLives[i];
        }

        if (healthBar != null)
        {
            healthBar.maxValue = player.GetComponent<PlayerController>().health;
            healthBar.minValue = 0f;
            //AmmunitionBar.minValue = 0;

        }
        currentAmmunition = maxAmmunition;
    }

    void Update()
    {
       
        GameTimer();

        if (healthBar != null)
            healthBar.value = player.GetComponent<PlayerController>().health;

        if (ammunitionText != null)
            AmmunitionCount();

        switch (deathCounter)
        {
            case 1:
                lives[2].SetActive(false);
                break;
            case 2:
                lives[1].SetActive(false);
                break;
            case 3:
                lives[0].SetActive(false);
                break;
            case 4:
                gameOver.SetActive(true);
                ammunitionText.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().canMove = false;
                Time.timeScale = 0f;
                break;
            default:
                break;
        }
    }

    public void Fade()
    {
        StartCoroutine("Fading");
    }

    IEnumerator Fading()
    {
        fadeAnim.SetBool("Fade", true);
        yield return new WaitUntil(() => fadeObject.color.a == 1);
        fadeAnim.SetBool("Fade", false);
    }

    private void GameTimer()
    {
        if (!inMainMenu)
        {
            gameTimer += Time.deltaTime;
            minutes = ((int)gameTimer / 60).ToString();
            seconds = (gameTimer % 60).ToString("f0");
            timerText.text = "Time: " + minutes + ":" + seconds;
        }
    }

    private void AmmunitionCount()
    {
        ammunitionText.text = maxAmmunition + "-" + currentAmmunition;
    }
}
