using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSave : MonoBehaviour
{
    private int sceneToLoad;
    public int slotnumber;
    public void SlotOne()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        SaveSlots.usedOne = true;
        if (SaveSlots.usedOne == true)
        {
            slotnumber = 1;
            SaveSlots.SlotNumber = 1;
        }
    }

    public void SlotTwo()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        SaveSlots.usedTwo = true;
        if (SaveSlots.usedTwo == true)
        {
            slotnumber = 2;
            SaveSlots.SlotNumber = 2;
        }
    }

    public void SlotThree()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        SaveSlots.usedThree = true;
        if (SaveSlots.usedThree == true)
        {
            slotnumber = 3;
            SaveSlots.SlotNumber = 3;
        }
    }
    public void Load()
    {
        if (PlayerPrefs.HasKey("SavedScene" + SaveSlots.SlotNumber) == true)
        {
            sceneToLoad = PlayerPrefs.GetInt("SavedScene" + SaveSlots.SlotNumber);

            if (sceneToLoad != 0 && slotnumber != 0 && slotnumber == SaveSlots.SlotNumber)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
                GameObject.FindGameObjectWithTag("LoadingScreen").GetComponent<LoadingScreenController>().LoadScreen(sceneToLoad);
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }
}
