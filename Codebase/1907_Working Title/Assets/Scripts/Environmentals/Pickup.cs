using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{

    //Lists
    [SerializeField] List<GameObject> HeldWeapons = null;

    // All Weapons
    [SerializeField] GameObject Stapler = null;
    [SerializeField] GameObject PencilSlinger = null;
    [SerializeField] GameObject SodaLauncher = null;

    [SerializeField] public bool Soda = false;
    [SerializeField] public bool Pencil = false;

    //Current
    GameObject EquippedGun = null;



    //Curent weapon number for list
    int CurrentEquipped = 0;
    public GameObject GetEquipped()
    {
        return EquippedGun;
    }

    private void Start()
    {
        HeldWeapons.Add(Stapler);
    }

    int updateEquipped2(int CurrentEquipped)
    {
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>().WeaponSwap();
        CurrentEquipped++;
        if (CurrentEquipped == HeldWeapons.Count - 1)
        {
            CurrentEquipped = 0;
        }

        return CurrentEquipped;
    }

    void SetAllFalse()
    {
        Stapler.SetActive(false);
        SodaLauncher.SetActive(false);
        PencilSlinger.SetActive(false);
    }

    //Swap Weapons
    void Update()
    {
       

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (HeldWeapons[1] != null)
            {
                CurrentEquipped = updateEquipped2(CurrentEquipped);
                EquippedGun = HeldWeapons[CurrentEquipped];
                SetAllFalse();
                EquippedGun.SetActive(true);

            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
                CurrentEquipped = 0;
                EquippedGun = HeldWeapons[CurrentEquipped];
                SetAllFalse();
                EquippedGun.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (HeldWeapons.Count >= 1)
            {
                CurrentEquipped = 1;
                EquippedGun = HeldWeapons[CurrentEquipped];
                SetAllFalse();
                EquippedGun.SetActive(true);
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (HeldWeapons.Count > 2)
            {
                CurrentEquipped = 2;
                EquippedGun = HeldWeapons[CurrentEquipped];
                SetAllFalse();
                EquippedGun.SetActive(true);
            }
        }


        if (Soda == true || Pencil == true)
        {
            HeldWeapons.Add(Stapler);
            if (Soda == true && Pencil == false)
            {
                HeldWeapons[1] = SodaLauncher;
                Soda = false;
            }

            if (Pencil == true)
            {
                HeldWeapons[2] = PencilSlinger;
                Pencil = false;
            }
        }
    }


    //Pickup Item
    private void OnTriggerEnter(Collider col)
    {
        //If Weapon
        if (col.gameObject.tag == "Pencil" || col.gameObject.tag == "Soda")
        {
            HeldWeapons.Add(Stapler);

            //Unequipping
            Destroy(col.gameObject);
            SetAllFalse();

            //Collecting New Weapons
            if (col.gameObject.tag == "Pencil")
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Pickup();
                HeldWeapons[2] = PencilSlinger;
                if (CurrentEquipped == 1)
                {
                    CurrentEquipped = updateEquipped2(CurrentEquipped);
                }
                if (CurrentEquipped == 0)
                {
                    CurrentEquipped += 2;
                }
                PencilSlinger.SetActive(true);
            }

            else if (col.gameObject.tag == "Soda")
            {
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<AudioManager>()?.Pickup();
                HeldWeapons[1] = SodaLauncher;
                CurrentEquipped = updateEquipped2(CurrentEquipped);
                SodaLauncher.SetActive(true);
            }
        }
    }
}

