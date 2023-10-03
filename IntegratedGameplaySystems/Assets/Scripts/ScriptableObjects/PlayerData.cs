using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [NonSerialized] public GameObject PlayerMesh;

    public GameObject PlayerPrefab;

    [NonSerialized] public Rigidbody playerRigidBody;

    public Vector3 playerPosition;

    public float MovementSpeed = 10.0f;

    public float SlideSpeedBoost = 15.0f;

    public float PlayerMass = 10.0f;

    public LayerMask WallRunLayerMask;

    [NonSerialized] public List<KeyCommand> playerWASDKeys = new List<KeyCommand>();
}
