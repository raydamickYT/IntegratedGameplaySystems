using UnityEngine;

public class Player : ActorBase
{
    public readonly InputHandler InputHandler = new();
    private GameManager gameManager;
    private ActorData playerData;

    public Player(GameManager gameManager, ActorData playerData)
    {
        this.playerData = playerData;
        this.gameManager = gameManager;

        Initialization();

        gameManager.OnUpdate += OnUpdate;
        gameManager.OnFixedUpdate += OnFixedUpdate;
        gameManager.OnDisableEvent += OnDisable;
    }

    private void OnDisable()
    {
        NoLongerMoving = null;
        StartedMoving = null;
    }

    private void Initialization()
    {
        playerData.ActorMesh = GameObject.Instantiate(playerData.ActorPrefab, gameManager.transform.position, Quaternion.identity);
        playerData.ActorRigidBody = playerData.ActorMesh.GetComponent<Rigidbody>();
        playerData.playerCameraTransform = GameObject.FindObjectOfType<Camera>().gameObject.transform;
        playerData.playerCameraHolderTransform = playerData.ActorMesh.GetComponentInChildren<Grid>().gameObject.transform;

        playerData.CurrentMoveSpeed = playerData.StandardMovementSpeed;

        SetupInputsAndStates();
    }

    private void SetupInputsAndStates()
    {
        var playerMovement = new PlayerMovement(playerData, this);
        var cameraControl = new CameraControl(playerData);
        var jumping = new Jumping(playerData, this);
        var sliding = new Sliding(playerData);
        var sprinting = new Sprinting(playerData);
        var wallRun = new WallRunning(playerData, this);

        InputHandler.BindInputToCommand(playerMovement, KeyCode.W, new MovementContext { Direction = Vector3.forward }, isMovementKey: true);
        InputHandler.BindInputToCommand(playerMovement, KeyCode.A, new MovementContext { Direction = Vector3.left }, isMovementKey: true);
        InputHandler.BindInputToCommand(playerMovement, KeyCode.S, new MovementContext { Direction = Vector3.back }, isMovementKey: true);
        InputHandler.BindInputToCommand(playerMovement, KeyCode.D, new MovementContext { Direction = Vector3.right }, isMovementKey: true);

        InputHandler.BindInputToCommand(cameraControl, isMouseControl: true);

        InputHandler.BindInputToCommand(jumping, KeyCode.Space, new MovementContext { Direction = Vector3.up });
        InputHandler.BindInputToCommand(sliding, KeyCode.LeftControl, new MovementContext { Direction = Vector3.down });
        InputHandler.BindInputToCommand(sprinting, KeyCode.LeftShift);
    }

    private void OnFixedUpdate()
    {
        OnFixedUpdateEvent?.Invoke();
    }

    private void OnUpdate()
    {
        OnUpdateEvent?.Invoke();
        InputHandler.HandleInput();
    }
}