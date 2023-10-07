using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly FSM<GameManager> fsm = new();
    public PlayerData playerData;

    #region Adjustable Variables
    public int AmountToPool = 30;
    #endregion

    #region Delegates
    public delegate void Deactivationhandler(ActorBase bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate ActorBase ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public event Action OnUpdate;
    public event Action OnFixedUpdate;

    public ObjectPool ObjectPool;

    public GameObject BulletPrefab;
    public Bullets bullets;

    public List<IUpdate> UpdatableObjects = new();

    #region Dictionaries and Lists


    #endregion

    private Player player;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = new(this, playerData);

        SetupGameStates();

        objectPoolDelegate += ObjectPool.GetPooledObjects;
        DeactivationDelegate += ObjectPool.DeActivate;

    }

    private void SetupGameStates()
    {
        ObjectPool = new(this);

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
        fsm.OnUpdate();

        OnUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnFixedUpdate?.Invoke();
    }
}
