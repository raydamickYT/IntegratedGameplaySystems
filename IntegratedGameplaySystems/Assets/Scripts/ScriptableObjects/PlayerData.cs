using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public GameObject PlayerMesh;

    public GameObject PlayerPrefab;

    public Rigidbody playerRigidBody;

    public Vector3 playerPosition;

    public float MovementSpeed = 10.0f;

    public float SlideSpeedBoost = 15.0f;

    public float PlayerMass = 10.0f;
}
