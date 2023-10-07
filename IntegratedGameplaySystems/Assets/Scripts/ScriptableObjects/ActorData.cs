using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class ActorData : ScriptableObject
{
    [NonSerialized] public GameObject ActorMesh;

    [NonSerialized] public Rigidbody ActorRigidBody;

    public GameObject ActorPrefab;

    public float JumpForce = 10.0f;

    public float CameraSensitivity = 250.0f;

    public float StandardMovementSpeed = 10.0f;

    public float AirSpeedMultiplier = 0.8f;

    [NonSerialized] public float CurrentMoveSpeed;

    public float SlideSpeedIncrase = 25.0f;

    public float SprintSpeedIncrease = 20.0f;

    public LayerMask WallRunLayerMask = 0;

    public LayerMask GroundLayerMask = 0;

    public bool isWallRunning = false;

    [NonSerialized] public Transform playerCameraTransform;
    [NonSerialized] public Transform playerCameraHolderTransform;
}
