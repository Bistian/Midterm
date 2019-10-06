using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private string gunName = null;
    [SerializeField] private float damage = 1f;
    [SerializeField] private GameObject bulletPrefab = null;
    [SerializeField] private Transform firePoint = null;
    [SerializeField] private int sodaMaxAmmunition = 10;
    [HideInInspector] public int sodaAmmunition = 0;
    Animator anim;
    GameObject UI = null;
    GameObject player = null;
    GameObject ammunitionBar = null;

    private void Start()
    {
        sodaAmmunition = sodaMaxAmmunition;
        player = GameObject.FindGameObjectWithTag("Player");
        UI = GameObject.FindGameObjectWithTag("Main HUD");
        if (gameObject.tag != "Enemy" && UI != null)
        {
            UI.GetComponent<HudScript>().maxAmmunition = sodaMaxAmmunition;
        }
        ammunitionBar = GameObject.FindGameObjectWithTag("AmmunitionBar");
    }

    void Update()
    {
        Reload();
        if (ammunitionBar != null)
            UI.GetComponent<HudScript>().currentAmmunition = sodaAmmunition;
        
        if (player.GetComponent<PlayerController>().canShoot == true && (Input.GetMouseButtonDown(0) || Input.GetKeyDown((KeyCode.KeypadEnter)) || Input.GetKeyDown((KeyCode.Return))))        
        {
            if (ActivateMenu.GameIsPaused == false && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().Died != true)
            {
                if (gunName == "Stapler" || gunName == "Pencil Crossbow" || gunName == "Soda Launcher")
                {
                    if (gunName == "Stapler")
                    {
                        Fire();
                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.PlayPew();
                    }
                    else if (gunName == "Pencil Crossbow")
                    {
                        Fire();
                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Pencil();
                    }
                    else if (gunName == "Soda Launcher")
                    {
                        if (sodaAmmunition > 0)
                        {
                            sodaAmmunition--;
                            Fire();
                            GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.SodaLauncher();
                        }
                    }
                }
            }
        }
    }

    public void Fire()
    {
        if (firePoint != null)
        {
            bulletPrefab.GetComponent<Bullet>().Damage = damage;
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }
    public void Reload()
    {
        if (player != null && player.GetComponent<PlayerController>().gotToElevator)
        {
            sodaAmmunition = sodaMaxAmmunition;
            player.GetComponent<PlayerController>().gotToElevator = false;
        }
    }

   
}
