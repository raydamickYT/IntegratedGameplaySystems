using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public bool boltUnlocked = false;
    public bool gatlingUnlocked = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // var WeaponPickup = other.GetComponent<Weapon>();
        // if (other.GetComponent<Bolt>() != null) //if the object has the tag bolt
        // {
        //     boltUnlocked = true; //bolt weapon is unlocked
        // }
        // if (other.GetComponent<Gatling>() != null)
        // {
        //     gatlingUnlocked = true;
        // }
    }
}
