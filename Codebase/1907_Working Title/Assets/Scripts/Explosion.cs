using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float time = 0.3f;
    [SerializeField] float damage = 1f;
    bool playerTookDamage = false;
    bool enemyTookDamage = false;

    private void Start()
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Explosion();
    }
    private void OnTriggerEnter(Collider damageable)
    {
        if (damageable.gameObject.tag == "Player")
        {
            if (!playerTookDamage)
            {
                damageable.gameObject.GetComponent<PlayerController>().TakeDamage(1);
                playerTookDamage = true;
            }
        }

        if (damageable.gameObject.tag == "Enemy")
        {
            if (!enemyTookDamage)
            {
                EnemyBase enemy = damageable.gameObject.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    damageable.gameObject.GetComponent<EnemyBase>().TakeDamage(damage);
                }
                enemyTookDamage = true;
            }
        }

        if (damageable.gameObject.tag == "Cardboard")
        {
            damageable.gameObject.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.position, 5f, 2f);
            damageable.gameObject.GetComponent<ObjectHealth>()?.TakeDamage(damage);
            damageable.gameObject.GetComponent<BossHearts>()?.TakeDamage(damage);
        }

    }
    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            GetComponent<Collider>().enabled = false;
        }
    }
}
