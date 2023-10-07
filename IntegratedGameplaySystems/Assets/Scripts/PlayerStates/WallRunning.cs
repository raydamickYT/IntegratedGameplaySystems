using UnityEngine;

public class WallRunning : State<GameManager>
{
    private PlayerData playerData;

    private float maxWallRunTime = 1.5f;
    private bool isWallRunning = false;
    private float wallRunTimer;
    private float maxWallDistance = .5f;
    private float minJumpHeight = .5f;

    private RaycastHit leftWallHit;
    private RaycastHit rightWallHit;
    private bool wallLeft, wallRight;

    private bool wallRunning = false;

    public WallRunning(PlayerData _playerData, GameManager gameManager, FSM<GameManager> fsm)
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

        rb.useGravity = false;
        rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
    }

    private void WallCheck()
    {
        wallLeft = Physics.Raycast(playerData.PlayerMesh.transform.position, playerData.PlayerMesh.transform.right, out leftWallHit, maxWallDistance, playerData.WallRunLayerMask);
        wallRight = Physics.Raycast(playerData.PlayerMesh.transform.position, -playerData.PlayerMesh.transform.right, out rightWallHit, maxWallDistance, playerData.WallRunLayerMask);
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
