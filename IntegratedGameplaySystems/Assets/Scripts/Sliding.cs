using UnityEngine;

public class Sliding : State<GameManager>, ICommand
{
    private readonly FSM<GameManager> fsm;
    private readonly PlayerData playerData;

    public Sliding(FSM<GameManager> _fsm, PlayerData _playerData)
    {
        fsm = _fsm;
        playerData = _playerData;
    }

    public void Execute(KeyCode key, object context = null)
    {
        if (playerData.playerRigidBody != null && context is MovementContext)
        {
            playerData.MovementSpeed = playerData.SlideSpeedBoost;
        }
    }

    public void OnKeyDownExecute()
    {
        playerData.PlayerMesh.transform.localScale = new Vector3(1, 0.5f, 1);
        fsm.SwitchState(typeof(Sliding));
    }

    public void OnKeyUpExecute()
    {
        playerData.MovementSpeed = 10.0f;
        playerData.PlayerMesh.transform.localScale = Vector3.one;
    }
}