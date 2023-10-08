using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class ActorData : ScriptableObject
{
    [NonSerialized] public GameObject ActorMesh;

    [NonSerialized] public Rigidbody ActorRigidBody;

    [NonSerialized] public GameObject GunHolder;

    public GameObject ActorPrefab;

    public float JumpForce = 10.0f;

    public float CameraSensitivity;

    [NonSerialized] public Rigidbody playerRigidBody;

    public float StandardMovementSpeed;

    public float AirSpeedMultiplier = 0.5f;

    [NonSerialized] public float CurrentMoveSpeed;

    public float SlideSpeedIncrease = 25.0f;

    public float SprintSpeedIncrease = 20.0f;

    public LayerMask WallRunLayerMask = 0;

    public LayerMask GroundLayerMask = 0;

    public bool isWallRunning = false;

    [NonSerialized] public Transform playerCameraTransform;
    [NonSerialized] public Camera playerCamera;
    [NonSerialized] public Transform playerCameraHolderTransform;
}