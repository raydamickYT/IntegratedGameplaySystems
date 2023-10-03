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

    #region Dictionaries and Lists
    public List<GameObject> InactivePooledObjects = new();
    public List<GameObject> ActivePooledObjects = new();

    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, IPoolable> InstantiatedObjects = new Dictionary<string, IPoolable>();
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
        ObjectPool = new(this);
        var playerMovement = new PlayerMovement(BulletPrefab, playerData, this);
        var fireGun = new FireGunCommand(fsm);
        var sliding = new Sliding(fsm, playerData);
        var jumping = new Jumping(fsm, playerData);

        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.W, playerMovement, new MovementContext { Direction = Vector3.forward });
        inputHandler.BindInputToCommand(KeyCode.A, playerMovement, new MovementContext { Direction = Vector3.left });
        inputHandler.BindInputToCommand(KeyCode.S, playerMovement, new MovementContext { Direction = Vector3.back });
        inputHandler.BindInputToCommand(KeyCode.D, playerMovement, new MovementContext { Direction = Vector3.right });

        inputHandler.BindInputToCommand(KeyCode.LeftControl, sliding, new MovementContext { Direction = Vector3.down });
        inputHandler.BindInputToCommand(KeyCode.Space, jumping, new MovementContext { Direction = Vector3.up });

        fsm.AddState(jumping);
        fsm.AddState(new InstantiateGameObjects(fsm, ObjectPool, this));
        fsm.AddState(fireGun);
        fsm.AddState(new IdleState(fsm));
        //fsm.AddState(playerMovement);
    }

    private void Update()
    {
        Debug.Log(InstantiatedObjects.Count);
        Debug.Log($" [{string.Join(",", InstantiatedObjects)}]");
        inputHandler.HandleInput();
        fsm.OnUpdate();
    }
}
