using UnityEngine;

public class PlayerMovement : ICommand, IUpdate
{
    private PlayerData playerData;
    public float velocity = 0.0f;
    private bool IsMoving = false;

    public PlayerMovement(PlayerData _playerData, GameManager gameManager)
    {
        playerData = _playerData;

        gameManager.AddToUpdatableList(this);
    }

    public void Execute(object context = null)
    {
        if (playerData.playerRigidBody == null || context is not MovementContext movementContext) { return; }

        IsMoving = true;
        var force = playerData.CurrentMoveSpeed / 20.0f;
        var acceleration = force / playerData.PlayerMass;

        velocity += acceleration;

        velocity = Mathf.Clamp(velocity, -playerData.CurrentMoveSpeed, playerData.CurrentMoveSpeed);

        playerData.PlayerMesh.transform.Translate(Time.deltaTime * velocity * movementContext.Direction.normalized);
    }

    public void OnUpdate()
    {
        Deceleration();
    }

    private void Deceleration()
    {
        if (IsMoving == true) { return; }

        bool left = velocity < 0;

        if (left)
        {
            velocity = Mathf.Min(0, velocity + (20.0f * Time.deltaTime));
        }
        else if (!left && velocity != 0)
        {
            velocity = Mathf.Max(0, velocity - (20.0f * Time.deltaTime));
        }
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
        foreach (KeyCommand keyCommand in playerData.playerWASDKeys)
        {
            if (keyCommand.Pressed == true)
            {
                IsMoving = true;
                return;
            }
        }
        IsMoving = false;
    }
}