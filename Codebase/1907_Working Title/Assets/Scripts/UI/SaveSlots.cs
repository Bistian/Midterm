using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class SaveSlots : MonoBehaviour
{
    [SerializeField] GameObject SaveMenu = null;
    [SerializeField] GameObject nameMenu = null;
    [SerializeField] EventSystem Events = null;
    [SerializeField] GameObject Name = null;
    public static int SlotNumber;
    public static bool usedOne = false;
    public static bool usedTwo = false;
    public static bool usedThree = false;
    
    public void SlotOne()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        SlotNumber = 1;
        usedOne = true;
        SaveMenu.SetActive(false);
        nameMenu.SetActive(true);
        Events.SetSelectedGameObject(Name);
    }

    public void SlotTwo()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        SlotNumber = 2;
        usedTwo = true;
        SaveMenu.SetActive(false);
        nameMenu.SetActive(true);
        Events.SetSelectedGameObject(Name);
    }

    public void SlotThree()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Button();
        SlotNumber = 3;
        usedThree = true;
        SaveMenu.SetActive(false);
        nameMenu.SetActive(true);
        Events.SetSelectedGameObject(Name);
    }
}
