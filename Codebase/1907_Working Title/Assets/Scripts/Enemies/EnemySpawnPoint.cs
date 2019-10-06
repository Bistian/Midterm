using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    #region Variables
    // Enemy to be spawned.
    [SerializeField] GameObject enemy;

    //Distance between player and spawn point.
    Vector3 distance = new Vector3();

    // Get player.
    GameObject player;
    #endregion

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void Update()
    {
        distance = player.transform.position;
        if (this.transform.position.x <= distance.x &&
            this.transform.position.y <= distance.y)
        {

        }
        
    }
}
