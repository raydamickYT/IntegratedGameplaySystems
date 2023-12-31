public class Sprinting : ICommand
{
    private ActorData playerData;

    public Sprinting(ActorData playerData)
    {
        this.playerData = playerData;
    }

    public void Execute(object context = null)
    {
    }

    public void OnKeyDownExecute(object context = null)
    {
        playerData.CurrentMoveSpeed += playerData.SprintSpeedIncrease;
    }

    public void OnKeyUpExecute()
    {
        playerData.CurrentMoveSpeed -= playerData.SprintSpeedIncrease;
    }
}