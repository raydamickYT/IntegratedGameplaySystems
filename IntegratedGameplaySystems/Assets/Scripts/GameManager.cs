using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private FSM<GameManager> fsm;

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
    public ObjectPool objectPool;
    public GameObject Prefab;
    public Bullets bullets;

    #region Dictionaries and Lists
    public List<GameObject> InactivePooledObjects = new();
    public List<GameObject> ActivePooledObjects = new();

    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();
    #endregion

    private void Start()
    {
        //ZORG DAT DIT BOVENAAN STAAT ANDERS KRIJG JE EEN NULLREFERENCE
        fsm = new FSM<GameManager>();
        fsm.Initialize(this);

        objectPool = new(this);
        inputHandler = new InputHandler();

        var playerMovement = new PlayerMovement(fsm);
        var fireGun = new FireGunCommand(fsm);
        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.W, playerMovement, new MovementContext { Direction = Vector3.forward });
        inputHandler.BindInputToCommand(KeyCode.A, playerMovement, new MovementContext { Direction = Vector3.left });
        inputHandler.BindInputToCommand(KeyCode.S, playerMovement, new MovementContext { Direction = Vector3.back });
        inputHandler.BindInputToCommand(KeyCode.D, playerMovement, new MovementContext { Direction = Vector3.right });

        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.W, playerMovement, new MovementContext { Direction = Vector3.forward });
        inputHandler.BindInputToCommand(KeyCode.A, playerMovement, new MovementContext { Direction = Vector3.left });
        inputHandler.BindInputToCommand(KeyCode.S, playerMovement, new MovementContext { Direction = Vector3.back });
        inputHandler.BindInputToCommand(KeyCode.D, playerMovement, new MovementContext { Direction = Vector3.right });

        fsm.AddState(new InstantiateGameObjects(fsm));
        fsm.AddState(fireGun);
        fsm.AddState(new IdleState(fsm));
        fsm.AddState(playerMovement);

        fsm.SwitchState(typeof(InstantiateGameObjects));


        DeactivationDelegate += objectPool.DeActivate;
        objectPoolDelegate += objectPool.GetPooledObjects;
    }

    private void Update()
    {
        inputHandler.HandleInput();
        fsm.Update();
    }

    public void RunCoroutine(IEnumerator Routine)
    {
        StartCoroutine(Routine);
    }
}
