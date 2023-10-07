public class Sprinting : ICommand
{
    private ActorData playerData;

    public Sprinting(ActorData playerData)
    {
        this.playerData = playerData;
    }

    public void Execute(object context = null)
    {
        //playerData.CurrentMoveSpeed = playerData.SprintSpeedBoost;
    }

    public void OnKeyDownExecute()
    {
        playerData.CurrentMoveSpeed += 5.0f;
    }

    public void OnKeyUpExecute()
    {
        playerData.CurrentMoveSpeed -= 5.0f;
        //playerData.CurrentMoveSpeed = playerData.StandardMovementSpeed;
    }
}
