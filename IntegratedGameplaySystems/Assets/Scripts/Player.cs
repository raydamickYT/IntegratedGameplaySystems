using UnityEngine;

public class Player : ActorBase
{
    public readonly InputHandler InputHandler = new();
    private GameManager gameManager;
    private ActorData playerData;

    public Player(GameManager gameManager, ActorData playerData) : base(playerData.ActorMesh)
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
        playerData.playerCamera = GameObject.FindObjectOfType<Camera>();
        playerData.playerCameraTransform = playerData.playerCamera.gameObject.transform;
        playerData.playerCameraHolderTransform = playerData.ActorMesh.GetComponentInChildren<Grid>().gameObject.transform;

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
        var playerMovement = new PlayerMovement(playerData, this);
        var EquipmentManager = new EquipmentManager(gameManager);

        var shooting = new Shooting(gameManager, playerData);

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
        InputHandler.BindInputToCommand(shooting, KeyCode.Mouse0);

        //equipment
        InputHandler.BindInputToCommand(EquipmentManager, KeyCode.Alpha1, new WeaponSelectContext { WeaponIndex = 0 });
        InputHandler.BindInputToCommand(EquipmentManager, KeyCode.Alpha2, new WeaponSelectContext { WeaponIndex = 1 });
        //inputHandler.BindInputToCommand(EquipmentManager, KeyCode.Alpha3, new WeaponSelectContext {WeaponIndex = 2});
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