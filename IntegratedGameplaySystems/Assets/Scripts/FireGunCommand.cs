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
    }

    public void OnKeyUpExecute()
    {
    }

    public void Execute(object context = null)
    {
    }
}