using UnityEngine;

public class PlayerMovement : ICommand
{
    private ActorData playerData;
    private Player owner;

    public PlayerMovement(ActorData _playerData, Player owner)
    {
        playerData = _playerData;
        this.owner = owner;
    }

    public void Execute(object context = null)
    {
        if (playerData.ActorRigidBody == null || context is not MovementContext movementContext) { return; }

        playerData.ActorRigidBody.AddRelativeForce(100.0f * Time.deltaTime *
            playerData.CurrentMoveSpeed * movementContext.Direction.normalized, ForceMode.Force);

        SpeedControl();
    }

    private void SpeedControl()
    {
        var rb = playerData.ActorRigidBody;
        Vector3 flatVelocity = new(rb.velocity.x, 0.0f, rb.velocity.z);

        if (flatVelocity.magnitude > playerData.CurrentMoveSpeed)
        {
            Vector3 newVelocity = flatVelocity.normalized * playerData.CurrentMoveSpeed;

            rb.velocity = new(newVelocity.x, rb.velocity.y, newVelocity.z);
        }
    }

    public void OnKeyDownExecute(object context = null)
    {
    }

    public void OnKeyUpExecute()
    {
        if (!owner.InputHandler.IsAKeyPressed())
        {
            owner.NoLongerMoving?.Invoke();
        }
    }
}