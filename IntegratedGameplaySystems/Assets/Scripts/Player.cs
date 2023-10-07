using UnityEngine;

public class Player : ActorBase, IUpdate
{
    private InputHandler inputHandler;
    private GameManager gameManager;
    private PlayerData playerData;
    private FSM<Player> fsm;

    public Player(GameManager gameManager, PlayerData playerData)
    {
        this.playerData = playerData;
        this.gameManager = gameManager;

        gameManager.OnUpdate += OnUpdate;
        gameManager.OnFixedUpdate += OnFixedUpdate;

        fsm = new FSM<Player>();

        Initialization();
    }

    private void Initialization()
    {
        playerData.PlayerMesh = GameObject.Instantiate(playerData.PlayerPrefab, gameManager.transform.position, Quaternion.identity);
        playerData.playerRigidBody = playerData.PlayerMesh.GetComponent<Rigidbody>();
        playerData.playerCameraTransform = GameObject.FindObjectOfType<Camera>().gameObject.transform;
        playerData.playerCameraHolderTransform = playerData.PlayerMesh.GetComponentInChildren<Grid>().gameObject.transform;

        playerData.CurrentMoveSpeed = playerData.StandardMovementSpeed;

        SetupInputsAndStates();
    }

    private void SetupInputsAndStates()
    {
        inputHandler = new InputHandler();

        var playerMovement = new PlayerMovement(playerData, gameManager);
        var cameraControl = new CameraControl(playerData);
        var jumping = new Jumping(playerData, gameManager);
        var sliding = new Sliding(playerData);
        var sprinting = new Sprinting(playerData);
        var wallRun = new WallRunning(playerData, gameManager);

        inputHandler.BindInputToCommand(playerMovement, KeyCode.W, new MovementContext { Direction = Vector3.forward });
        inputHandler.BindInputToCommand(playerMovement, KeyCode.A, new MovementContext { Direction = Vector3.left });
        inputHandler.BindInputToCommand(playerMovement, KeyCode.S, new MovementContext { Direction = Vector3.back });
        inputHandler.BindInputToCommand(playerMovement, KeyCode.D, new MovementContext { Direction = Vector3.right });

        inputHandler.BindInputToCommand(cameraControl, isMouseControl: true);

        inputHandler.BindInputToCommand(jumping, KeyCode.Space, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(sliding, KeyCode.LeftControl, new MovementContext { Direction = Vector3.down });
        inputHandler.BindInputToCommand(sprinting, KeyCode.LeftShift);

        fsm.AddState(wallRun);
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate()
    {
        inputHandler.HandleInput();
        fsm.OnUpdate();
    }
}