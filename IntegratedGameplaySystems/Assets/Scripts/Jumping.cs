using System.Timers;
using UnityEngine;

public class Jumping : ICommand, IUpdate
{
    private PlayerData playerData;
    private bool canJump = true;
    private float jumpMagnitude = 10.0f;
    private float jumpCooldownDuration = 0.5f;
    private Timer jumpCooldownTimer;
    private bool hasExtraJump = true;
    private Rigidbody rb;

    public Jumping(PlayerData playerData, GameManager gameManager)
    {
        this.playerData = playerData;

        rb = playerData.playerRigidBody;

        jumpCooldownTimer = new Timer()
        {
            //Interval runs in milliseconds instead of seconds.
            Interval = jumpCooldownDuration * 1000.0f
        };
        jumpCooldownTimer.Elapsed += ResetJump;

        gameManager.AddToUpdatableList(this);
    }

    private void ResetJump(object sender, ElapsedEventArgs e)
    {
        canJump = true;
    }

    public void Execute(object context = null)
    {
        if (context is not MovementContext movementContext) { return; }

        var playerMeshTransform = playerData.PlayerMesh.transform;

        bool grounded = Physics.SphereCast(playerMeshTransform.position, radius: 0.5f, direction: -playerMeshTransform.up, out RaycastHit hit, 1f, layerMask: playerData.GroundLayerMask);

        if (grounded && canJump)
        {
            canJump = false;
            Jump(movementContext.Direction);
            jumpCooldownTimer.Start();
        }
        else if (hasExtraJump && canJump)
        {
            hasExtraJump = false;
            canJump = false;
            Jump(movementContext.Direction);
            jumpCooldownTimer.Start();
        }
    }

    public void OnUpdate()
    {
        CheckForExtraJumpReset();
    }

    private void CheckForExtraJumpReset()
    {
        var playerMeshTransform = playerData.PlayerMesh.transform;
        bool grounded = Physics.SphereCast(playerMeshTransform.position, radius: .5f, direction: -playerMeshTransform.up, out RaycastHit hit, 1f, layerMask: playerData.GroundLayerMask);
        if (grounded)
        {
            hasExtraJump = true;
        }
    }

    private void Jump(Vector3 direction)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(jumpMagnitude * direction.normalized, ForceMode.Impulse);
    }

    public void OnKeyDownExecute()
    {
    }

    public void OnKeyUpExecute()
    {
    }
}
