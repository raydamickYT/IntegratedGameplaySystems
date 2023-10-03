using UnityEngine;

public class PlayerMovement : State<GameManager>, ICommand
{
    protected FSM<GameManager> fsm;

    private PlayerData playerData;
    public float velocity = 0.0f;
    private float deceleration = 0.01f;

    private bool IsMoving = false;

    public PlayerMovement(FSM<GameManager> _fsm, PlayerData _playerData)
    {
        fsm = _fsm;
        playerData = _playerData;
    }

    public override void OnEnter()
    {
    }

    public void Execute(KeyCode key, object context = null)
    {
        if (playerData.playerRigidBody != null && context is MovementContext movementContext)
        {
            IsMoving = true;
            playerData.playerRigidBody.drag = 0.0f;
            var force = playerData.MovementSpeed / 50.0f;
            var acceleration = force / playerData.PlayerMass;

            velocity += (acceleration) * Time.deltaTime;

            velocity = Mathf.Clamp(velocity, -playerData.MovementSpeed, playerData.MovementSpeed);

            playerData.PlayerMesh.transform.Translate(movementContext.Direction.normalized * velocity);
        }
    }

    public override void OnUpdate()
    {
        Deceleration();
    }

    private void Deceleration()
    {
        if (IsMoving == true) { return; }

        bool left = velocity < 0;

        if (left)
        {
            velocity = Mathf.Min(0, velocity + deceleration);
        }
        else if (!left && velocity != 0)
        {
            velocity = Mathf.Max(0, velocity - deceleration);
        }
    }

    public void OnKeyDownExecute()
    {
        fsm.SwitchState(typeof(PlayerMovement));
    }

    public void OnKeyUpExecute()
    {
        IsMoving = false;
    }
}