public class Sprinting : ICommand
{
    private PlayerData playerData;

    public Sprinting(PlayerData playerData)
    {
        this.playerData = playerData;
    }

    public void Execute(object context = null)
    {
        playerData.CurrentMoveSpeed = playerData.SprintSpeedBoost;
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
        playerData.CurrentMoveSpeed = playerData.MaxMovementSpeed;
    }
}
