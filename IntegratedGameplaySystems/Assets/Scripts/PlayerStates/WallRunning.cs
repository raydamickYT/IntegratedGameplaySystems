using UnityEngine;

public class WallRunning
{
    private ActorData playerData;

    private float maxWallDistance = 1.0f;
    private float minJumpHeight = .5f;

    private float speedIncrease;
    private bool speedIncreased = false;
    private float amountOfSpeedIncreaseWhileWallRunning = 1.0f;

    private bool isWallRunning = false;

    public WallRunning(ActorData _playerData, ActorBase owner)
    {
        playerData = _playerData;

        owner.OnUpdateEvent += OnUpdate;
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

        playerData.ActorRigidBody.velocity = new Vector3(playerData.ActorRigidBody.velocity.x, 0.0f, playerData.ActorRigidBody.velocity.z);

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
    }

    private void WallCheck()
    {
        playerData.WallLeft = Physics.SphereCast(playerData.ActorMesh.transform.position, .5f, playerData.ActorMesh.transform.right, out _, maxWallDistance, playerData.WallRunLayerMask);
        playerData.WallRight = Physics.SphereCast(playerData.ActorMesh.transform.position, .5f, -playerData.ActorMesh.transform.right, out _, maxWallDistance, playerData.WallRunLayerMask);
    }

    private void OnFixedUpdate()
    {
        if (playerData.isWallRunning)
        {
            WallRunningMovement();
        }
    }

    private void OnUpdate()
    {
        WallCheck();

        if ((playerData.WallLeft || playerData.WallRight) && GroundCheck())
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