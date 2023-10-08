using UnityEngine;

public class WallRunning
{
    private ActorData playerData;

    private float maxWallDistance = .5f;
    private float minJumpHeight = .5f;

    private bool wallLeft, wallRight;

    private float speedIncrease;
    private float amountOfSpeedIncreaseWhileWallRunning = 1.0f;
    private bool speedIncreased = false;

    private bool isWallRunning = false;

    public WallRunning(ActorData _playerData, ActorBase owner)
    {
        playerData = _playerData;

        owner.OnUpdateEvent += WallRunningOnUpdate;
        owner.OnFixedUpdateEvent += OnFixedUpdate;

        owner.NoLongerMoving += MovementStopped;
    }

    private bool GroundCheck()
    {
        return !Physics.Raycast(playerData.ActorMesh.transform.position, -playerData.ActorMesh.transform.up, minJumpHeight, playerData.GroundLayerMask);
    }

    private void StartWallRun()
    {
        if (isWallRunning) { return; }

        speedIncreased = true;
        isWallRunning = true;
        playerData.isWallRunning = true;
        playerData.ActorRigidBody.useGravity = false;
    }

    private void MovementStopped()
    {
        if (speedIncreased == false) { return; }

        speedIncreased = false;
        playerData.CurrentMoveSpeed -= speedIncrease;
        speedIncrease = 0.0f;
    }

    private void StopWallRun()
    {
        if (!isWallRunning) { return; }

        isWallRunning = false;
        playerData.ActorRigidBody.useGravity = true;
        playerData.isWallRunning = false;
    }

    private void WallRunningMovement()
    {
        playerData.CurrentMoveSpeed += amountOfSpeedIncreaseWhileWallRunning * Time.deltaTime;
        speedIncrease += amountOfSpeedIncreaseWhileWallRunning * Time.deltaTime;

        Vector3 directionToWall = wallLeft ? Vector3.left : Vector3.right;

        playerData.ActorRigidBody.AddRelativeForce(20.0f * directionToWall.normalized);
    }

    private void WallCheck()
    {
        wallLeft = Physics.SphereCast(playerData.ActorMesh.transform.position, .5f, playerData.ActorMesh.transform.right, out RaycastHit leftWallHit, maxWallDistance, playerData.WallRunLayerMask);
        wallRight = Physics.SphereCast(playerData.ActorMesh.transform.position, .5f, -playerData.ActorMesh.transform.right, out RaycastHit rightWallHit, maxWallDistance, playerData.WallRunLayerMask);
    }

    private void OnFixedUpdate()
    {
        if (playerData.isWallRunning)
        {
            WallRunningMovement();
        }
    }

    private void WallRunningOnUpdate()
    {
        WallCheck();

        if ((wallLeft || wallRight) && GroundCheck())
        {
            if (!isWallRunning)
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