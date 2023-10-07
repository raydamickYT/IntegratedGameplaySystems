using UnityEngine;

public class Player : ActorBase, IUpdate
{
    private InputHandler inputHandler;
    private GameManager gameManager;
    private PlayerData playerData;
    private FSM<Player> fsm;

    public Player(GameManager gameManager, PlayerData playerData) : base(playerData.PlayerMesh)
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
        playerData.playerCamera = GameObject.FindObjectOfType<Camera>();
        playerData.playerCameraTransform = playerData.playerCamera.gameObject.transform;
        playerData.playerCameraHolderTransform = playerData.PlayerMesh.GetComponentInChildren<Grid>().gameObject.transform;


        playerData.CurrentMoveSpeed = playerData.StandardMovementSpeed;
        //niet heel netjes, maar moet even voor nu.
        playerData.GunHolder = GameObject.Find("GunHolder");
        if (playerData.GunHolder == null)
        {
            Debug.LogWarning("GunHolder is niet gevonden in de scene");
        }

        SetupInputsAndStates();
    }

    private void SetupInputsAndStates()
    {
        inputHandler = new InputHandler();
        
        var shooting = new Shooting(gameManager, playerData);
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
        inputHandler.BindInputToCommand(sprinting, KeyCode.LeftShift);
        inputHandler.BindInputToCommand(sliding, KeyCode.LeftControl);
        inputHandler.BindInputToCommand(shooting, KeyCode.Mouse0);

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