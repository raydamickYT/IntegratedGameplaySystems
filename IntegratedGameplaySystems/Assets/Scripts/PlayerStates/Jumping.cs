using System.Timers;
using UnityEngine;

public class Jumping : ICommand
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

        gameManager.OnUpdate += OnUpdate;
    }

    private void ResetJump(object sender, ElapsedEventArgs e)
    {
        canJump = true;
    }

    public void Execute(object context = null)
    {
        if (context is not MovementContext movementContext) { return; }

        bool grounded = GroundCheck();
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

    private bool GroundCheck()
    {
        if (playerData.isWallRunning)
        {
            return true;
        }
        else
        {
            var playerMeshTransform = playerData.PlayerMesh.transform;
            var ray = new Ray(playerMeshTransform.position, -playerMeshTransform.up);
            return Physics.SphereCast(ray, 0.5f, 1f, layerMask: playerData.GroundLayerMask);
        }
    }

    public void OnUpdate()
    {
        CheckForExtraJumpReset();
    }

    private void CheckForExtraJumpReset()
    {
        bool grounded = GroundCheck();

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