using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

//concrete command
public class FireGunCommand : State<GameManager>, ICommand
{
    protected FSM<GameManager> owner;


    public static bool _canFire = true;

    //dependency injection via de Finite state machine.
    public FireGunCommand(FSM<GameManager> _owner)
    {
        owner = _owner;
    }

    //dit is de uitvoering van de concrete command die geroepen wordt door de inputmanager.
    public void OnKeyDownExecute()
    {
        //owner.SwitchState(typeof(FireGunCommand));
    }
    //deze wordt gecalled wanneer de key omhoog komt
    public void OnKeyUpExecute()
    {
        //owner.SwitchState(typeof(IdleState));
    }

    //deze word constant gecalled terwijl de key naar beneden is
    public void Execute(KeyCode key, object context = null)
    {
        //FireGun(context);
        Debug.Log("pew");
    }

    public void FireGun(object context = null)
    {
        GameObject bullet = owner.pOwner.objectPoolDelegate?.Invoke();

        if (bullet != null && _canFire)
        {
            //set position
            bullet.transform.position = owner.pOwner.InstantiatedObjects["player"].gameObject.transform.position;
            bullet.transform.rotation = owner.pOwner.InstantiatedObjects["player"].gameObject.transform.rotation;

            //activeer de gameobject van "bullet"
            bullet.SetActive(true);

            Rigidbody rb = bullet.gameObject.GetComponent<Rigidbody>();
            if (rb != null && context is MovementContext movementContext)
            {
                //rb.velocity = movementContext.Direction.normalized * owner.pOwner.bullets.BulletSpeed; //bulletspeed is te veranderen in de scriptable object
            }

            //logic voor fire rate en bullet life
            //owner.pOwner.RunCoroutine(Wait());
            //owner.pOwner.RunCoroutine(BulletLifeTime(bullet));
        }
    }

    // public IEnumerator Wait()
    // {
    //     //vrij simpel, we zorgen dat de speler even niet meer kan schieten en dan na een bepaalde tijd kan de speler weer schieten
    //     _canFire = false;
    //     yield return new WaitForSeconds(owner.pOwner.bullets.FireRate); //FireRate kan je in de "GameManager" aanpassen, kleiner getal = sneller schieten.
    //     _canFire = true;
    // }

    // public IEnumerator BulletLifeTime(GameObject bullet)
    // {
    //     //net zo simpel, als er een bepaalde tijd verstreken is, dan word de bullet weer naar de inactive pool verplaatst.
    //     yield return new WaitForSeconds(owner.pOwner.bullets.BulletLife); //bulletlife kan je in de "GameManager" aanpassen.
    //     owner.pOwner.DeactivationDelegate?.Invoke(bullet);
    // }
}

public class PlayerMovement : State<GameManager>, ICommand
{
    Rigidbody rb;
    protected FSM<GameManager> owner;
    public PlayerMovement(FSM<GameManager> _owner)
    {
        owner = _owner;
    }

    public override void OnEnter()
    {
        GameObject player = owner.pOwner.InstantiatedObjects["player"].gameObject;
        rb = player.GetComponent<Rigidbody>();
    }

    public void Execute(KeyCode key, object context = null)
    {

        Debug.Log("hoi");
        if (rb != null && context is MovementContext movementContext)
        {
            // rb.velocity = movementContext.Direction.normalized * 10f; //bulletspeed is te veranderen in de scriptable object
        }
    }

    public void OnKeyDownExecute()
    {
        owner.SwitchState(typeof(PlayerMovement));
    }

    public void OnKeyUpExecute()
    {
        rb.velocity = new Vector3(0, 0, 0);

        //owner.SwitchState(typeof(IdleState));
    }
}