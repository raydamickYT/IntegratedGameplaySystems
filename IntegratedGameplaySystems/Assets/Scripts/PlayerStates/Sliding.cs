using UnityEngine;

public class Sliding : State<GameManager>, ICommand
{
    private readonly PlayerData playerData;

    public Sliding(PlayerData _playerData)
    {
        playerData = _playerData;
    }

    public void Execute(object context = null)
    {
        playerData.CurrentMoveSpeed = playerData.SlideSpeedBoost;
    }

    public void OnKeyDownExecute()
    {
        playerData.PlayerMesh.transform.localScale = new Vector3(1, 0.5f, 1);
    }

    public void OnKeyUpExecute()
    {
        playerData.CurrentMoveSpeed = playerData.MaxMovementSpeed;
        playerData.PlayerMesh.transform.localScale = Vector3.one;
    }
}