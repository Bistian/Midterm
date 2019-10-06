using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int Type = 0;
    [SerializeField] private GameObject explosion = null;
    [SerializeField] private GameObject pencil = null;
    private GameObject player = null;
    public GameObject hitEffect;
    private Rigidbody _rigidbody;

    private Quaternion rotation = Quaternion.Euler(0, 0, 0);

    public float Damage = 1f;
    public float speed = 20f;
    public float destroyTime = 1f;
    public bool collided = false;
    public bool trackPlayer = false;
    private float adjustX = 1f;
    private float adjustY = .5f;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        _rigidbody = GetComponent<Rigidbody>();
        if (trackPlayer)
            _rigidbody.velocity = (player.transform.position - this.transform.position).normalized * speed;
        else
            _rigidbody.velocity = transform.right * speed;

        if (!explosion)
            Destroy(gameObject, destroyTime);
    }


    private void OnCollisionEnter(Collision hitInfo)
    {
        if (Mathf.Round(this.transform.rotation.eulerAngles.y) == 0)
        {
            adjustX = 1f;
            adjustY = .1f;
            rotation = new Quaternion(transform.rotation.x, transform.rotation.w, transform.rotation.y, transform.rotation.y);
        }
        else if (Mathf.Round(this.transform.rotation.eulerAngles.y) == 180)
        {
            
            adjustX = -1f;
            adjustY = -.5f;
            rotation = new Quaternion(transform.rotation.x, transform.rotation.x, transform.rotation.w, transform.rotation.y);

        }

        if (!collided)
        {
            if (hitInfo.gameObject.tag != "Weapon" && hitInfo.gameObject.tag != "Elevator" && hitInfo.gameObject.tag != "Firepoint")
            {
                collided = true;
                if (Type == 0)
                {
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Type == 1)
                {
                    Instantiate(explosion, transform.position, explosion.transform.rotation);
                }
                else if (Type == 2)
                {
                    if (hitInfo.gameObject.tag == "MovingPlat")
                    {
                        Instantiate(pencil, new Vector3(transform.position.x - adjustX, transform.position.y + adjustY, 0), rotation);
                    }
                    if (hitInfo.gameObject.tag != "MovingPlat" && hitInfo.gameObject.tag != "Enemy" && hitInfo.gameObject.tag != "Player" && hitInfo.gameObject.tag != "PlayerProjectile" && hitInfo.gameObject.tag != "EnemyProjectile" && hitInfo.gameObject.tag != "Cardboard" && hitInfo.gameObject.tag != "Shield" && hitInfo.gameObject.tag != "Boss")
                    {
                        Instantiate(pencil, new Vector3(transform.position.x - adjustX, transform.position.y  + adjustY , 0), new Quaternion(transform.rotation.x, transform.rotation.x, transform.rotation.w, transform.rotation.y));
                    }
                }
                Destroy(gameObject);
            }

            if (hitInfo.gameObject.tag == "Cardboard")
            {
                collided = true;
                hitInfo.gameObject.GetComponent<ObjectHealth>()?.TakeDamage(Damage);
                hitInfo.gameObject.GetComponent<BossHearts>()?.TakeDamage(Damage);
                Destroy(gameObject);
            }

            if (hitInfo.gameObject.tag == "Player" && gameObject.tag != "PlayerProjectile")
            {
                collided = true;
                hitInfo.gameObject.GetComponent<PlayerController>().TakeDamage(Damage);
                Destroy(gameObject);
            }


            if (hitInfo.gameObject.tag == "Boss")
            {

                collided = true;
                EnemyBase enemy = hitInfo.gameObject.GetComponent<EnemyBase>();
                Destroy(gameObject);
            }

            if (hitInfo.gameObject.tag == "Enemy")
            {

                collided = true;
                EnemyBase enemy = hitInfo.gameObject.GetComponent<EnemyBase>();
                if (enemy != null)
                {
                    enemy.TakeDamage(Damage);
                }
                Destroy(gameObject);
            }
        }
    }

}
