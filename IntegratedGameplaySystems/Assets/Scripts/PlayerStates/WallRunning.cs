using UnityEngine;

public class WallRunning : State
{
    private PlayerData playerData;

    private float maxWallDistance = .5f;
    private float minJumpHeight = .5f;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft, wallRight;

    private bool wallRunning = false;

    public WallRunning(PlayerData _playerData, GameManager gameManager)
    {
        playerData = _playerData;

        gameManager.OnUpdate += OnUpdate;
        gameManager.OnFixedUpdate += OnFixedUpdate;
    }

    private bool GroundCheck()
    {
        return !Physics.Raycast(playerData.PlayerMesh.transform.position, -playerData.PlayerMesh.transform.up, minJumpHeight, playerData.GroundLayerMask);
    }

    private void StartWallRun()
    {
        wallRunning = true;
        playerData.isWallRunning = true;
        playerData.playerRigidBody.useGravity = false;
    }

    private void StopWallRun()
    {
        wallRunning = false;
        playerData.playerRigidBody.useGravity = true;
        playerData.isWallRunning = false;
    }

    private void WallRunningMovement()
    {
        var rb = playerData.playerRigidBody;

        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);

        Vector3 wallNormal = wallLeft ? leftWallHit.normal : rightWallHit.normal;
        Vector3 wallForward = Vector3.Cross(playerData.PlayerMesh.transform.up, wallNormal);

        Vector3 directionToWall = wallLeft ? Vector3.left : Vector3.right;

        rb.AddRelativeForce(wallForward * playerData.CurrentMoveSpeed);

        rb.AddRelativeForce(directionToWall * 2.0f);
    }

    private void WallCheck()
    {
        wallLeft = Physics.SphereCast(playerData.PlayerMesh.transform.position, .5f, playerData.PlayerMesh.transform.right, out leftWallHit, maxWallDistance, playerData.WallRunLayerMask);
        wallRight = Physics.SphereCast(playerData.PlayerMesh.transform.position, .5f, -playerData.PlayerMesh.transform.right, out rightWallHit, maxWallDistance, playerData.WallRunLayerMask);
    }

    private void OnFixedUpdate()
    {
        if (wallRunning)
        {
            WallRunningMovement();
        }
    }

    private void OnUpdate()
    {
        WallCheck();

        if ((wallLeft || wallRight) && GroundCheck())
        {
            if (!wallRunning)
            {
                StartWallRun();
            }
        }
        else
        {
            StopWallRun();
        }
    }
}
