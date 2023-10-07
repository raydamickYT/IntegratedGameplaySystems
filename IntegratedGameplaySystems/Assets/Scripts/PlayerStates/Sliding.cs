using UnityEngine;

public class Sliding : State, ICommand
{
    private readonly PlayerData playerData;

    public Sliding(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    public void Execute(object context = null)
    {
        playerData.CurrentMoveSpeed = playerData.SlideSpeedBoost;

        if (context is MovementContext movementContext)
        {
            playerData.playerRigidBody.AddRelativeForce(movementContext.Direction * 5.0f, ForceMode.Force);
        }
    }

    public void OnKeyDownExecute()
    {
        playerData.PlayerMesh.transform.localScale = new Vector3(1, 0.5f, 1);
    }

    public void OnKeyUpExecute()
    {
        playerData.CurrentMoveSpeed = playerData.StandardMovementSpeed;
        playerData.PlayerMesh.transform.localScale = Vector3.one;
    }
}