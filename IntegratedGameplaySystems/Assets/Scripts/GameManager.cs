using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    FSM<GameManager> fsm;
    #region Adjustable Variables
    public int AmountToPool = 30;
    #endregion

    #region Delegates
    //Delegates
    public delegate void Deactivationhandler(GameObject bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate GameObject ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public InputHandler inputHandler;
    public ObjectPool objectPool;
    public GameObject PreFab;
    //scriptable object
    #region Dictionaries and Lists
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();

    //alle dictionaries voor mijn simpele object pool
    public List<GameObject> InactivePooledObjects = new List<GameObject>();
    public List<GameObject> ActivePooledObjects = new List<GameObject>();
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        //ZORG DAT DIT BOVENAAN STAAT ANDERS KRIJG JE EEN NULLREFERENCE
        fsm = new FSM<GameManager>();
        fsm.Initialize(this);
        inputHandler = new InputHandler();

        var playerMovement = new PlayerMovement(fsm);
        var fireGun = new FireGunCommand(fsm);
        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.W, playerMovement, new MovementContext { Direction = Vector3.up });


        fsm.AddState(new InstantiateGameObjects(fsm));
        fsm.AddState(fireGun);
        fsm.AddState(new IdleState(fsm));
        fsm.AddState(playerMovement);

        //fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
