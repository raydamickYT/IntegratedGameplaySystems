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

    public List<KeyCommand> playerWASDKeys = new List<KeyCommand>();

    #region Dictionaries and Lists
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();
    #endregion

    private void Start()
    {
        inputHandler = new InputHandler();

        playerData.PlayerMesh = Instantiate(playerData.PlayerPrefab);
        playerData.playerRigidBody = playerData.PlayerMesh.GetComponent<Rigidbody>();

        SetupInputsAndStates();

        fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    private void SetupInputsAndStates()
    {
        var playerMovement = new PlayerMovement(fsm, playerData);
        var fireGun = new FireGunCommand(fsm);
        var sliding = new Sliding(fsm, playerData);
        var jumping = new Jumping(fsm, playerData);

        var wallRun = new WallRunning(playerData);

        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });

        playerWASDKeys.Clear();
        playerData.playerWASDKeys.Clear();
        playerWASDKeys.Add(inputHandler.BindInputToCommand(KeyCode.W, playerMovement, new MovementContext { Direction = Vector3.forward }));
        playerWASDKeys.Add(inputHandler.BindInputToCommand(KeyCode.A, playerMovement, new MovementContext { Direction = Vector3.left }));
        playerWASDKeys.Add(inputHandler.BindInputToCommand(KeyCode.S, playerMovement, new MovementContext { Direction = Vector3.back }));
        playerWASDKeys.Add(inputHandler.BindInputToCommand(KeyCode.D, playerMovement, new MovementContext { Direction = Vector3.right }));
        playerData.playerWASDKeys = playerWASDKeys;

        inputHandler.BindInputToCommand(KeyCode.LeftControl, sliding, new MovementContext { Direction = Vector3.down });
        inputHandler.BindInputToCommand(KeyCode.Space, jumping, new MovementContext { Direction = Vector3.up });

        fsm.AddState(jumping);
        fsm.AddState(new InstantiateGameObjects(fsm, ObjectPool, this));
        fsm.AddState(fireGun);
        fsm.AddState(new IdleState(fsm));
        fsm.AddState(playerMovement);
    }

    private void Update()
    {
        inputHandler.HandleInput();
        fsm.OnUpdate();
    }
}