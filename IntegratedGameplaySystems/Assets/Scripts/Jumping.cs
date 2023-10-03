using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State<GameManager>, ICommand
{
    private FSM<GameManager> fsm;
    private PlayerData playerData;

    public Jumping(FSM<GameManager> fsm, PlayerData playerData)
    {
        this.fsm = fsm;
        this.playerData = playerData;
    }

    public void Execute(KeyCode key, object context = null)
    {
        if (context is MovementContext movementContext)
        {
            playerData.playerRigidBody.AddForce(movementContext.Direction.normalized * 10.0f, ForceMode.Impulse);
        }
    }

    public void OnKeyDownExecute()
    {
        fsm.SwitchState(typeof(Jumping));
    }

    public void OnKeyUpExecute()
    {
        fsm.SwitchState(typeof(PlayerMovement));
    }
}
