using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{ //this is the script that is activated when the player activates a weapon pickup
    public GameObject player;
    //find the player

    public bool selectable = false;
    //check if this weapon is unlocked
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if player touches it
        if (other.gameObject == player) 
        {//it becomes accessible and is destroyed.
            selectable = true;
            Destroy(gameObject);
        }
    }
}
