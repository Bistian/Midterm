using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerTakeDamage : MonoBehaviour
{
    [SerializeField] GameObject mainBody = null;
    float health = 0;

    private void Start()
    {
        if (mainBody != null)
            health = mainBody.GetComponent<EnemyCharger>().maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerProjectile"))
        {
            health = mainBody.GetComponent<EnemyCharger>().currentHealth -= collision.gameObject.GetComponent<Bullet>().Damage;
        }
    }
}
