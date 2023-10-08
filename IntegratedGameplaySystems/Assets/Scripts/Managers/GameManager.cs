using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly FSM<GameManager> fsm = new();

    public ActorData playerData;

    #region Delegates
    public delegate void Deactivationhandler(ActorBase bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate ActorBase ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public event Action OnUpdate;
    public event Action OnFixedUpdate;
    public event Action OnDisableEvent;

    public ObjectPool ObjectPool;

    public Bullets bullets;
    public EnemyData enemyData;

    public WeaponData[] Weapons = new WeaponData[1];

    #region Dictionaries and Lists


    #endregion

    private Player player;
    public IDamageableActor damageable;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = new(this, playerData);
        ObjectPool = new(this);

        SetupGameStates();

        objectPoolDelegate += ObjectPool.GetPooledObjects;
        DeactivationDelegate += ObjectPool.DeActivate;

    }

    private void SetupGameStates()
    {

        fsm.AddState(new InstantiateGameObjects(fsm, ObjectPool, this));
        fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    private void Update()
    {
        fsm.OnUpdate();

        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }

    private void OnDisable()
    {
        OnDisableEvent?.Invoke();
        OnFixedUpdate = null;
        OnUpdate = null;
        OnDisableEvent = null;
    }
}
