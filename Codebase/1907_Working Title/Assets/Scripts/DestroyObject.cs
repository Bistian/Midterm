﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public float timer = 1;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timer);
    }

}
