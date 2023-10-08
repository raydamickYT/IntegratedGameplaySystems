using UnityEngine;

public class PlayerMovement : ICommand
{
    private ActorData playerData;
    private Player owner;

    public PlayerMovement(ActorData _playerData, Player owner)
    {
        playerData = _playerData;
        this.owner = owner;

        owner.OnFixedUpdateEvent += OnFixedUpdate;
        owner.OnDisableEvent += OnDisable;
    }

    public void Execute(object context = null)
    {
        if (playerData.ActorRigidBody == null || context is not Vector3 movementContext) { return; }

        playerData.ActorRigidBody.AddRelativeForce(100.0f * Time.deltaTime * playerData.CurrentMoveSpeed * movementContext.normalized, ForceMode.Force);

        SpeedControl();
    }

    private void OnFixedUpdate()
    {
    }

    private void OnDisable()
    {
        owner.OnFixedUpdateEvent -= OnFixedUpdate;
        owner.OnDisableEvent -= OnDisable;
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

    public void OnKeyUpExecute()
    {
        if (!owner.InputHandler.IsAKeyPressed())
        {
            owner.NoLongerMoving?.Invoke();
        }
    }

    public void OnKeyDownExecute(object context = null)
    {
    }
}
