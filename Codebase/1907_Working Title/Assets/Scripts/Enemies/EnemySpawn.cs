using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------
// --------------> Ask Frey about this script <--------------
// ----------------------------------------------------------

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab = null;
    int health = 0;
    Animator anim;


    private void Start()
    {
        health = (int)enemyPrefab.GetComponent<EnemyBase>().maxHealth;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enemyPrefab.SetActive(true);
           

        }
    }
}
