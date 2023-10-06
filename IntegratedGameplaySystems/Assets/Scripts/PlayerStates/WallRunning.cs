using UnityEngine;

public class WallRunning : State<GameManager>, ICommand
{
    private PlayerData playerData;

    public WallRunning(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    public void Execute(object context = null)
    {
        if (playerData.playerRigidBody != null && context is MovementContext movementContext)
        {

        }
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
    }
}
