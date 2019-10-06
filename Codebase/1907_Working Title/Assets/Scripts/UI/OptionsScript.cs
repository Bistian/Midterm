using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{

    private AudioManager audioManager = null;
    [SerializeField] private Slider Master = null;
    [SerializeField] private Slider Music = null;
    [SerializeField] private Slider Sound = null;
    [SerializeField] private Text MasterValue = null;
    [SerializeField] private Text MusicValue = null;
    [SerializeField] private Text SoundValue = null;

    private void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>();
        Master.value = PlayerPrefs.GetInt("Master", 70);
        Music.value = PlayerPrefs.GetInt("Music", 50);
        Sound.value = PlayerPrefs.GetInt("Sound", 50);
    }

    private void OnEnable()
    {
        Master.value = PlayerPrefs.GetInt("Master", 70);
        Music.value = PlayerPrefs.GetInt("Music", 50);
        Sound.value = PlayerPrefs.GetInt("Sound", 50);
    }

    private void Update()
    {
        if (Master != null)
        {
            MasterValue.text = "" + Master.value;
        }

        if (Music != null)
        {
            MusicValue.text = "" + Music.value;
        }

        if (Sound != null)
        {
            SoundValue.text = "" + Sound.value;
        }
    }


    public void SetVolume()
    {
        if (gameObject.activeInHierarchy)
        {
            if (audioManager != null)
            {
                audioManager.SetMaster((int)Master.value);
                audioManager.SetMusic((int)Music.value);
                audioManager.SetSound((int)Sound.value);
                audioManager.SetAll();

            }
        }
    }

    public void Open()
    {
        gameObject.SetActive(true);
        Master.value = PlayerPrefs.GetInt("Master");
        Music.value = PlayerPrefs.GetInt("Music");
        Sound.value = PlayerPrefs.GetInt("Sound");
    }

    public void Close()
    {
        SetVolume();
        gameObject.SetActive(false);
    }




}
