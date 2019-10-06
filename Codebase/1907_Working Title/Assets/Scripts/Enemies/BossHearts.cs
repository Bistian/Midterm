using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class BossHearts : MonoBehaviour
{
    float maxHealth = 5;
    float health = 5;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 1)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().GlassChip();
        }
        if (health < 1)
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().GlassBreak();
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void FillHealth()
    {
        health = maxHealth;
    }
    
}
