using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onActivate : MonoBehaviour
{
    [SerializeField] Button button = null;
    public int slot;
    void OnEnable()
    {
        button.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("SaveName" + slot);
    }
}
