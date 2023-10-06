using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly FSM<GameManager> fsm = new();

    public PlayerData playerData;

    #region Adjustable Variables
    public int AmountToPool = 30;
    #endregion

    #region Delegates
    public delegate void Deactivationhandler(GameObject bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate GameObject ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public InputHandler inputHandler;
    public ObjectPool ObjectPool = new();
    public GameObject Prefab;

    public List<IUpdate> UpdatableObjects = new();

    #region Dictionaries and Lists
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();
    #endregion

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SetUpPlayerData();
        SetupInputsAndStates();

    }

    private void SetUpPlayerData()
    {
        playerData.PlayerMesh = Instantiate(playerData.PlayerPrefab, transform.position, Quaternion.identity);
        playerData.playerRigidBody = playerData.PlayerMesh.GetComponent<Rigidbody>();
        playerData.playerCameraTransform = FindObjectOfType<Camera>().gameObject.transform;
        playerData.playerCameraHolderTransform = playerData.PlayerMesh.GetComponentInChildren<Grid>().gameObject.transform;
    }

    private void SetupInputsAndStates()
    {
        inputHandler = new InputHandler();

        PlayerMovement playerMovement = new(playerData, this);
        FireGunCommand fireGun = new(fsm);
        Sliding sliding = new(fsm, playerData);
        Jumping jumping = new(playerData, this);
        //WallRunning wallRun = new(playerData);
        CameraControl cameraControl = new(playerData);

        List<KeyCommand> playerWASDKeys = new();
        playerWASDKeys.Clear();
        playerData.playerWASDKeys.Clear();
        playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.W, new MovementContext { Direction = Vector3.forward }));
        playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.A, new MovementContext { Direction = Vector3.left }));
        playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.S, new MovementContext { Direction = Vector3.back }));
        playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.D, new MovementContext { Direction = Vector3.right }));
        playerData.playerWASDKeys = playerWASDKeys;

        inputHandler.BindInputToCommand(cameraControl, isMouseControl: true);

        inputHandler.BindInputToCommand(jumping, KeyCode.Space, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(sliding, KeyCode.LeftControl, new MovementContext { Direction = Vector3.down });

        fsm.AddState(new InstantiateGameObjects(fsm, ObjectPool, this));
        fsm.AddState(fireGun);
        fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    public void AddToUpdatableList(IUpdate objectToUpdate)
    {
        if (UpdatableObjects.Contains(objectToUpdate)) { return; }

        UpdatableObjects.Add(objectToUpdate);
    }

    float xRotation, yRotation;

    private void Update()
    {
        inputHandler.HandleInput();
        fsm.OnUpdate();

        foreach (IUpdate objectToUpdate in UpdatableObjects)
        {
            objectToUpdate.OnUpdate();
        }
    }
}