using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] GameObject boss = null;
    public Slider healthBar;

    private void Start()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = boss.GetComponent<EnemyBoss>().health;
            healthBar.minValue = 0f;
        }
    }

    private void Update()
    {
        if (healthBar != null && boss != null)
            healthBar.value = boss.GetComponent<EnemyBoss>().health;
    }
}
