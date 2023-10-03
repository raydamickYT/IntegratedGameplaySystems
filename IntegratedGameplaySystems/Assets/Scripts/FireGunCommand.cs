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

    public FireGunCommand(FSM<GameManager> _owner)
    {
        owner = _owner;
    }

    public void OnKeyDownExecute()
    {
        //owner.SwitchState(typeof(FireGunCommand));
    }

    public void OnKeyUpExecute()
    {
        //owner.SwitchState(typeof(IdleState));
    }

    public void Execute(KeyCode key, object context = null)
    {
    }
}