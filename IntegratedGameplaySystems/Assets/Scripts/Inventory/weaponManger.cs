using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManger : MonoBehaviour
{
    private int _totalWeapons = 1;
    public int currentWeaponIndex;
    public WeaponPickup pickup;
    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    // Start is called before the first frame update
    void Start()
    {
        _totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[_totalWeapons];

        for (int i = 0; i < _totalWeapons; i++)
        { //for as long as the amount of weapons
            guns[i] = weaponHolder.transform.GetChild(i).gameObject; //a gun gets added to a list
            guns[i].SetActive(false); // and it is set to false
        }

        guns[0].SetActive(true); //the starter gun (smg), is set to active
        currentWeapon = guns[0];// guns are recognized by numer
        currentWeaponIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchGun();
    }

    private void SwitchGun()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        { //if you press nr 1, you switch to weapon 0
                guns[currentWeaponIndex].SetActive(false); //turn off the last weapon
                currentWeaponIndex = 0; //change to a new gun (smg)
                guns[currentWeaponIndex].SetActive(true);
        }
        if (pickup.gatlingUnlocked){ //if the gun has been unlocked, you can use it
            if (Input.GetKey(KeyCode.Alpha2))
            { //if you press nr 2, you switch to weapon 1
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 1; //change to a new gun (bolt)
                guns[currentWeaponIndex].SetActive(true);
            }
        }
        if (pickup.boltUnlocked){//if the gun has been unlocked, you can use it
            if (Input.GetKey(KeyCode.Alpha3))
            { //if you press nr 3, you switch to weapon 2
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 2; // change to a new gun (Gatling)
                guns[currentWeaponIndex].SetActive(true);
            }
        }
    }
}
