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

    public ObjectPool ObjectPool;
    public InputHandler inputHandler;
    public GameObject BulletPrefab;
    public Bullets bullets;

    public List<IUpdate> UpdatableObjects = new();

    #region Dictionaries and Lists
    public List<GameObject> InactivePooledObjects = new();
    public List<GameObject> ActivePooledObjects = new();

    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, IPoolable> InstantiatedObjects = new Dictionary<string, IPoolable>();
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

        var playerMovement = new PlayerMovement(playerData, this);
        var cameraControl = new CameraControl(playerData);
        var jumping = new Jumping(playerData, this);
        var sliding = new Sliding(playerData);
        var sprinting = new Sprinting(playerData);
        //WallRunning wallRun = new(playerData);

        playerData.playerWASDKeys.Clear();
        playerData.playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.W, new MovementContext { Direction = Vector3.forward }));
        playerData.playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.A, new MovementContext { Direction = Vector3.left }));
        playerData.playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.S, new MovementContext { Direction = Vector3.back }));
        playerData.playerWASDKeys.Add(inputHandler.BindInputToCommand(playerMovement, KeyCode.D, new MovementContext { Direction = Vector3.right }));

        inputHandler.BindInputToCommand(cameraControl, isMouseControl: true);

        inputHandler.BindInputToCommand(jumping, KeyCode.Space, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(sprinting, KeyCode.LeftShift);
        inputHandler.BindInputToCommand(sliding, KeyCode.LeftControl);

        fsm.AddState(new InstantiateGameObjects(fsm, ObjectPool, this));
        fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    public void AddToUpdatableList(IUpdate objectToUpdate)
    {
        if (UpdatableObjects.Contains(objectToUpdate)) { return; }

        UpdatableObjects.Add(objectToUpdate);
    }

    private void Update()
    {
        Debug.Log(InstantiatedObjects.Count);
        Debug.Log($" [{string.Join(",", InstantiatedObjects)}]");
        inputHandler.HandleInput();
        fsm.OnUpdate();

        foreach (IUpdate objectToUpdate in UpdatableObjects)
        {
            objectToUpdate.OnUpdate();
        }
    }
}
