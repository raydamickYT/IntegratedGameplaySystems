using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [NonSerialized] public GameObject PlayerMesh;

    [NonSerialized] public GameObject GunHolder;

    public GameObject PlayerPrefab;

    [NonSerialized] public Rigidbody playerRigidBody;

    public float StandardMovementSpeed = 10.0f;

    public float AirSpeedMultiplier = 0.8f;

    [NonSerialized] public float CurrentMoveSpeed;

    public float SlideSpeedBoost = 25.0f;

    public float SprintSpeedBoost = 20.0f;

    public LayerMask WallRunLayerMask = 0;

    public LayerMask GroundLayerMask = 0;

    public bool isWallRunning = false;

    [NonSerialized] public Transform playerCameraTransform;
    [NonSerialized] public Camera playerCamera;
    [NonSerialized] public Transform playerCameraHolderTransform;
}
