using System.Timers;
using UnityEngine;

public class Jumping : ICommand
{
    private ActorData playerData;
    private bool canJump = true;
    private float jumpCooldownDuration = .6f;
    private Timer jumpCooldownTimer;
    private bool hasExtraJump = true;
    private Rigidbody rb;

    public void OnKeyDownExecute(object context = null)
    {
    }

    public void OnKeyUpExecute()
    {
    }

    public Jumping(ActorData playerData, ActorBase owner)
    {
        this.playerData = playerData;

        rb = playerData.ActorRigidBody;

        jumpCooldownTimer = new Timer()
        {
            Interval = jumpCooldownDuration * 1000.0f
        };
        jumpCooldownTimer.Elapsed += ResetJump;

        owner.OnUpdateEvent += OnUpdate;
    }

    public void Execute(object context = null)
    {
        if (context is not Vector3 movementContext) { return; }

        bool grounded = GroundCheck();
        if ((grounded || (!grounded && hasExtraJump)) && canJump)
        {
            Jump(movementContext, !grounded);
        }
    }

    private void ResetJump(object sender, ElapsedEventArgs e)
    {
        canJump = true;
    }

    private bool GroundCheck()
    {
        if (playerData.isWallRunning)
        {
            return true;
        }
        else
        {
            var playerMeshTransform = playerData.ActorMesh.transform;
            var ray = new Ray(playerMeshTransform.position, -playerMeshTransform.up);
            return Physics.SphereCast(ray, 1.0f, 1.0f, layerMask: playerData.GroundLayerMask);
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

    private void Jump(Vector3 direction, bool extraJump = false)
    {
        if (extraJump)
        {
            hasExtraJump = false;
        }

        canJump = false;
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        rb.AddForce(playerData.JumpForce * direction.normalized, ForceMode.Impulse);

        jumpCooldownTimer.Start();
    }
}