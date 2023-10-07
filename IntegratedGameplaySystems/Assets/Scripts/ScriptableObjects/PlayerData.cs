using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [NonSerialized] public GameObject PlayerMesh;

    public GameObject PlayerPrefab;

    [NonSerialized] public Rigidbody playerRigidBody;

    public Vector3 playerPosition;

    public float MaxMovementSpeed = 10.0f;

    public float CurrentMoveSpeed = 10.0f;

    public float SlideSpeedBoost = 25.0f;

    public float SprintSpeedBoost = 20.0f;

    public float PlayerMass = 10.0f;

    public LayerMask WallRunLayerMask = 0;

    public LayerMask GroundLayerMask = 0;

    public bool isWallRunning = false;

    [NonSerialized] public Transform playerCameraTransform;
    [NonSerialized] public Transform playerCameraHolderTransform;

    [NonSerialized] public List<KeyCommand> playerWASDKeys = new List<KeyCommand>();
}
