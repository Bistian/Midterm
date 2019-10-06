using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHud : MonoBehaviour
{
    public bool isMainMenu = false;
    [SerializeField] GameObject MainHud = null;
    // Start is called before the first frame update
    void Start()
    {
        if (!isMainMenu)
        {
            MainHud.SetActive(true);
        }
    }
}
