using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : ICommand
{
    private PlayerData playerData;

    public PlayerMovement(PlayerData _playerData, GameManager gameManager)
    {
        playerData = _playerData;
    }

    public void Execute(object context = null)
    {
        if (playerData.playerRigidBody == null || context is not MovementContext movementContext) { return; }

        playerData.playerRigidBody.AddRelativeForce(playerData.CurrentMoveSpeed * movementContext.Direction, ForceMode.Force);

        SpeedControl();
    }

    private void SpeedControl()
    {
        var rb = playerData.playerRigidBody;
        Vector3 flatVelocity = new(rb.velocity.x, 0.0f, rb.velocity.z);

        if (flatVelocity.magnitude > playerData.CurrentMoveSpeed)
        {
            Vector3 newVelocity = flatVelocity.normalized * playerData.CurrentMoveSpeed;

            rb.velocity = new(newVelocity.x, rb.velocity.y, newVelocity.z);
        }
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
    }
}