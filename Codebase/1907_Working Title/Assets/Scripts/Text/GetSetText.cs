using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GetSetText : MonoBehaviour
{
    [SerializeField] GameObject Namemenu;
    public void SetText(string text)
    {
        PlayerPrefs.SetString("SaveName" + SaveSlots.SlotNumber, text);
    }

    public void Check(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SetText(text);
            SceneManager.LoadScene("Intro");
        }
    }
}
